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
        private SupplierService _svc;
        public ViewSupplierObjModel(SupplierService service)
        {
            _svc = service;
        }
        public void OnGet(string id)
        {
            SelectedSupplier = _svc.GetSupplierbyId(id);
        }
    }
}
