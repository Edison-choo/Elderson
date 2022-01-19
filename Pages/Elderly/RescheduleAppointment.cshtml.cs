using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Elderson.Models;
using Elderson.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Elderson.Pages.Elderly
{
    public class RescheduleAppointmentModel : PageModel
    {
        [BindProperty]
        public Booking myBooking { get; set; }
        [BindProperty]
        [Required, DataType(DataType.DateTime)]
        public DateTime myDateTime { get; set; }
        private BookingService _svc;
        private ScheduleService _sSvc;
        public RescheduleAppointmentModel(BookingService service, ScheduleService sService)
        {
            _svc = service;
            _sSvc = sService;
        }
        public void OnGet(string id)
        {
            if (HttpContext.Session.GetString("LoginUser") != null)
            {
                myBooking = _svc.GetBookingById(id);
            }
            else
            {
                Redirect("~/Login");
            }
        }
        public IActionResult OnPost()
        {
            Booking updatedBook = _svc.GetBookingById(myBooking.Id);
            DateTime dateTime = updatedBook.BookDateTime;
            updatedBook.BookDateTime = myDateTime;
            try
            {
                Schedule schedule = _sSvc.GetScheduleByDateTime(updatedBook.BookDateTime);
                Schedule schedule2 = _sSvc.GetScheduleByDateTime(dateTime);
                schedule.Availability = "B";
                schedule2.Availability = "A";
                _svc.UpdateBooking(updatedBook);
                _sSvc.UpdateScheduleStatus(schedule);
                _sSvc.UpdateScheduleStatus(schedule2);
            }
            catch
            {
                throw;
            }
            return Redirect("~/Elderly/Appointments");
        }
    }
}
