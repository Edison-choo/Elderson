using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Elderson.Pages.DRR
{
    public class ConsultationModel : PageModel
    {
        public string doctorid { get; set; }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("LoginUser") != null)
            {
                if (HttpContext.Session.GetString("LoginUserType") == "Doctor")
                {
                    doctorid = HttpContext.Session.GetString("LoginUser");
                    return Page();
                }
            }

            return Redirect("~/");
        }
    }
}
