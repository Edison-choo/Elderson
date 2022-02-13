using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Elderson.Models;
using Elderson.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Elderson.Pages
{
    public class CartModel : PageModel
    {
        [BindProperty]
        public List<CartItem> myCart { get; set; }
        [BindProperty]
        public int myTotal { get; set; } = 0;
        private BookingService _svc;
        private PrescriptionService _psvc;
        private readonly ILogger<CartModel> _logger;
        private readonly INotyfService _notfy;
        private ScheduleService _sSvc;

        public CartModel(ILogger<CartModel> logger, BookingService service, ScheduleService sService, PrescriptionService pservice, INotyfService notyf)
        {
            _svc = service;
            _logger = logger;
            _sSvc = sService;
            _psvc = pservice;
            _notfy = notyf;
        }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("LoginUser") != null)
            {
                return Page();
            }
            else
            {
                return Redirect("~/");
            }
            
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                bool valid = true;
                myCart = HttpContext.Session.GetCart("Cart");
                if (myCart == null)
                {
                    _notfy.Warning("Your cart is empty!");
                    valid = false;
                }
                else
                {
                    bool avail = true;
                    List<DateTime> times = new List<DateTime>();
                    foreach (CartItem item in myCart)
                    {
                        Schedule availability =
                            _sSvc.GetScheduleByDateTime(item.DoctorID, Convert.ToDateTime(item.BookDateTime));
                        if (times.Contains(availability.StartDateTime))
                        {
                            avail = false;
                        }
                        times.Add(availability.StartDateTime);
                        if (availability.Availability.ToUpper() != "A")
                        {
                            avail = false;
                        }
                        if (DateTime.Now > availability.StartDateTime)
                        {
                            avail = false;
                        }
                    }
                    if (!avail)
                    {
                        valid = false;
                        _notfy.Warning("Booking is invalid!");
                    }
                }
                if (valid)
                {
                    foreach (CartItem item in myCart)
                    {
                        string bookUUID = Guid.NewGuid().ToString();
                        string callUUID = Guid.NewGuid().ToString();
                        Booking book = new Booking();
                        book.Id = bookUUID;
                        book.Clinic = item.Clinic;
                        book.BookDateTime = Convert.ToDateTime(item.BookDateTime);
                        book.Symptoms = item.Symptoms;
                        book.PatientID = HttpContext.Session.GetString("LoginUser");
                        book.DoctorID = item.DoctorID;
                        book.CallUUID = callUUID;
                        _svc.AddBooking(book);
                        Schedule schedule = _sSvc.GetScheduleByDateTime(item.DoctorID, Convert.ToDateTime(item.BookDateTime));
                        schedule.Availability = "B";
                        _sSvc.UpdateScheduleStatus(schedule);
                        HttpContext.Session.Remove("Cart");
                    }
                }
                if (HttpContext.Session.GetString("Prescription") != null)
                {
                    Prescription myPrescription = _psvc.GetPrescriptionByID(HttpContext.Session.GetString("Prescription"));
                    myPrescription.IsPurchased = true;
                    _psvc.UpdatePrescription(myPrescription);
                    HttpContext.Session.Remove("Prescription");
                    HttpContext.Session.Remove("MedicationCart");
                }
                else
                {
                    return Page();
                }
            }
            else
            {
                return Page();
            }
            return Redirect("~/");
        }
    }
    public static class SessionExtensions
    {
        public static void SetCart(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        public static List<CartItem> GetCart(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(List<CartItem>) : JsonConvert.DeserializeObject<List<CartItem>>(value);
        }
        public static List<CartMedication> GetMedicationCart(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(List<CartMedication>) : JsonConvert.DeserializeObject<List<CartMedication>>(value);
        }
    }
}
