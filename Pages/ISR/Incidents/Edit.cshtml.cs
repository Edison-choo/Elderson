using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Elderson.Models;
using Elderson.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Elderson.Pages.ISR.Incidents
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Incident SelectedIncident { get; set; }
        public Incident UpdatedIncident { get; set; }
        private IncidentService _svc;
        private readonly ILogger<EditModel> _logger;
        private readonly INotyfService _notfy;
        public EditModel(ILogger<EditModel> logger, IncidentService service, INotyfService notyf)
        {
            _logger = logger;
            _svc = service;
            _notfy = notyf;
        }
        public void OnGet(string id)
        {
            SelectedIncident = _svc.GetIncidentById(id);
        }
        public IActionResult OnPost()
        {
            if (!(ModelState.IsValid))
            {
                _logger.LogInformation("{actionStatus} {userAction}.", "Unsuccessful", "edit incident");
                _notfy.Error("Error");
                return Page();
            }

            UpdatedIncident = _svc.GetIncidentById(SelectedIncident.Id);
            UpdatedIncident.Title = SelectedIncident.Title;
            UpdatedIncident.Description = SelectedIncident.Description;
            UpdatedIncident.Reason = SelectedIncident.Reason;
            UpdatedIncident.Recommendation = SelectedIncident.Recommendation;
            Boolean valid = _svc.UpdateIncident(UpdatedIncident);

            if (valid)
            {
                _logger.LogInformation("{actionStatus} {userAction} {incidentId} of User {userId}.", "Successful", "edit incident", SelectedIncident.Id, SelectedIncident.UserId);
                _notfy.Success("Edit Incident Successfully");
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
