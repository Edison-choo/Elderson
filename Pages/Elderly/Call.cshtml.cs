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
        private BookingService _svc;
        private UserService _uSvc;
        public CallModel(BookingService service, UserService uService)
        {
            _svc = service;
            _uSvc = uService;
        }
        public void OnGet(string id)
        {
            if (HttpContext.Session.GetString("LoginUser") != null)
            {
                myUserID = HttpContext.Session.GetString("LoginUser");
            }
            else
            {
                Redirect("~/Login");
            }
            myCallID = _svc.GetBookingById(id).CallUUID;
            myDoctorID = _svc.GetBookingById(id).DoctorID;
            HttpContext.Session.SetString("CallID", myCallID);
        }
    }
}
