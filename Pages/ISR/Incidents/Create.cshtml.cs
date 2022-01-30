using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
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
        private readonly INotyfService _notfy;
        public CreateModel(ILogger<CreateModel> logger, IncidentService service, INotyfService notyf)
        {
            _svc = service;
            _logger = logger;
            _notfy = notyf;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!(ModelState.IsValid))
            {
                _logger.LogInformation("{actionStatus} {userAction}. Model state is invalid.", "Unsuccessful", "create incident");
                _notfy.Error("Error");
                return Page();
            }

            string guid = Guid.NewGuid().ToString();
            newIncident.Id = guid;
            newIncident.Timestamp = DateTime.Now;
            newIncident.UserId = "8d96eb8d-fdc9-45f9-bf11-bc69bf93ef39";
            _svc.AddIncident(newIncident);

            _logger.LogInformation("{actionStatus} {userAction} {incidentId} by User {userId}.", "Successful", "create incident", newIncident.Id, newIncident.UserId);
            _notfy.Success("Create Incident Successfully");
            return RedirectToPage("Index");
        }
    }
}
