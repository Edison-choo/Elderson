using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elderson.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Elderson.Services;

namespace Elderson
{
    public class PatientDetailsModel : PageModel
    {

        [BindProperty]
        public PatientDetails NewEntry { get; set; }
        private PatientDetails _svc;
        private readonly ILogger<PatientDetailsModel> _logger;
        public PatientDetailsModel(ILogger<PatientDetailsModel> logger, PatientDetails service)
        {
            _svc = service;
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}