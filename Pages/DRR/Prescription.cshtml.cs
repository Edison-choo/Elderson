using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elderson.Models;
using Elderson.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Elderson.Pages.DRR
{
    public class PrescriptionModel : PageModel
    {
        [BindProperty]
        public string booking_id { get; set; }
        [BindProperty]
        public string form_id { get; set; }
        [BindProperty]
        public bool check { get; set; }
        [BindProperty]
        public Patient patient { get; set; }
        [BindProperty]
        public User patientuser { get; set; }
        [BindProperty]
        public Booking booking { get; set; }
        private FormService _svc { get; set; }
        private UserService _usrsvc { get; set; }
        private BookingService _bksvc { get; set; }
        private readonly ILogger<PrescriptionModel> _logger;
        public PrescriptionModel(ILogger<PrescriptionModel> logger, FormService service, UserService userservice, BookingService bookservice)
        {
            _svc = service;
            _usrsvc = userservice;
            _bksvc = bookservice;
            _logger = logger;
        }
        public void OnGet(string bookingid, string formid)
        {
            booking_id = bookingid;
            form_id = formid;
            check = _svc.TemplateFormExist(formid);
            booking = _bksvc.GetBookingById(bookingid);
            patientuser = _usrsvc.GetUserById(booking.PatientID);
        }
    }
}
