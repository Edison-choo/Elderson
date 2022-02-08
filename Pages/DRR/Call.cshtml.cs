using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Elderson.Pages.DRR
{
    public class CallModel : PageModel
    {
        [BindProperty]
        public string myUserID { get; set; }
        [BindProperty]
        public string myCallID { get; set; }
        public void OnGet(string callId)
        {
            if (HttpContext.Session.GetString("LoginUser") != null)
            {
                myUserID = HttpContext.Session.GetString("LoginUser");
            }
            myCallID = callId;
        }
    }
}
