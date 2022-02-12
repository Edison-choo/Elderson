using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Elderson.Models;
using Elderson.Services;
using Microsoft.AspNetCore.Http;

namespace Elderson.Pages.Users
{
    public class UserManagementModel : PageModel
    {
        [BindProperty]
        public List<User> allusers { get; set; }
        private UserService _svc;
        public UserManagementModel(UserService service)
        {
            _svc = service;
        }
        public IActionResult OnGet()
        {
            Console.WriteLine(HttpContext.Session.GetString("LoginUser"));
            if (HttpContext.Session.GetString("LoginUser") != null)
            {
                if (HttpContext.Session.GetString("LoginUserType") == "ITSupport")
                {
                    allusers = _svc.GetAllUsers();
                    return Page();
                }
            }
            return Redirect("~/");
            //return Page();
        }
    }
}
