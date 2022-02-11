using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Elderson.Models;
using Elderson.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Elderson.Pages.ISR.Clinics
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Clinic SelectedClinic { get; set; }
        [BindProperty]
        public string Select { get; set; }
        public Clinic UpdatedClinic { get; set; }
        private UserService _svc;
        private readonly ILogger<EditModel> _logger;
        private readonly INotyfService _notfy;
        public EditModel(ILogger<EditModel> logger, UserService service, INotyfService notyf)
        {
            _logger = logger;
            _svc = service;
            _notfy = notyf;
        }
        public void OnGet(string id)
        {
            SelectedClinic = _svc.GetClinicById(id);
        }
        public IActionResult OnPost()
        {
            if (!(ModelState.IsValid))
            {
                _logger.LogInformation("{actionStatus} {userAction}.", "Unsuccessful", "edit clinic");
                _notfy.Error("Error");
                return Page();
            }

            UpdatedClinic = _svc.GetClinicById(SelectedClinic.Id);
            UpdatedClinic.Name = SelectedClinic.Name;
            UpdatedClinic.Address = SelectedClinic.Address;
            UpdatedClinic.OpeningHours = SelectedClinic.OpeningHours;
            UpdatedClinic.ClosingHours = SelectedClinic.ClosingHours;
            UpdatedClinic.CountryCode = SelectedClinic.CountryCode;
            UpdatedClinic.Phone = SelectedClinic.Phone;
            Boolean valid = _svc.UpdateClinic(UpdatedClinic);

            if (valid)
            {
                _logger.LogInformation("{actionStatus} {userAction} {clinicId} by {userId}.", "Successful", "edit clinic", SelectedClinic.Id, HttpContext.Session.GetString("LoginUser"));
                _notfy.Success("Edit Clinic Successfully");
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
