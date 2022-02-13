using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elderson.Models;
using Elderson.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Elderson.Pages.DRR
{
    public class PrescriptionModel : PageModel
    {
        [BindProperty]
        public string currentform_id { get; set; }
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
        private InventoryService _invsvc { get; set; }
        private FormMedsService _fmsvc { get; set; }
        private PrescriptionService _pssvc { get; set; }
        private readonly ILogger<PrescriptionModel> _logger;
        public PrescriptionModel(ILogger<PrescriptionModel> logger, FormService service, UserService userservice, BookingService bookservice, InventoryService invservice, FormMedsService fmservice, PrescriptionService psservice)
        {
            _svc = service;
            _usrsvc = userservice;
            _bksvc = bookservice;
            _invsvc = invservice;
            _fmsvc = fmservice;
            _pssvc = psservice;
            _logger = logger;
        }
        public IActionResult OnGet(string bookingid, string formid)
        {
            if (HttpContext.Session.GetString("LoginUser") != null)
            {
                if (HttpContext.Session.GetString("LoginUserType") == "Doctor")
                {
                    currentform_id = Guid.NewGuid().ToString();
                    booking_id = bookingid;
                    form_id = formid;
                    check = _svc.TemplateFormExist(formid);
                    List<FormMeds> formmeds = new List<FormMeds>();
                    Medication meds = new Medication();
                    if (check)
                    {
                        formmeds = _fmsvc.GetFormMedsByFormId(formid);
                        foreach (FormMeds formmed in formmeds)
                        {
                            meds = _invsvc.GetMedicationById(formmed.MedicationId);
                            FormMeds currentMeds = new FormMeds();
                            currentMeds.Id = Guid.NewGuid().ToString();
                            currentMeds.FormId = currentform_id;
                            currentMeds.MedicationId = meds.Id;
                            currentMeds.Quantity = formmed.Quantity;
                            currentMeds.MedName = formmed.MedName;
                            currentMeds.MedType = formmed.MedType;
                            _fmsvc.AddFormMeds(currentMeds);
                        }
                    }
                    booking = _bksvc.GetBookingById(bookingid);
                    patientuser = _usrsvc.GetUserById(booking.PatientID);
                    return Page();
                }
            }

            return Redirect("~/");
            
            
        }

        public IActionResult OnPost()
        {
            Prescription prescription = new Prescription();
            string uuid = Guid.NewGuid().ToString();
            prescription.Id = uuid;
            prescription.PatientName = patientuser.Fullname;
            prescription.PatientId = booking.PatientID;
            prescription.FormId = currentform_id;
            prescription.DoctorName = _usrsvc.GetUserById(HttpContext.Session.GetString("LoginUser")).Fullname;
            prescription.Date = DateTime.Now;
            prescription.Status = "0";
            prescription.Symptoms = booking.Symptoms;
            prescription.BookingId = booking_id;
            prescription.DoctorId = HttpContext.Session.GetString("LoginUser");

            _pssvc.AddPrescription(prescription);

            Booking updateBooking = new Booking();
            updateBooking = _bksvc.GetBookingById(booking_id);
            updateBooking.FormId = uuid;
            _bksvc.UpdateBooking(updateBooking);

            return RedirectToPage("Consultation");
        }
    }
}
