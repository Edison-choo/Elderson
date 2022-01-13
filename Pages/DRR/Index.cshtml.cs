using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elderson.Models;
using Elderson.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Elderson.Pages.DRR
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public List<User> allusers { get; set; }
        private UserService _svc;
        public IndexModel(UserService service)
        {
            _svc = service;
        }
        public void OnGet()
        {
            allusers = _svc.GetAllUsers();
        }
    }
}
