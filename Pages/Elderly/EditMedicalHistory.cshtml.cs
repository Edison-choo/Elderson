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
        private string m_id { get; set; }
        [BindProperty]
        public string conditionName { get; set; }
        [BindProperty]
        public string conditionDescription { get; set; }
        [BindProperty]
        public DateTime StartDate { get; set; }
        [BindProperty]
        public DateTime EndDate { get; set; }
        private MedicalHistoryService _svc;
        public EditMedicalHistoryModel(MedicalHistoryService service)
        {
            _svc = service;
        }
        public IActionResult OnGet(string id)
        {
            if (HttpContext.Session.GetString("LoginUser") != null)
            {
                m_id = id;
                medicalHistory = _svc.GetMedicalHistoryById(id);
                return Page();
            }
            return Redirect("~/");

        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                MedicalHistory newmedicalHistory = _svc.GetMedicalHistoryById(m_id);
                newmedicalHistory.Name = conditionName;
                newmedicalHistory.Description = conditionDescription;
                newmedicalHistory.StartDate = StartDate;
                newmedicalHistory.EndDate = EndDate;
                _svc.UpdateMedicalHistory(newmedicalHistory);
                return Redirect("/Elderly/MedicalHistory");
            }
            return Page();
        }
    }
}
