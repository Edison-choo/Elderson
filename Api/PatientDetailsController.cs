using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Elderson.Models;
using Elderson.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
        private readonly INotyfService _notfy;
        private readonly ILogger<UserController> _logger;
        public PatientDetailsController(EldersonContext context, AdministratorService service, INotyfService notyf, ILogger<UserController> logger)
        {
            _context = context;
            _svc = service;
            _notfy = notyf;
            _logger = logger;

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

        [HttpGet("{apiname}/{userId}", Name = "GetDetails")]
        public ActionResult<List<PatientDetails>> GetDetails(string userId)
        {
            List<PatientDetails> allDetails = new List<PatientDetails>();

            try
            {
                allDetails = _svc.GetAllEntries();
                var jsonStr = JsonSerializer.Serialize(allDetails.Where(e => e.PatientID == userId).Select(x => new { x.Id, x.Title, x.DetailsofVisit, x.PatientID }));
                return Ok(jsonStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine("instalmentController.getCarLoan", ex);
                return BadRequest();
            }
        }

        // Update api/<controller>/5
        [HttpPut ("{id}")]
        public ActionResult Update(string Id, PatientDetails details)
        {
            try
            {
                var updateDetails= _svc.GetEntryById(Id);
                
                _svc.UpdateEntry(updateDetails);
            }
            catch (Exception ex)
            {
                Console.WriteLine("PatientDetailsController.UpdateDetails", ex);
            }
            return Ok();
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

        public class CompositeObject
        {
            //public PatientDetails patientDetails { get; set; }
            public string DetailsofVisit { get; set; }
            public string Title { get; set; }
            public string Id { get; set; }

        }

        // POST api/<UserController>
        [HttpPost]
        public ActionResult<string> Post([FromForm] CompositeObject body)
        {
            Console.WriteLine(body.Title + body.DetailsofVisit);
            Console.WriteLine(ModelState.IsValid);

            if (!(ModelState.IsValid))
            {
                _notfy.Error("Error");
                Console.WriteLine("Test1");
                return BadRequest("Error");
            }

            PatientDetails UpdatedDetails;

            UpdatedDetails = _svc.GetEntryById(body.Id);
            if (_svc.GetEntryById(body.Id) != null && body.Id != UpdatedDetails.Id)
            {
                _logger.LogInformation("Unsuccessful", body.Id, "edit user details");
                _notfy.Error("ID is already used");
                Console.WriteLine("Test2");
                return BadRequest("Error");
            }
            UpdatedDetails.Title = body.Title;
            UpdatedDetails.DetailsofVisit = body.DetailsofVisit;
            Boolean valid = _svc.UpdateEntry(UpdatedDetails);

            if (valid)
            {
                _logger.LogInformation("{actionStatus} User {userId} {userAction}.", "Successful", body.Id, "edit user");
                _notfy.Success("Edit User Successfully");
                Console.WriteLine("Test3");
                return Ok("Success");

            }
            Console.WriteLine("Test4"); ;
            return BadRequest("Error");

        }
    } 
}
