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

namespace Elderson.Pages.Shared
{
    public class DateAndTimeModel : PageModel
    {
        [BindProperty]
        public string myClinic { get; set; }
        [BindProperty]
        public string myDoctor { get; set; }
        [BindProperty]
        public DateTime myDateTime { get; set; }
        public void OnGet(string clinic, string doctor)
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
                    myClinic = clinic;
                    myDoctor = doctor;
                }
            }
        }
        public IActionResult OnPost()
        {
            HttpContext.Session.SetString("myClinic", myClinic);
            HttpContext.Session.SetString("myDoctor", myDoctor);
            HttpContext.Session.SetString("myDateTime", myDateTime.ToString("dd MMMM yyyy HH:MM tt"));
            return RedirectToPage("Symptoms");
        }
    }
}
