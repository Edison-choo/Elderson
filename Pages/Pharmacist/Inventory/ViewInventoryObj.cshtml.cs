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
        public string supplier { get; set; }
        private InventoryService _svc;
        private SupplierService _sup_svc;
        public ViewInventoryObjModel(InventoryService service, SupplierService supplier_service)
        {
            _svc = service;
            _sup_svc = supplier_service;
        }
        public void OnGet(string id)
        {
            SelectedInventoryItem = _svc.GetInvMedicationById(id);
            SelectedMedication = _svc.GetMedicationById(id);

            string supplier = String.Format("<a href=\"/Pharmacist/MedSup/ViewSupplierObj?id={0}\">{1}</a>", SelectedMedication.MedSupplierID, SelectedMedication.MedSupplierAbb);
        }
    }
}
