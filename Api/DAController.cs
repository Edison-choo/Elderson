using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elderson.Models;
using Elderson.Services;
using System.Text.Json;

namespace Elderson.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class DAController : ControllerBase
    {
        private readonly EldersonContext _context;
        private ScheduleService _svc;
        public DAController(EldersonContext context, ScheduleService service)
        {
            _context = context;
            _svc = service;
        }
        [HttpGet("{doctorID}")]
        public ActionResult<List<Schedule>> GetDoctorSchedule(string doctorID)
        {
            List<Schedule> dSchedule = new List<Schedule>();
            try
            {
                dSchedule = _svc.GetOneDoctorSchedule(doctorID);
                var jsonStr = JsonSerializer.Serialize(dSchedule.Select(s =>));
            }
            catch
            {

            }
            return dSchedule;
        }

    }
}
