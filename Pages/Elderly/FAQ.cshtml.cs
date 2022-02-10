using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elderson.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Elderson.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace Elderson
{
    public class FAQModel : PageModel
    {
        [BindProperty]
        public FAQ newQuery { get; set; }
        private FAQService _svc;
        private readonly ILogger<FAQModel> _logger;

        public FAQModel(ILogger<FAQModel> logger, FAQService service)
        {
            _svc = service;
            _logger = logger;
        }
        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if (HttpContext.Session.GetString("LoginUser") != null)
                {
                    string guid = Guid.NewGuid().ToString();
                    newQuery.Id = guid;
                    newQuery.UserId = HttpContext.Session.GetString("LoginUser");
                    _svc.SubmitQuery(newQuery);
                }
            }
            return Page();
        }
    }
}