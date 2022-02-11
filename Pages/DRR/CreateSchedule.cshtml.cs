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
    public class CreateScheduleModel : PageModel
    {
        [BindProperty]
        public Schedule newschedule { get; set; }
        public DateTime datetime { get; set; }
        [BindProperty]
        public int Date { get; set; }
        [BindProperty]
        public int Month { get; set; }
        [BindProperty]
        public int Year { get; set; }
        [BindProperty]
        public int hour { get; set; }
        [BindProperty]
        public int min { get; set; }
        [BindProperty]
        public bool check { get; set; }
        public DateTime StartDateTime { get; set; }

        public Schedule schedule { get; set; }
        

        private ScheduleService _svc;

        private readonly ILogger<CreateScheduleModel> _logger;
        public CreateScheduleModel(ILogger<CreateScheduleModel> logger, ScheduleService service)
        {
            _svc = service;
            _logger = logger;
        }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("LoginUser") != null)
            {
                if (HttpContext.Session.GetString("LoginUserType") == "Doctor")
                {
                    check = false;
                    return Page();
                }
            }

            return Redirect("~/Elderly");
        }

        public IActionResult OnPost()
        {
            
            var valid = false;
            while (!(valid))
            {
                newschedule.Id = Guid.NewGuid().ToString();
                newschedule.DoctorId = HttpContext.Session.GetString("LoginUser");
                StartDateTime = new DateTime(Year, Month, Date, hour, min, 00);
                if (_svc.ScheduleExistByDateTime(HttpContext.Session.GetString("LoginUser"), StartDateTime))
                {
                    check = true;
                    return Page();
                }
                newschedule.StartDateTime = StartDateTime;
                newschedule.Availability = "A";
                valid = _svc.AddSchedule(newschedule);
            }
            return RedirectToPage("Index");
        }
    }
}
