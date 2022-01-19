using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elderson.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Elderson.Services;

namespace Elderson.Pages.Shared
{
    public class DateAndTimeModel : PageModel
    {
        [BindProperty]
        [Required]
        public string myClinic { get; set; }
        [BindProperty]
        [Required]
        public string myDoctor { get; set; }
        [BindProperty]
        public string myDoctorName { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Please enter date and time of booking!"), DataType(DataType.DateTime)]
        public DateTime myDateTime { get; set; }
        [BindProperty]
        [Required]
        public string myDate { get; set; }
        [BindProperty]
        [Required]
        public string myTime { get; set; }
        private UserService _svc;
        public DateAndTimeModel(UserService service)
        {
            _svc = service;
        }
        public void OnGet(string doctor)
        {
            if (HttpContext.Session.GetString("myClinic") != null)
            {
                myClinic = HttpContext.Session.GetString("myClinic");
                myDoctor = HttpContext.Session.GetString("myDoctor");
            }
            else
            {
                if (doctor == null)
                {
                    Response.Redirect("Clinic");
                }
                else
                {
                    myDoctor = doctor;
                    try
                    {
                        myDoctorName = _svc.GetUserById(doctor).Fullname;
                        myClinic = _svc.GetDoctorById(doctor).Clinic;
                    }
                    catch
                    {
                        Response.Redirect("Clinic");
                    }
                }
            }
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
<<<<<<< HEAD
<<<<<<< HEAD
                HttpContext.Session.SetString("myClinic", myClinic);
                HttpContext.Session.SetString("myDoctor", myDoctor);
                HttpContext.Session.SetString("myDate", myDate);
                HttpContext.Session.SetString("myTime", myTime);
                HttpContext.Session.SetString("myDateTime", myDateTime.ToString());
                return RedirectToPage("Symptoms");
=======
                if (myDateTime > DateTime.Now)
                {
=======
                if (myDateTime > DateTime.Now)
                {
<<<<<<< HEAD
                    bool valid = true;
                    if (!(_svc.UserExists(myDoctor)))
                    {
                        valid = false;
                    }
                    if (!(_sSvc.ScheduleAvaliable(myDoctor, myDateTime)))
                    {
                        valid = false;
                    }
>>>>>>> parent of e8257f9 (done)
=======
>>>>>>> parent of 2bf0c8e (validation)
                    HttpContext.Session.SetString("myClinic", myClinic);
                    HttpContext.Session.SetString("myDoctor", myDoctor);
                    HttpContext.Session.SetString("myDate", myDate);
                    HttpContext.Session.SetString("myTime", myTime);
                    HttpContext.Session.SetString("myDateTime", myDateTime.ToString());
                    return RedirectToPage("Symptoms");
                }
                else
                {
                    return Page();
                }
<<<<<<< HEAD
>>>>>>> parent of 2bf0c8e (validation)
=======
>>>>>>> parent of e8257f9 (done)
            }
            return Page();
        }
    }
}
