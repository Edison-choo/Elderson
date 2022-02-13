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
        private BookingService _bksvc;
        private UserService _usrsvc;

        public ScheduleController(EldersonContext context, ScheduleService service, BookingService bookservice, UserService userservice)
        {
            _context = context;
            _svc = service;
            _bksvc = bookservice;
            _usrsvc = userservice;
        }

        // GET: api/<ScheduleController>
        [HttpGet("{doctor_id}")]
        public ActionResult<List<Schedule>> Get(string doctor_id)
        {
            List<Schedule> allschedule = new List<Schedule>();

            try
            {
                allschedule = _svc.GetScheduleByDoctor(doctor_id);
                var jsonStr = JsonSerializer.Serialize(allschedule.Select(x => new { x.Id, x.DoctorId, x.StartDateTime }));
                return Ok(jsonStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine("instalmentController.getCarLoan", ex);
                return BadRequest();
            }

        }

        // GET: api/<ScheduleController>/DoctorSchedule
        [HttpGet("GetDoctorSchedule/{doctor_id}", Name = "GetDoctorSchedule")]
        public ActionResult<List<Schedule>> GetDoctorSchedule(string doctor_id)
        {
            List<Schedule> allschedule = new List<Schedule>();

            try
            {
                allschedule = _svc.GetScheduleByDoctorTime(doctor_id);
                var jsonStr = JsonSerializer.Serialize(allschedule.Select(x => new { x.Id, x.DoctorId, x.StartDateTime, x.Availability }));
                return Ok(jsonStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine("instalmentController.getCarLoan", ex);
                return BadRequest();
            }

        }

        // GET: api/<ScheduleController>/GetAllBookings/doctor_id
        [HttpGet("GetAllBookings/{doctor_id}", Name = "GetAllBookings")]
        public ActionResult<List<Booking>> GetAllBookings(string doctor_id)
        {
            List<Booking> allbookings = new List<Booking>();

            try
            {
                allbookings = _bksvc.GetBookingOfDoctor(doctor_id);
                var jsonStr = JsonSerializer.Serialize(allbookings.Where(y => y.FormId == null).Select(x => new { x.Id, x.Clinic, x.BookDateTime, x.Symptoms, x.PatientID, x.DoctorID, x.CallUUID, x.FormId, x.Status, PatientName = _usrsvc.GetUserById(x.PatientID).Fullname, date = x.BookDateTime.Date.ToShortDateString(), time = x.BookDateTime.ToLongTimeString() }).OrderByDescending(x => x.Status));
                return Ok(jsonStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine("instalmentController.getCarLoan", ex);
                return BadRequest();
            }

        }

        // DELETE api/<ScheduleController>/2
        [HttpDelete("{scheduleId}")]
        public ActionResult Delete(string scheduleId)
        {
            try
            {
                var deleteSchedule = _svc.GetScheduleById(scheduleId);
                _svc.DeleteSchedule(deleteSchedule);
                return RedirectToPage("DRR/Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine("UserController.DeleteUser", ex);
            }
            return Ok();
        }
    }
}
