using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elderson.Models;
using Elderson.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Elderson.Pages.DRR
{
    public class ViewDetailsModel : PageModel
    {
        [BindProperty]
        public Booking bkdetail { get; set; }
        [BindProperty]
        public User usrdetail { get; set; }
        [BindProperty]
        public Patient patdetail { get; set; }

        private ScheduleService _svc;
        private readonly ILogger<ViewDetailsModel> _logger;
        public ViewDetailsModel(ILogger<ViewDetailsModel> logger, ScheduleService service)
        {
            _svc = service;
            _logger = logger;
        }
        public IActionResult OnGet(string id)
        {
            if (HttpContext.Session.GetString("LoginUser") != null)
            {
                if (HttpContext.Session.GetString("LoginUserType") == "Doctor")
                {
                    bkdetail = _svc.GetPatientDetails(id);
                    usrdetail = _svc.GetUserDetails(bkdetail.PatientID);
                    patdetail = _svc.GetPatient(bkdetail.PatientID);
                    return Page();
                }
            }

            return Redirect("~/Elderly");
        }
    }
}
