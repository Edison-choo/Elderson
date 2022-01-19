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
        public List<DoctorDetail> myDetailList { get; set; }
        private UserService _svc;
        public DoctorModel(UserService service)
        {
            _svc = service;
        }

        public void OnGet(string clinic)
        {
            if (clinic != null)
            {
                myDetailList = new List<DoctorDetail>();
                myClinic = "HealthWerkz Clinic";
                myDoctorList = _svc.GetDoctorsByClinic(myClinic);
                foreach (Doctor doc in myDoctorList)
                {
                    DoctorDetail dd = new DoctorDetail();
                    User user = _svc.GetUserById(doc.UserId);
                    dd.id = doc.UserId;
                    dd.name = user.Fullname;
                    dd.experience = doc.WorkExp;
                    dd.language = doc.Language;
                    myDetailList.Add(dd);
                }
            }
            else
            {
                Redirect("/Clinic");
            }
        }
    }
    public class DoctorDetail
    {
        public string id { get; set; }
        public string name { get; set; }
        public string experience { get; set; }
        public string language { get; set; }
    }
}
