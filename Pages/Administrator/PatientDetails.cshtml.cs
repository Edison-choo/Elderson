﻿using System;
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
        [BindProperty]
        public string Id { get; set; }
        private AdministratorService _svc;
        private readonly ILogger<PatientDetailsModel> _logger;
        public PatientDetailsModel(ILogger<PatientDetailsModel> logger, AdministratorService service)
        {
            _svc = service;
            _logger = logger;
        }

        public void OnGet(string id)
        {
            Id = id;
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                string guid = Guid.NewGuid().ToString();
                NewEntry.Id = guid;
                NewEntry.PatientID = "8d96eb8d-fdc9-45f9-bf11-bc69bf93ef39";
                NewEntry.DateofVisit = DateTime.Now;
                _svc.AddEntry(NewEntry);
            }
            return Page();
        }
    }
}