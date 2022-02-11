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
        private UserService _usrsvc;
        private readonly ILogger<FAQModel> _logger;

        public FAQModel(ILogger<FAQModel> logger, FAQService service, UserService userService)
        {
            _svc = service;
            _logger = logger;
            _usrsvc = userService;
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
                    var userID = HttpContext.Session.GetString("LoginUser");
                    newQuery.FullName = _usrsvc.GetUserById(userID).Fullname;
                    _svc.SubmitQuery(newQuery);
                    return Redirect("/Elderly/FAQ");
                }
                else
                {

                }
            }
            return Redirect("/Elderly/FAQ");
        }
    }
}