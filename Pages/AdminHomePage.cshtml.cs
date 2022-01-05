using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elderson.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Elderson
{
    public class AdminHomePageModel : PageModel
    {

        private UserService _svc;
        private readonly ILogger<AdminHomePageModel> _logger;

        public AdminHomePageModel(ILogger<AdminHomePageModel> logger, UserService service)
        {
            _logger = logger;
            _svc = service;
        }
        public void OnGet()
        {

        }
    }
}