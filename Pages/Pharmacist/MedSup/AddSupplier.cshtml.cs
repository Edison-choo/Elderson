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
    public class AddSupplierModel : PageModel
    {
        [BindProperty]
        public Supplier supplier { get; set; }

        private SupplierService _svc;
        private readonly ILogger<AddSupplierModel> _logger;
        public AddSupplierModel(ILogger<AddSupplierModel> logger, SupplierService service)
        {
            _svc = service;
            _logger = logger;
        }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("LoginUser") != null)
            {
                if (HttpContext.Session.GetString("LoginUserType") == "Pharmacist")
                {
                    return Page();
                }
            }

            return Redirect("~/");
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if (_svc.GetSupplierbyId(supplier.SupplierName) != null)
                {
                    return Page();
                }

                string guid = Guid.NewGuid().ToString();
                supplier.Id = guid;
                _svc.AddSupplier(supplier);
                return RedirectToPage("SupplierList");
            }


            return Page();

        }
    }
}
