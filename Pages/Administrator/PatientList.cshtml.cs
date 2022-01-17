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
    public class PatientListModel : PageModel
    {

        private UserService _svc;
        private readonly ILogger<PatientListModel> _logger;

        public PatientListModel(ILogger<PatientListModel> logger, UserService service)
        {
            _logger = logger;
            _svc = service;
        }

        public void OnGet()
        {

        }
    }
}