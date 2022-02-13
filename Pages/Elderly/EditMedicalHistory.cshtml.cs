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
    public class EditMedicalHistoryModel : PageModel
    {
        [BindProperty]
        public MedicalHistory medicalHistory { get; set; }

        private MedicalHistoryService _svc;
        public EditMedicalHistoryModel(MedicalHistoryService service)
        {
            _svc = service;
        }
        public IActionResult OnGet(string id)
        {
            if (HttpContext.Session.GetString("LoginUser") != null)
            {
                medicalHistory = _svc.GetMedicalHistoryById(id);
                return Page();
            }
            return Redirect("~/");

        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                MedicalHistory newmedicalHistory = new MedicalHistory();
                newmedicalHistory = _svc.GetMedicalHistoryById(medicalHistory.Id);
                newmedicalHistory.Name = medicalHistory.Name;
                newmedicalHistory.Description = medicalHistory.Description;
                newmedicalHistory.StartDate = medicalHistory.StartDate;
                newmedicalHistory.EndDate = medicalHistory.EndDate;
                _svc.UpdateMedicalHistory(newmedicalHistory);
                return Redirect("/Elderly/MedicalHistory");
            }
            return Page();
        }
    }
}
