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
    public class AddMedicalHistoryModel : PageModel
    {
        [BindProperty]
        public MedicalHistory medicalHistory { get; set; }
        private MedicalHistoryService _svc;
        public AddMedicalHistoryModel(MedicalHistoryService service)
        {
            _svc = service;
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
                MedicalHistory newMedicalHistory = new MedicalHistory();
                newMedicalHistory.Id = Guid.NewGuid().ToString();
                newMedicalHistory.Name = medicalHistory.Name;
                newMedicalHistory.Description = medicalHistory.Description;
                newMedicalHistory.StartDate = medicalHistory.StartDate;
                newMedicalHistory.EndDate = medicalHistory.EndDate;
                newMedicalHistory.PatientID = HttpContext.Session.GetString("LoginUser");
                _svc.AddMedicalHistory(newMedicalHistory);
                return Redirect("/Elderly/MedicalHistory");
            }
            return Page();
        }
    }
}
