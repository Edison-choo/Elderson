using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Elderson.Models;
using Elderson.Services;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Elderson.Pages.ISR.Users
{
    public class ChangePwdModel : PageModel
    {
        public User user { get; set; }
        [BindProperty]
        public string Id { get; set; }
        [BindProperty]
        [Required]
        public string currentPwd { get; set; }
        [BindProperty]
        [Required]
        public string newPwd { get; set; }
        [BindProperty]
        [Required]
        public string confirmPwd { get; set; }
        [BindProperty]
        public string ErrorMsg { get; set; }

        private UserService _svc;
        private readonly ILogger<ChangePwdModel> _logger;
        public ChangePwdModel(ILogger<ChangePwdModel> logger, UserService service)
        {
            _logger = logger;
            _svc = service;
        }
        public void OnGet(string id)
        {
            Id = id;
            user = _svc.GetUserById(id);
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                user = _svc.GetUserById(Id);
                //HttpContext.Session.SetString("SSName", MyEmployee.Name);
                //HttpContext.Session.SetString("SSDept", MyEmployee.Department);

                // Hash Password
                SHA512 hashing = SHA512.Create();
                string checkPwdWithSalt = currentPwd + user.PasswordSalt;
                byte[] checkHashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(checkPwdWithSalt));

                if (user.Password.Equals(Convert.ToBase64String(checkHashWithSalt)))
                {
                    if (newPwd.Equals(confirmPwd))
                    {
                        string pwdWithSalt = newPwd + user.PasswordSalt;
                        byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));
                        user.Password = Convert.ToBase64String(hashWithSalt);

                        // Update user to Db
                        Boolean valid = _svc.UpdateUser(user);
                        if (valid)
                        {
                            _logger.LogInformation("Update user {userId} password successfully.", user.Id);
                            return RedirectToPage("Index");
                        }
                    }
                    else
                    {
                        _logger.LogInformation("Update user {userId} password unsuccessfully. The passwords do not match.", user.Id);
                        ErrorMsg = "New password does not match";
                    }

                }
                else
                {
                    _logger.LogInformation("Update user {userId} password successfully. Password is incorrect.", user.Id);
                    ErrorMsg = "Current password is incorrect";
                }
                return Page();
            }
            else
            {
                return Page();
            }
        }
    }
}
