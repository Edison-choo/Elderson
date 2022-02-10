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
    public class EditFormModel : PageModel
    {
        private FormService _svc;
        private readonly ILogger<EditFormModel> _logger;

        public EditFormModel(ILogger<EditFormModel> logger, FormService service)
        {
            _logger = logger;
            _svc = service;
        }
        [BindProperty]
        public Form editForm { get; set; }
        public IActionResult OnGet(string id)
        {
            if (HttpContext.Session.GetString("LoginUser") != null)
            {
                if (HttpContext.Session.GetString("LoginUserType") == "Doctor")
                {
                    editForm = _svc.GetFormById(id);
                    editForm.Id = id;
                    return Page();
                }
            }

            return Redirect("~/Elderly");
        }

        public IActionResult OnPost()
        {
            if (_svc.updateForm(editForm)){
                return RedirectToPage("Form");
            }
            else
            {
                return Page();
            }
        }
    }
}
