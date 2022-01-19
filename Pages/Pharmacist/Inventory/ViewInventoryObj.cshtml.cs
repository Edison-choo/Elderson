using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elderson.Models;
using Elderson.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Elderson.Pages.Pharmacist.Inventory
{
    public class ViewInventoryObjModel : PageModel
    {

        [BindProperty]
        public MedInventory SelectedInventoryItem { get; set; }
        [BindProperty]
        public Medication SelectedMedication { get; set; }
        private InventoryService _svc;
        public ViewInventoryObjModel(InventoryService service)
        {
            _svc = service;
        }
        public void OnGet(string id)
        {
            SelectedInventoryItem = _svc.GetInvMedicationById(id);
            SelectedMedication = _svc.GetMedicationById(id);
        }
    }
}
