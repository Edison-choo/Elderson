using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elderson.Models;
using Elderson.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Elderson.Pages.Pharmacist.MedSup
{
    public class EditSupplierModel : PageModel
    {
        [BindProperty]
        public Supplier selectedSupplier { get; set; }

        public Supplier updatedSupplier { get; set; }


        private SupplierService _svc;
        private readonly ILogger<EditSupplierModel> _logger;
        public EditSupplierModel(ILogger<EditSupplierModel> logger, SupplierService service)
        {
            _svc = service;
            _logger = logger;
        }
        public IActionResult OnGet(string id)
        {
            if (HttpContext.Session.GetString("LoginUser") != null)
            {
                if (HttpContext.Session.GetString("LoginUserType") == "Pharmacist")
                {
                    selectedSupplier = _svc.GetSupplierbyId(id);
                    return Page();
                }
            }

            return Redirect("~/");
        }

        public IActionResult OnPost()
        {
            if (!(ModelState.IsValid))
            {
                return Page();
            }

            updatedSupplier = _svc.GetSupplierbyId(selectedSupplier.Id);
            if (_svc.GetSupplierbyId(selectedSupplier.Id) != null && selectedSupplier.Id != updatedSupplier.Id)
            {
                return Page();
            }
            updatedSupplier.SupplierName = selectedSupplier.SupplierName;
            updatedSupplier.SupplierEmail = selectedSupplier.SupplierEmail;
            updatedSupplier.SupplierPhone = selectedSupplier.SupplierPhone;
            updatedSupplier.SupplierWebsite = selectedSupplier.SupplierWebsite;
            updatedSupplier.SupplierAddress = selectedSupplier.SupplierAddress;
            updatedSupplier.SupplierAbbreviation = selectedSupplier.SupplierAbbreviation;
            Boolean Valid = _svc.UpdateSupplier(updatedSupplier);
            if (Valid)
            {
                return RedirectToPage("SupplierList");
            }

            return Page();
        }
    }
}