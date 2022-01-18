using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elderson.Models;
using Elderson.Pages.Users;
using Elderson.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Elderson.Pages.ISR.Incidents
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Incident newIncident { get; set; }
        private IncidentService _svc;
        private readonly ILogger<CreateModel> _logger;
        public CreateModel(ILogger<CreateModel> logger, IncidentService service)
        {
            _svc = service;
            _logger = logger;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!(ModelState.IsValid))
            {
                _logger.LogInformation("{actionStatus} {userAction}. Model state is invalid.", "Unsuccessful", "create incident");
                return Page();
            }

            string guid = Guid.NewGuid().ToString();
            newIncident.Id = guid;
            newIncident.Timestamp = DateTime.Now;
            newIncident.UserId = "3af4d0f9-0c8a-46b3-8465-b90cbf0090c9";
            _svc.AddIncident(newIncident);

            _logger.LogInformation("{actionStatus} {userAction} {incidentId} by User {userId}.", "Successful", "create incident", newIncident.Id, newIncident.UserId);
            return RedirectToPage("Index");
        }
    }
}
