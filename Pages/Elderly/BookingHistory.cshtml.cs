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
    public class BookingHistory
    {
        public string clinicName { get; set; }
        public string doctorName { get; set; }
        public DateTime BookingDateTime { get; set; }
    }
    public class BookingHistoryModel : PageModel
    {
        public List<Booking> bookings { get; set; }
        [BindProperty]
        public List<BookingHistory> bookingHistoryList { get; set; }
        private BookingService _svc;
        private UserService _usvc;
        public BookingHistoryModel(BookingService service, UserService uservice)
        {
            _svc = service;
            _usvc = uservice;
        }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("LoginUser") != null)
            {
                bookingHistoryList = new List<BookingHistory>();
                bookings = _svc.GetBookingHistoryOfUser(HttpContext.Session.GetString("LoginUser"));
                foreach (Booking booking in bookings)
                {
                    BookingHistory bookingHistory = new BookingHistory();
                    bookingHistory.clinicName = booking.Clinic;
                    bookingHistory.doctorName = _usvc.GetUserById(booking.DoctorID).Fullname;
                    bookingHistory.BookingDateTime = booking.BookDateTime;
                    bookingHistoryList.Add(bookingHistory);
                }
                return Page();
            }
            else
            {
                return Redirect("~/");
            }
        }
    }
}
