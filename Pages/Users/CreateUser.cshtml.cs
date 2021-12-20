using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Elderson.Models;
using Elderson.Services;

namespace Elderson.Pages.Users
{
    public class CreateUserModel : PageModel
    {
        [BindProperty]
        public User newUser { get; set; }
        private UserService _svc;
        public CreateUserModel(UserService service)
        {
            _svc = service;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                //HttpContext.Session.SetString("SSName", MyEmployee.Name);
                //HttpContext.Session.SetString("SSDept", MyEmployee.Department);
                _svc.AddUser(newUser);
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
