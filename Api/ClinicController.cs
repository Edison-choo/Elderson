using AspNetCoreHero.ToastNotification.Abstractions;
using Elderson.Models;
using Elderson.Filter;
using Elderson.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Elderson.Wrappers;
using Elderson.Helpers;

namespace Elderson.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicController : ControllerBase
    {
        private readonly EldersonContext _context;
        private UserService _svc;
        private readonly INotyfService _notfy;
        private readonly IUriService _uriService;
        public ClinicController(EldersonContext context, UserService service, INotyfService notyf, IUriService uriService)
        {
            _context = context;
            _svc = service;
            _notfy = notyf;
            _uriService = uriService;
        }
        [HttpGet("GetAll", Name = "GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await _svc.GetAllClinicAsync()
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
            var totalRecords = await _svc.GetAllClinicAsync().CountAsync();
            var pagedResponse = PaginationHelper.CreatePagedReponse<Clinic>(pagedData, validFilter, totalRecords, _uriService, route);
            return Ok(pagedResponse);
        }

        // GET: api/<UserController>
        [HttpGet]
        public ActionResult<List<Clinic>> Get()
        {
            List<Clinic> allclinic = new List<Clinic>();

            try
            {
                allclinic = _svc.GetAllClinic();
                var jsonStr = JsonSerializer.Serialize(allclinic.Select(x => new { x.Id, x.Name, Phone = "+"+x.CountryCode+" "+x.Phone }));
                return Ok(jsonStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine("instalmentController.getCarLoan", ex);
                return BadRequest();
            }

        }

        // DELETE api/<UserController>/5
        [HttpDelete("{clinicId}")]
        public ActionResult Delete(string clinicId)
        {
            try
            {
                var deleteClinic = _svc.GetClinicById(clinicId);

                var doctors = _svc.GetDoctorsByClinic(deleteClinic.Name);
                foreach (var doctor in doctors)
                {
                    Console.WriteLine(doctor.ClinicId);
                    doctor.ClinicId = null;
                    _svc.UpdateDoctor(doctor);
                }
                _svc.DeleteClinic(deleteClinic);
                _notfy.Success("Delete Clinic Successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ClinicController.DeleteClinic", ex);
                return BadRequest();
            }
            return Ok();
        }
    }
}
