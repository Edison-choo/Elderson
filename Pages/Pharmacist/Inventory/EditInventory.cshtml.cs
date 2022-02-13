using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elderson.Models;
using Elderson.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace Elderson.Pages.Pharmacist.Inventory
{
    public class EditInventoryModel : PageModel
    {

        [BindProperty]
        public MedInventory selectedInv { get; set; }
        [BindProperty]
        public Medication selectedMedication { get; set; }

        public MedInventory updatedInv { get; set; }
        public Medication updatedMedication { get; set; }
        [BindProperty]
        public List<SelectListItem> abbreviations { get; set; }
        public List<Supplier> suppliers { get; set; }

        private InventoryService _svc;
        private SupplierService _supplier_svc;

        private readonly ILogger<EditInventoryModel> _logger;
        public EditInventoryModel(ILogger<EditInventoryModel> logger, InventoryService service, SupplierService supplier_service)
        {
            _svc = service;
            _logger = logger;
            _supplier_svc = supplier_service;
        }
        public IActionResult OnGet(string id)
        {

            if (HttpContext.Session.GetString("LoginUser") != null)
            {
                if (HttpContext.Session.GetString("LoginUserType") == "Pharmacist")
                {
                    selectedInv = _svc.GetInvMedicationById(id);
                    selectedMedication = _svc.GetMedicationById(id);
                    suppliers = _supplier_svc.GetAllSuppliers();
                    abbreviations = new List<SelectListItem>();
                    foreach (var a in suppliers)
                    {
                        abbreviations.Add(new SelectListItem { Text = a.SupplierAbbreviation, Value = a.SupplierAbbreviation });
                    }
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

            updatedInv = _svc.GetInvMedicationById(selectedInv.Id);
            updatedMedication = _svc.GetMedicationById(selectedMedication.Id);
            if (_svc.GetInvMedicationById(selectedInv.Id) != null && selectedInv.Id != updatedMedication.Id)
            {
                return Page();
            }
            updatedInv.CurrentAmt = selectedInv.CurrentAmt;
            updatedInv.MinimumAmt = selectedInv.MinimumAmt;
            updatedInv.Price = selectedInv.Price;
            updatedMedication.MedName = selectedMedication.MedName;
            updatedMedication.MedAbbreviation = selectedMedication.MedAbbreviation;
            updatedMedication.MedType = selectedMedication.MedType;
            updatedMedication.MedDescription = selectedMedication.MedDescription;
            updatedMedication.MedSupplierAbb = selectedMedication.MedSupplierAbb;
            updatedMedication.MedAllergyIngredients = selectedMedication.MedAllergyIngredients;
            Boolean invValid = _svc.UpdateInventory(updatedInv, updatedMedication);
            Boolean medValid = _svc.UpdateMedication(updatedMedication);
            if (invValid && medValid)
            {
                return RedirectToPage("InventoryList");
            }

            return Page();
        }
    }
}

