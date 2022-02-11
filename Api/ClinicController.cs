using AspNetCoreHero.ToastNotification.Abstractions;
using Elderson.Models;
using Elderson.Pages;
using Elderson.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Elderson.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicController : ControllerBase
    {
        private readonly EldersonContext _context;
        private UserService _svc;
        private readonly INotyfService _notfy;
        public ClinicController(EldersonContext context, UserService service, INotyfService notyf)
        {
            _context = context;
            _svc = service;
            _notfy = notyf;
        }
        [HttpGet("{apiname}/{search}/{page}", Name = "getPage")]
        public ActionResult<List<Clinic>> Get(string search, int page)
        {
            List<Clinic> allClinics = _svc.GetAllClinic();
            try
            {
                var jsonStr = JsonSerializer.Serialize(allClinics.Select(c => new { c.Name, c.Address, c.CountryCode, c.Phone, }));
                return Ok(jsonStr);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
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
