using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public EditModel(ILogger<EditModel> logger, IncidentService service)
        {
            _logger = logger;
            _svc = service;
        }
        public void OnGet(string id)
        {
            SelectedIncident = _svc.GetIncidentById(id);
        }
        public IActionResult OnPost()
        {
            if (!(ModelState.IsValid))
            {
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
                _logger.LogInformation("Edit incident {incidentId} successfully.", SelectedIncident.Id);
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
