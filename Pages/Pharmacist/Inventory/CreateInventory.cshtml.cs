using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elderson.Models;
using Elderson.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace Elderson.Pages.Pharmacist.Inventory
{
    public class CreateInventoryModel : PageModel
    {
        [BindProperty]
        public MedInventory newInventory { get; set; }
        [BindProperty]
        public Medication newMedication { get; set; }
        [BindProperty]
        public List<SelectListItem> abbreviations { get; set; }
        public List<Supplier> suppliers { get; set; }

        private InventoryService _svc;
        private SupplierService _supplier_svc;
        private readonly ILogger<CreateInventoryModel> _logger;
        public CreateInventoryModel(ILogger<CreateInventoryModel> logger, InventoryService service, SupplierService supplier_service)
        {
            _svc = service;
            _logger = logger;
            _supplier_svc = supplier_service;
        }
        public void OnGet()
        {
            suppliers = _supplier_svc.GetAllSuppliers();
            abbreviations = new List<SelectListItem>();
            foreach (var a in suppliers)
            {
                abbreviations.Add(new SelectListItem { Text = a.SupplierAbbreviation, Value = a.SupplierAbbreviation });
            }
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if (_svc.GetMedicationByName(newMedication.MedName) != null)
                {
                    return Page();
                }

                string guid = Guid.NewGuid().ToString();
                newInventory.Id = guid;
                newInventory.MedicationId = guid;
                newMedication.Id = guid;
                newMedication.MedSupplierID = _supplier_svc.GetSupplierbyAbbreviation(newMedication.MedSupplierAbb).Id;

                if ( _svc.AddMedicationToInventory(newInventory, newMedication))
                {

                    return RedirectToPage("InventoryList");
                }
                
            }


            return Page();
        
        }
    }
}
