using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Elderson.Pages.ISR.Incidents
{
    public class IndexModel : PageModel
    {
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("LoginUser") != null)
            {
                if (HttpContext.Session.GetString("LoginUserType") == "ITSupport")
                {
                    return Page();
                }
            }

            return Redirect("~/");
        }
    }
}
