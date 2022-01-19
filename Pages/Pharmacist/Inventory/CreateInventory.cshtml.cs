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
    public class CreateInventoryModel : PageModel
    {
        [BindProperty]
        public MedInventory newInventory { get; set; }
        [BindProperty]
        public Medication newMedication { get; set; }

        private InventoryService _svc;
        private readonly ILogger<CreateInventoryModel> _logger;
        public CreateInventoryModel(ILogger<CreateInventoryModel> logger, InventoryService service)
        {
            _svc = service;
            _logger = logger;
        }
        public void OnGet()
        {
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
                _svc.AddMedication(newMedication);
                _svc.AddMedicationToInventory(newInventory);
                return RedirectToPage("InventoryList");
            }


            return Page();
        
        }
    }
}
