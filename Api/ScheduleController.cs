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
    public class ScheduleController : ControllerBase
    {
        private readonly EldersonContext _context;
        private ScheduleService _svc;

        public ScheduleController(EldersonContext context, ScheduleService service)
        {
            _context = context;
            _svc = service;
        }

        // GET: api/<ScheduleController>
        [HttpGet]
        public ActionResult<List<Schedule>> Get()
        {
            List<Schedule> allschedule = new List<Schedule>();

            try
            {
                allschedule = _svc.GetAllSchedule();
                var jsonStr = JsonSerializer.Serialize(allschedule.Select(x => new { x.Id, x.DoctorId, x.StartDateTime }));
                return Ok(jsonStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine("instalmentController.getCarLoan", ex);
                return BadRequest();
            }

        }
    }
}
