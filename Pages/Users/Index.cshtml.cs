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
    public class UserManagementModel : PageModel
    {
        [BindProperty]
        public List<User> allusers { get; set; }
        private UserService _svc;
        public UserManagementModel(UserService service)
        {
            _svc = service;
        }
        public void OnGet()
        {
        }
    }
}
