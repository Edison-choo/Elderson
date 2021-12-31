using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Elderson.Pages
{
    public class SymptomsModel : PageModel
    {
        [BindProperty]
        public string mySymptoms { get; set; }
        [BindProperty]
        public DateTime mySymptomStart { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            HttpContext.Session.SetString("mySymptoms", mySymptoms);
            HttpContext.Session.SetString("mySymptomStart", mySymptomStart.ToString());
            return RedirectToPage("Cart");
        }
    }
}
