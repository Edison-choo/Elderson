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
using AspNetCoreHero.ToastNotification.Abstractions;

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
        private readonly INotyfService _notfy;
        public OrganizationLoginModel(ILogger<OrganizationLoginModel> logger, UserService service, INotyfService notyf)
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
                var user = _svc.GetOtherUserByEmail(Email);
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
                        _logger.LogInformation("{actionStatus} {userType} User {userId} {userAction}.", "Successful", user.UserType, user.Id, "login");

                        _notfy.Success("Login Successfully");

                        if (user.UserType == "Doctor")
                        {
                            return Redirect("~/DRR");
                        } else if (user.UserType == "ITSupport")
                        {
                            return Redirect("~/ISR/Users");
                        } else if (user.UserType == "Administrator")
                        {
                            return Redirect("~/Administrator/AdminHomePage");
                        } else if (user.UserType == "Pharmacist")
                        {
                            return Redirect("~/Pharmacist");
                        } else
                        {
                            return Redirect("~/");
                        }
                    }
                    else
                    {
                        _logger.LogInformation("{actionStatus} Organization User {userId} {userAction}. Password is incorrect.", "Unsuccessful", user.Id, "login");
                        //ErrorMsg = "Login Information is incorrect";
                        _notfy.Error("Login Information is incorrect");
                        return Page();
                    }
                }
                else
                {
                    _logger.LogInformation("{actionStatus} Organization User {userAction}. Login information is incorrect.", "Unsuccessful", "login");
                    //ErrorMsg = "Login Information is incorrect";
                    _notfy.Error("Login Information is incorrect");
                    return Page();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Page();
            }
        }
    }
}
