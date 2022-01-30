using AspNetCoreHero.ToastNotification.Abstractions;
using Elderson.Models;
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
    public class IncidentController : Controller
    {
        private readonly EldersonContext _context;
        private IncidentService _svc;
        private UserService _u_svc;
        private readonly INotyfService _notfy;

        public IncidentController(EldersonContext context, IncidentService service, UserService userService, INotyfService notyf)
        {
            _context = context;
            _svc = service;
            _u_svc = userService;
            _notfy = notyf;
        }

        // GET: api/<UserController>
        [HttpGet]
        public ActionResult<List<Incident>> Get()
        {
            List<Incident> allIncident = new List<Incident>();

            try
            {
                allIncident = _svc.GetAllIncidents();
                var jsonStr = JsonSerializer.Serialize(allIncident.Select(x => new { x.Id, x.Title, User = _u_svc.GetUserById( x.UserId ).Fullname}));
                return Ok(jsonStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine("instalmentController.getCarLoan", ex);
                return BadRequest();
            }

        }

        //// GET api/<UserController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<UserController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<UserController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<UserController>/5
        [HttpDelete("{incidentId}")]
        public ActionResult Delete(string incidentId)
        {
            try
            {
                var deleteUser = _svc.GetIncidentById(incidentId);
                _svc.DeleteIncident(deleteUser);
                _notfy.Success("Delete User Successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine("UserController.DeleteUser", ex);
                return BadRequest();
            }
            return Ok();
        }
    }
}
