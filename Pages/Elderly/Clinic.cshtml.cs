using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Elderson.Pages
{
    public class ClinicModel : PageModel
    {
        
        public void OnGet()
        {
        }
    }
    public class PaginationFilter
    {
        public int PageNumber { get; set; }
        public PaginationFilter()
        {
            this.PageNumber = 1;
        }
        public PaginationFilter(int pageNumber)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
        }
    }
}
