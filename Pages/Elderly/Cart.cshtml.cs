using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Elderson.Pages
{
    public class CartModel : PageModel
    {
        [BindProperty (SupportsGet =true)]
        public string myClinic { get; set; }
        [BindProperty]
        public string myDateTime { get; set; }
        public void OnGet()
        {
            if (HttpContext.Session.GetString("myClinic") != null)
            {
                myClinic = HttpContext.Session.GetString("myClinic");
                myDateTime = HttpContext.Session.GetString("myDateTime");
            }
        }
        public void OnPost()
        {
            //HttpContext.Session.Remove("myClinic");
            //HttpContext.Session.Remove("myDate");
            //HttpContext.Session.Remove("myTime");
        }
    }
}
