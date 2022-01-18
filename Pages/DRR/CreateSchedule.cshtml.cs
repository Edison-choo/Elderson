using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elderson.Models;
using Elderson.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Elderson.Pages.DRR
{
    public class CreateScheduleModel : PageModel
    {
        [BindProperty]
        public DateTime datetime { get; set; }
        public Schedule schedule { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {

            return RedirectToPage("Index");
        }
    }
}
