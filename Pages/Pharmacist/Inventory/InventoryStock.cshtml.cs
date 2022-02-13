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
    public class InventoryStockModel : PageModel
    {

        [BindProperty]
        public List<Medication> medication { get; set; }
        [BindProperty]
        public List<MedInventory> inventory { get; set; }
        [BindProperty]
        public MedInventory updatedInventory { get; set; }
        [BindProperty]
        public MedInventory selectedInventory { get; set; }
        public Medication updateMedication { get; set; }
        private InventoryService _svc;
        private readonly ILogger<InventoryStockModel> _logger;
        public InventoryStockModel(ILogger<InventoryStockModel> logger, InventoryService service)
        {
            _svc = service;
            _logger = logger;
        }


        public void OnGet()
        {
            medication = _svc.GetAllMedications();
            inventory = _svc.GetAllInventories();
        }

        //public IActionResult OnPost()
        //{
        //    inventory = _svc.GetAllInventories();
        //    medication = _svc.GetAllMedications();
        //    if (!(ModelState.IsValid))
        //    {
        //        var errors = ModelState.Values.SelectMany(v => v.Errors);
        //        foreach ( var error in errors)
        //        {
        //            Console.WriteLine(error);
        //        }
        //        return Page();
        //    }
        //    try
        //    {
        //        foreach (var m in inventory)
        //        {
        //            updatedInventory = new MedInventory();
        //            updateMedication = new Medication();
        //            updatedInventory.CurrentAmt = selectedInventory.CurrentAmt;
        //            updatedInventory.MinimumAmt = m.MinimumAmt;
        //            updatedInventory.Price = m.Price;
        //            updatedInventory.MedicationId = m.Id;
        //            updateMedication = _svc.GetMedicationById(m.MedicationId);
        //            bool invValid = _svc.UpdateInventory(updatedInventory, updateMedication);
        //            if (invValid)
        //            {
        //                return RedirectToPage("InventoryList");
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //    return Page();

        //}
    }
}
