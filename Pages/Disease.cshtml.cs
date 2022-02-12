using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Elderson.Pages
{
    public class DiseaseModel : PageModel
    {
        [BindProperty]
        public string searchString { get; set; }
        public void OnGet(string search)
        {
            searchString = search;
        }
    }
}
