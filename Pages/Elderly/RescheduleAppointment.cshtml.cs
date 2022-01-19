using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elderson.Models;
using Elderson.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Elderson.Pages.Elderly
{
    public class RescheduleAppointmentModel : PageModel
    {
        [BindProperty]
        public Booking myBooking { get; set; }
        [BindProperty]
        public DateTime myDateTime { get; set; }
        private BookingService _svc;
        public RescheduleAppointmentModel(BookingService service)
        {
            _svc = service;
        }
        public void OnGet(string id)
        {
            myBooking = _svc.GetBookingById(id);
        }
        public IActionResult OnPost()
        {
            myBooking.BookDateTime = myDateTime;
            return Redirect("/");
        }
    }
}
