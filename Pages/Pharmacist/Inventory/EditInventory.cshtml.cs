using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elderson.Models;
using Elderson.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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


        private InventoryService _svc;
        private readonly ILogger<EditInventoryModel> _logger;
        public EditInventoryModel(ILogger<EditInventoryModel> logger, InventoryService service)
        {
            _svc = service;
            _logger = logger;
        }
        public void OnGet(string id)
        {
            selectedInv = _svc.GetInvMedicationById(id);
            selectedMedication = _svc.GetMedicationById(id);
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
            Boolean invValid = _svc.UpdateInventory(updatedInv);
            Boolean medValid = _svc.UpdateMedication(updatedMedication);
            if (invValid && medValid)
            {
                return RedirectToPage("InventoryList");
            }

            return Page();
        }
    }
}

