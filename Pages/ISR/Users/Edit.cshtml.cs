using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Elderson.Models;
using Elderson.Services;
using System.Globalization;

namespace Elderson.Pages.ISR.Users
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public User SelectedUser { get; set; }
        [BindProperty]
        public string Birthdate { get; set; }
        public User UpdatedUser { get; set; }
        private UserService _svc;
        public EditModel(UserService service)
        {
            _svc = service;
        }
        public void OnGet(string id)
        {
            SelectedUser = _svc.GetUserById(id);
            Birthdate = SelectedUser.Birthdate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        public IActionResult OnPost()
        {
            if (!(ModelState.IsValid))
            {
                return Page();
            }

            UpdatedUser = _svc.GetUserById(SelectedUser.Id);
            UpdatedUser.Fullname = SelectedUser.Fullname;
            UpdatedUser.Email = SelectedUser.Email;
            UpdatedUser.Phone = SelectedUser.Phone;
            UpdatedUser.Gender = SelectedUser.Gender;
            UpdatedUser.Birthdate = Convert.ToDateTime(Birthdate);
            Boolean valid = _svc.UpdateUser(UpdatedUser);
            if (valid)
            {
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
