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
using Elderson.Services;

namespace Elderson.Pages.Elderly
{
    public class DoctorModel : PageModel
    {
        [BindProperty]
        public string myClinic { get; set; }
        [BindProperty]
        public List<Doctor> myDoctorList { get; set; }
        [BindProperty]
        public List<User> myUserList { get; set; }
        private UserService _svc;
        public DoctorModel(UserService service)
        {
            _svc = service;
        }

        public void OnGet(string clinic)
        {
            if (clinic != null)
            {
                myUserList = new List<User>();
                myClinic = "HealthWerkz Clinic";
                myDoctorList = _svc.GetDoctorsByClinic(myClinic);
                foreach (Doctor doc in myDoctorList)
                {
                    myUserList.Add(_svc.GetUserById(doc.UserId));
                }
            }
            else
            {
                Redirect("/Clinic");
            }
        }
    }
}
