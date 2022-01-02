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
using Microsoft.Extensions.Logging;

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
        private readonly ILogger<OrganizationLoginModel> _logger;
        public OrganizationLoginModel(ILogger<OrganizationLoginModel> logger, UserService service)
        {
            _logger = logger;
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
                        _logger.LogInformation($"Organization User {user.Id} login successfullly.");
                        return RedirectToPage("Index");
                    }
                    else
                    {
                        _logger.LogInformation($"Organization User {user.Id} login unsuccessfullly. Password is incorrect.");
                        ErrorMsg = "Login Information is incorrect";
                        return Page();
                    }
                }
                else
                {
                    _logger.LogInformation($"Organization User login unsuccessfullly. Login information is incorrect.");
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
