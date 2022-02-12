using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elderson.Models;
using Elderson.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Elderson.Pages.ISR.Incidents
{
    public class DetailModel : PageModel
    {
        [BindProperty]
        public Incident SelectedIncident { get; set; }
        [BindProperty]
        public User SelectedUser { get; set; }
        private IncidentService _svc;
        private UserService _u_svc;
        public DetailModel(IncidentService service, UserService userService)
        {
            _svc = service;
            _u_svc = userService;
        }
        public IActionResult OnGet(string id)
        {
            if (HttpContext.Session.GetString("LoginUser") != null)
            {
                if (HttpContext.Session.GetString("LoginUserType") == "ITSupport")
                {

                    SelectedIncident = _svc.GetIncidentById(id);
                    SelectedUser = _u_svc.GetUserById(SelectedIncident.UserId);
                    return Page();
                }
            }

            return Redirect("~/");
            
        }
    }
}
