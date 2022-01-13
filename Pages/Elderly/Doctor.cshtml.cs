using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elderson.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Elderson.Pages.Elderly
{
    public class DoctorModel : PageModel
    {
        [BindProperty]
        public string myClinic { get; set; }
        [BindProperty]
        public string myDoctor { get; set; }

        public void OnGet(string clinic)
        {
            myClinic = clinic;
        }
    }
}
