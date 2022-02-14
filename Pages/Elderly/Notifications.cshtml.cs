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
    public class NotificationsModel : PageModel
    {
        public Prescription prescription { get; set; }
        public List<Prescription> prescriptionList { get; set; }
        public List<DateTime> specificPrescriptionList { get; set; } = new List<DateTime>();
        private PrescriptionService _svc;
        public NotificationsModel(PrescriptionService service)
        {
            _svc = service;
        }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("LoginUser") != null)
            {
                
                prescriptionList = _svc.GetAllPrescriptions();
                foreach (var p in prescriptionList)
                {
                    if (p.PatientId == HttpContext.Session.GetString("LoginUser") && p.Status == "1")
                    {
                        specificPrescriptionList.Add(p.Date);
                    }
                    else
                    {
                        specificPrescriptionList = null;
                    }
                }
                return Page();
            }
            return Redirect("~/");
        }
    }
}