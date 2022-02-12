using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Elderson.Pages.Elderly
{
    public class MedicalHistoryModel : PageModel
    {
        public void OnGet()
        {
            if (HttpContext.Session.GetString("LoginUser") != null)
            {

            }
            Redirect("~/");
        }
    }
}
