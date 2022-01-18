using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elderson.Models;
using Elderson.Services;
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
        public Schedule schedule { get; set; }

        private ScheduleService _svc;

        private readonly ILogger<CreateScheduleModel> _logger;
        public CreateScheduleModel(ILogger<CreateScheduleModel> logger, ScheduleService service)
        {
            _svc = service;
            _logger = logger;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            
            var valid = false;
            while (!(valid))
            {
                newschedule.Id = Guid.NewGuid().ToString();
                newschedule.DoctorId = "2";
                newschedule.StartDateTime = datetime;
                newschedule.Availability = "A";
                valid = _svc.AddSchedule(newschedule);
            }
            return RedirectToPage("Index");
        }
    }
}
