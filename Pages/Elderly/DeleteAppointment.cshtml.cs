using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elderson.Models;
using Elderson.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Elderson.Pages.Elderly
{
    public class DeleteAppointmentModel : PageModel
    {
        [BindProperty]
        public Booking myBooking { get; set; }
        private BookingService _svc;
        private ScheduleService _sSvc;
        
        public DeleteAppointmentModel(BookingService service, ScheduleService sService)
        {
            _svc = service;
            _sSvc = sService;
        }
        public IActionResult OnGet(string id)
        {
            if (HttpContext.Session.GetString("LoginUser") != null)
            {
                Booking deleteBook = _svc.GetBookingById(id);
                Schedule updateSchedule = _sSvc.GetScheduleByDateTime(deleteBook.BookDateTime);
                updateSchedule.Availability = "A";
                _svc.DeleteBooking(deleteBook);
                _sSvc.UpdateScheduleStatus(updateSchedule);
                return Page();
            }
            return Redirect("~/");
        }
    }
}
