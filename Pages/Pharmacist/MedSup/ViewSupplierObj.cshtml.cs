using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elderson.Models;
using Elderson.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Elderson.Pages.Pharmacist.MedSup
{
    public class ViewSupplierObjModel : PageModel
    {
        [BindProperty]
        public Supplier SelectedSupplier { get; set; }

        public List<string> medAbbreviations { get; set; }
        public List<Medication> Medications { get; set; }
        private SupplierService _svc;
        private InventoryService _med_svc;
        public ViewSupplierObjModel(SupplierService service, InventoryService inventory_service)
        {
            _svc = service;
            _med_svc = inventory_service;
        }
        public void OnGet(string id)
        {
            SelectedSupplier = _svc.GetSupplierbyId(id);
            Medications = _med_svc.GetAllMedications();
            foreach (var m in Medications)
            {
                if (SelectedSupplier.Id.Equals(m.MedSupplierID))
                {
                    
                }
            }
        }
    }
}
