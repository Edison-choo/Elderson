using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Elderson.Pages.DRR
{
    public class FormSelectModel : PageModel
    {
        [BindProperty]
        public string Doctor_Id { get; set; }
        [BindProperty]
        public string Booking_Id { get; set; }
        [BindProperty]
        public string New_Id { get; set; }
        public IActionResult OnGet(string id)
        {
            if (HttpContext.Session.GetString("LoginUser") != null)
            {
                if (HttpContext.Session.GetString("LoginUserType") == "Doctor")
                {
                    Doctor_Id = HttpContext.Session.GetString("LoginUser");
                    Booking_Id = id;
                    New_Id = Guid.NewGuid().ToString();
                    return Page();
                }
            }

            return Redirect("~/");
        }
    }
}
