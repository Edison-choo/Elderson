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
        [HttpGet("{apiname}/{doctorID}/{month}/{year}", Name = "GetDoctorDate")]
        public ActionResult<List<DateFormatSchedule>> GetDoctorDate(string doctorID, int month, int year)
        {
            try
            {
                List<DateFormatSchedule> dfsList = new List<DateFormatSchedule>();
                List<Schedule>  dSchedule = _svc.GetOneDoctorSchedule(doctorID);
                foreach(var item in dSchedule)
                {
                    if (item.StartDateTime.Month-1 == month && item.StartDateTime.Year == year)
                    {
                        DateFormatSchedule dfs = new DateFormatSchedule();
                        dfs.date = item.StartDateTime.ToString("dd");
                        dfs.availability = "a";
                        dfsList.Add(dfs);
                    }
                }
                var jsonStr = JsonSerializer.Serialize(dfsList);
                return Ok(dfsList);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }
        [HttpGet("{apiname}/{doctorID}/{day}/{month}/{year}", Name = "GetDoctorTime")]
        public ActionResult<List<TimeFormatSchedule>> GetDoctorTime(string doctorID, int day, int month, int year)
        {
            try
            {
                List<TimeFormatSchedule> tfsList = new List<TimeFormatSchedule>();
                List<Schedule> dSchedule = _svc.GetOneDoctorSchedule(doctorID);
                foreach (var item in dSchedule)
                {
                    if (item.StartDateTime.Day == day && item.StartDateTime.Month-1 == month && item.StartDateTime.Year == year)
                    {
                        TimeFormatSchedule tfs = new TimeFormatSchedule();
                        tfs.time = item.StartDateTime.ToString("HH:mm");
                        tfs.availability = item.Availability.ToLower();
                        tfsList.Add(tfs);
                    }
                }
                var jsonStr = JsonSerializer.Serialize(tfsList);
                return Ok(tfsList);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }
        public class DateFormatSchedule
        {
            public string date { get; set; }
            public string availability { get; set; }
        }
        public class TimeFormatSchedule
        {
            public string time { get; set; }
            public string availability { get; set; }
        }
    }
}
