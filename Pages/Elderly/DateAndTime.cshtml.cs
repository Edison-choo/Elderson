using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Elderson.Pages.Shared
{
    public class DateAndTimeModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string myClinic { get; set; }
        [BindProperty]
        public DateTime myDateTime { get; set; }
        [BindProperty]
        public string myTime { get; set; }
        public void OnGet(string clinic)
        {
            myClinic = clinic;
        }
        public IActionResult OnPost()
        {
            HttpContext.Session.SetString("myClinic", myClinic);
            HttpContext.Session.SetString("myDateTime", myDateTime.ToString());
            return RedirectToPage("Symptoms");
        }
    }
}
