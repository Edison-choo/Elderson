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
    public class DeleteMedicalHistoryModel : PageModel
    {
        [BindProperty]
        public MedicalHistory myBooking { get; set; }
        private MedicalHistoryService _svc;

        public DeleteMedicalHistoryModel(MedicalHistoryService service)
        {
            _svc = service;
        }
        public IActionResult OnGet(string id)
        {
            if (HttpContext.Session.GetString("LoginUser") != null)
            {
                MedicalHistory deleteHistory = _svc.GetMedicalHistoryById(id);
                _svc.DeleteMedicalHistory(deleteHistory);
                return Redirect("~/Elderly/MedicalHistory");
            }
            return Redirect("~/");
        }
    }
}
