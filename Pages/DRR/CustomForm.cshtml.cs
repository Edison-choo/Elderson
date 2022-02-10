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
    public class CustomFormModel : PageModel
    {
        [BindProperty]
        public Form newForm { get; set; }
        [BindProperty]
        public string uuid { get; set; }

        private FormService _svc;

        private readonly ILogger<CustomFormModel> _logger;
        public CustomFormModel(ILogger<CustomFormModel> logger, FormService service)
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
                    uuid = Guid.NewGuid().ToString();
                    return Page();
                }
            }

            return Redirect("~/Elderly");
        }

        public IActionResult OnPost()
        {
            newForm.Id = uuid;
            newForm.DoctorId = HttpContext.Session.GetString("LoginUser");
            _svc.AddForm(newForm);

            return RedirectToPage("Form");
        }
    }
}
