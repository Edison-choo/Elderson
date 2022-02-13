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
        [BindProperty]
        public string ErrMsg { get; set; }

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
                    newForm = new Form();
                    uuid = Guid.NewGuid().ToString();
                    ErrMsg = "";
                    return Page();
                }
            }

            return Redirect("~/");
        }

        public IActionResult OnPost()
        {
            if (newForm.TemplateName == null)
            {
                ErrMsg = "Please enter a template name";
                return Page();
            }
            newForm.Id = uuid;
            newForm.DoctorId = HttpContext.Session.GetString("LoginUser");
            _svc.AddForm(newForm);

            return RedirectToPage("Form");
        }
    }
}
