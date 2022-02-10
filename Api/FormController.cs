using Elderson.Models;
using Elderson.Services;
using Microsoft.AspNetCore.Http;
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
    public class FormController : ControllerBase
    {
        private readonly EldersonContext _context;
        private FormService _svc;

        public FormController(EldersonContext context, FormService service)
        {
            _context = context;
            _svc = service;
        }

        // GET: api/<FormController>/doctor_id
        [HttpGet("{doctor_id}")]
        public ActionResult<List<Form>> Get(string doctor_id)
        {
            List<Form> allform = new List<Form>();

            try
            {
                allform = _svc.GetAllForm(doctor_id);
                var jsonStr = JsonSerializer.Serialize(allform.Where(x => x.DoctorId == doctor_id).Select(x => new { x.Id, x.DoctorId, x.BookingId, x.TemplateName }));
                return Ok(jsonStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine("instalmentController.getCarLoan", ex);
                return BadRequest();
            }

        }

        // DELETE api/<FormController>/id
        [HttpDelete("{formId}")]
        public ActionResult Delete(string formId)
        {
            try
            {
                var deleteForm = _svc.GetFormById(formId);
                _svc.DeleteForm(deleteForm);
                return RedirectToPage("DRR/Form");
            }
            catch (Exception ex)
            {
                Console.WriteLine("UserController.DeleteUser", ex);
            }
            return Ok();
        }
    }
}
