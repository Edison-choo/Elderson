using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elderson.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Elderson.Pages.Elderly
{
    public class CallModel : PageModel
    {
        [BindProperty]
        public string myUserID { get; set; }
        [BindProperty]
        public string myCallID { get; set; }
        [BindProperty]
        public string myDoctorID { get; set; }
        [BindProperty]
        public string myDoctorPhoto { get; set; }
        [BindProperty]
        public string myDoctorName { get; set; }
        private BookingService _svc;
        private UserService _uSvc;
        public CallModel(BookingService service, UserService uService)
        {
            _svc = service;
            _uSvc = uService;
        }
        public IActionResult OnGet(string id)
        {
            if (_svc.isInCall(id, HttpContext.Session.GetString("LoginUser")))
            {
                myUserID = HttpContext.Session.GetString("LoginUser");
                myCallID = _svc.GetBookingById(id).CallUUID;
                myDoctorID = _svc.GetBookingById(id).DoctorID;
                myDoctorName = _uSvc.GetNameById(myDoctorID);
                myDoctorPhoto = _uSvc.GetDoctorById(myDoctorID).Photo;
                HttpContext.Session.SetString("CallID", myCallID);
                return Page();
            }
            else
            {
                return Redirect("~/");
            }
            
        }
        public IActionResult OnPost()
        {
            return Redirect("~/");
        }
    }
}
