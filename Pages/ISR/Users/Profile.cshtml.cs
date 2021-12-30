using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Elderson.Models;
using Elderson.Services;

namespace Elderson.Pages.ISR.Users
{
    public class ProfileModel : PageModel
    {
        [BindProperty]
        public User SelectedUser { get; set; }
        private UserService _svc;
        public ProfileModel(UserService service)
        {
            _svc = service;
        }
        public void OnGet(string id)
        {
            SelectedUser = _svc.GetUserById(id);
        }
    }
}
