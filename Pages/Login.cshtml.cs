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
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace Elderson.Pages.Users
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        [Required, RegularExpression(@"^\S+@\S+\.\S+$", ErrorMessage = "The email format is invalid")]
        public string Email { get; set; }

        [BindProperty]
        [Required]
        public string Password { get; set; }

        [BindProperty]
        public string ErrorMsg { get; set; }

        private UserService _svc;
        private readonly ILogger<LoginModel> _logger;
        private readonly INotyfService _notfy;

        public LoginModel(ILogger<LoginModel> logger, UserService service, INotyfService notyf)
        {
            _logger = logger;
            _svc = service;
            _notfy = notyf;
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

                    if (user.IsVerified == "1")
                    {
                        if (user.Password.Equals(Convert.ToBase64String(hashWithSalt)))
                        {
                            HttpContext.Session.SetString("LoginUser", user.Id);
                            HttpContext.Session.SetString("LoginUserName", user.Fullname);
                            HttpContext.Session.SetString("LoginUserType", user.UserType);

                            _logger.LogInformation("{actionStatus} User {userId} {userAction}.", "Successful", user.Id, "login");
                            _notfy.Success("Login Successfully");
                            return Redirect("/");
                        } else
                        {
                            _logger.LogInformation("{actionStatus} User {userId} {userAction}. Password is incorrect.", "Unsuccessful", user.Id, "login");
                            //ErrorMsg = "Login Information is incorrect";
                            _notfy.Error("Login Information is incorrect");
                            return Page();
                        }
                        
                    } else
                    {
                        _logger.LogInformation("{actionStatus} User {userId} {userAction}. This email is not verified.", "Unsuccessful", user.Id, "login");
                        _notfy.Error("This email is not verified");
                        return Page();
                    }
                } else
                {
                    _logger.LogInformation("{actionStatus} User {userId} {userAction}. Login information is incorrect.", "unsuccessful", user.Id, "login");
                    //ErrorMsg = "Login Information is incorrect";
                    _notfy.Error("Login Information is incorrect");
                    return Page();
                }
            } catch
            {
                return Page();
            }
        }
    }
}
