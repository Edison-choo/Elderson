using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Elderson.Models;
using Elderson.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Elderson.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientDetailsController : ControllerBase
    {
        public PatientDetails NewEntry { get; set; }
        private readonly EldersonContext _context;
        private AdministratorService _svc;
        public PatientDetailsController(EldersonContext context, AdministratorService service)
        {
            _context = context;
            _svc = service;
        }
        // GET: api/<controller>
        [HttpGet]
        public ActionResult<List<PatientDetails>> Get()
        {
            List<PatientDetails> allDetails = new List<PatientDetails>();

            try
            {
                allDetails = _svc.GetAllEntries();
                var jsonStr = JsonSerializer.Serialize(allDetails.Select(x => new { x.Id, x.Title, x.DetailsofVisit }));
                return Ok(jsonStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine("instalmentController.getCarLoan", ex);
                return BadRequest();
            }
        }


        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string Id)
        {
            try
            {
                var deleteUser = _svc.GetEntryById(Id);
                _svc.DeleteEntry(deleteUser);
            }
            catch (Exception ex)
            {
                Console.WriteLine("PatientDetailsController.DeleteDetails", ex);
            }
            return Ok();
        }
    }
}
