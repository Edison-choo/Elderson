using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elderson.Models;
using Elderson.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Elderson.Pages.DRR
{
    public class AppointmentsModel : PageModel
    {
        [BindProperty]
        public List<Booking> myBookings { get; set; }
        private BookingService _svc;
        public AppointmentsModel(BookingService service)
        {
            _svc = service;
        }
        public void OnGet()
        {
            if (HttpContext.Session.GetString("LoginUser") != null)
            {
                myBookings = _svc.GetBookingOfDoctor(HttpContext.Session.GetString("LoginUser"));
            }
            else
            {
                Redirect("~/Login");
            }
        }
    }
}
