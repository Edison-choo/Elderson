using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Elderson.Services;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Elderson.Pages
{
    public class OrganizationLoginModel : PageModel
    {
        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string ErrorMsg { get; set; }

        private UserService _svc;
        public OrganizationLoginModel(UserService service)
        {
            _svc = service;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            try
            {
                var user = _svc.GetOtherUserByEmail(Email);
                if (user != null)
                {
                    SHA512 hashing = SHA512.Create();
                    string dbSalt = user.PasswordSalt;
                    string pwdWithSalt = Password + dbSalt;
                    byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));

                    if (user.Password.Equals(Convert.ToBase64String(hashWithSalt)))
                    {
                        HttpContext.Session.SetString("LoginUser", user.Fullname);
                        HttpContext.Session.SetString("LoginUserType", user.UserType);
                        return RedirectToPage("Index");
                    }
                    else
                    {
                        ErrorMsg = "Login Information is incorrect";
                        return Page();
                    }
                }
                else
                {
                    ErrorMsg = "Login Information is incorrect";
                    return Page();
                }
            }
            catch
            {
                return Page();
            }
        }
    }
}
