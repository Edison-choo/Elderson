using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Elderson.Pages.Shared
{
    public class DateAndTimeModel : PageModel
    {
        [BindProperty]
        public string myClinic { get; set; }
        [BindProperty]
        public string myDate { get; set; }
        [BindProperty]
        public string myTime { get; set; }
        public void OnGet(string clinic)
        {
            myClinic = clinic;
        }
    }
}
