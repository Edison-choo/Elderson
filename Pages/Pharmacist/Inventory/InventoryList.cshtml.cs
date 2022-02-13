using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elderson.Models;
using Elderson.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Elderson.Pages.Pharmacist.Inventory
{
    public class InventoryListModel : PageModel
    {
        [BindProperty]
        public List<MedInventory> allinventory { get; set;}
        [BindProperty]
        public List<Medication> allmedications { get; set; }
        private InventoryService _svc;
        public InventoryListModel(InventoryService service)
        {
            _svc = service;
        }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("LoginUser") != null)
            {
                if (HttpContext.Session.GetString("LoginUserType") == "Pharmacist")
                {
                    return Page();
                }
            }

            return Redirect("~/");
        }
    }
}
