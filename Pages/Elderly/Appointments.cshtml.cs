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
    public class AppointmentModel : PageModel
    {
        [BindProperty]
        public List<Booking> myBookings { get; set; }
        private BookingService _svc;
        public AppointmentModel(BookingService service)
        {
            _svc = service;
        }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("LoginUser") != null)
            {
                myBookings = _svc.GetBookingOfUser(HttpContext.Session.GetString("LoginUser"));
                return Page();
            }
            return Redirect("~/");
        }
    }
}
