using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elderson.Models;
using Elderson.Services;
using Microsoft.AspNetCore.Http;
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
        public IActionResult OnGet(string id)
        {

            if (HttpContext.Session.GetString("LoginUser") != null)
            {
                if (HttpContext.Session.GetString("LoginUserType") == "Pharmacist")
                {
                    SelectedSupplier = _svc.GetSupplierbyId(id);
                    Medications = _med_svc.GetAllMedications();
                    medAbbreviations = new List<string>();
                    foreach (var m in Medications)
                    {
                        if (SelectedSupplier.Id.Equals(m.MedSupplierID))
                        {

                            medAbbreviations.Add(String.Format("<a href=\"/Pharmacist/Inventory/ViewInventoryObj?id={0}\">{1}</a>", m.Id, m.MedAbbreviation));

                        }
                        else
                        {
                            medAbbreviations = null;
                        }
                    }
                    return Page();
                }
            }

            return Redirect("~/");


        }
    }
}
