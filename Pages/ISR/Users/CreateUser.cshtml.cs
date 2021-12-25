using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Elderson.Models;
using Elderson.Services;
using System.Security.Cryptography;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Elderson.Pages.Users
{
    public class CreateUserModel : PageModel
    {
        [BindProperty]
        public User newUser { get; set; }
        [BindProperty]
        //[Required, Compare(nameof(CreateUserModel.newUser.Password))]
        public string confirmPwd { get; set; }
        private UserService _svc;
        public CreateUserModel(UserService service)
        {
            _svc = service;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                //HttpContext.Session.SetString("SSName", MyEmployee.Name);
                //HttpContext.Session.SetString("SSDept", MyEmployee.Department);

                // Hash Password
                RandomNumberGenerator rng = RandomNumberGenerator.Create();
                byte[] saltByte = new byte[8];
                rng.GetBytes(saltByte);
                var salt = Convert.ToBase64String(saltByte);
                SHA512 hashing = SHA512.Create();
                string pwdWithSalt = newUser.Password + salt;
                byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));
                newUser.Password = Convert.ToBase64String(hashWithSalt);
                newUser.PasswordSalt = salt;

                // Add user to Db
                var valid = false;
                while (!(valid))
                {
                    string guid = Guid.NewGuid().ToString();
                    newUser.Id = guid;
                    newUser.CreatedAt = DateTime.Now;
                    valid = _svc.AddUser(newUser);
                }
                
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
