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
    public class MedicalHistoryModel : PageModel
    {
        [BindProperty]
        public List<MedicalHistory> myMedicalHistory { get; set; }
        private MedicalHistoryService _svc;
        public MedicalHistoryModel(MedicalHistoryService service)
        {
            _svc = service;
        }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("LoginUser") != null)
            {
                myMedicalHistory = _svc.GetMedicalHistoryOfUser(HttpContext.Session.GetString("LoginUser"));
                return Page();
            }
            return Redirect("~/");
        }
    }
}
