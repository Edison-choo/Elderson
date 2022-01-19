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
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Elderson.Pages.Users
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string ErrorMsg { get; set; }

        private UserService _svc;
        private readonly ILogger<LoginModel> _logger;
        public LoginModel(ILogger<LoginModel> logger, UserService service)
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
                var user = _svc.GetPatientUserByEmail(Email);
                if (user != null)
                {
                    SHA512 hashing = SHA512.Create();
                    string dbSalt = user.PasswordSalt;
                    string pwdWithSalt = Password + dbSalt;
                    byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));

                    if (user.Password.Equals(Convert.ToBase64String(hashWithSalt)))
                    {
                        HttpContext.Session.SetString("LoginUser", user.Id);
                        HttpContext.Session.SetString("LoginUserType", user.UserType);
                        
                        _logger.LogInformation("{actionStatus} User {userId} {userAction}.", "Successful", user.Id, "login");
                        return Redirect("/");
                    } else
                    {
                        _logger.LogInformation("{actionStatus} User {userId} {userAction}. Password is incorrect.", "Unsuccessful", user.Id, "login");
                        ErrorMsg = "Login Information is incorrect";
                        return Page();
                    }
                } else
                {
                    _logger.LogInformation("{actionStatus} User {userId} {userAction}. Login information is incorrect.", "unsuccessful", user.Id, "login");
                    ErrorMsg = "Login Information is incorrect";
                    return Page();
                }
            } catch
            {
                return Page();
            }
        }
    }
}
