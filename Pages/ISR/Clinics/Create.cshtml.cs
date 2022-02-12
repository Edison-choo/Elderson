using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Elderson.Models;
using Elderson.Pages.Users;
using Elderson.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Elderson.Pages.ISR.Clinics
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Clinic newClinic { get; set; }
        private UserService _svc;
        private readonly ILogger<CreateModel> _logger;
        private readonly INotyfService _notfy;
        public CreateModel(ILogger<CreateModel> logger, UserService service, INotyfService notyf)
        {
            _svc = service;
            _logger = logger;
            _notfy = notyf;
        }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("LoginUser") != null)
            {
                if (HttpContext.Session.GetString("LoginUserType") == "ITSupport")
                {

                    return Page();
                }
            }

            return Redirect("~/");
        }

        public IActionResult OnPost()
        {
            if (!(ModelState.IsValid))
            {
                _logger.LogInformation("{actionStatus} {userAction}. Model state is invalid.", "Unsuccessful", "create clinic");
                _notfy.Error("Error");
                return Page();
            }

            string guid = Guid.NewGuid().ToString();
            newClinic.Id = guid;
            _svc.AddClinic(newClinic);

            //_logger.LogInformation("{actionStatus} {userAction} {incidentId} by User {userId}.", "Successful", "create clinic", newClinic.Id, HttpContext.Session.GetString("LoginUser"));
            _notfy.Success("Create Incident Successfully");
            return RedirectToPage("Index");
        }
    }
}
