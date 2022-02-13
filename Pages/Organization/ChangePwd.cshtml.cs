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
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Elderson.Pages.Organization
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
        public ViewModel viewModel { get; set; }
        public class ViewModel
        {
            [Required, RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^0-9a-zA-Z]).{8,}$", ErrorMessage = "Password must meet requirements")]
            public string newPwd { get; set; }

            [Required]
            [Compare(nameof(newPwd), ErrorMessage = "Passwords do not match.")]
            public string confirmPwd { get; set; }
        }
        [BindProperty]
        public string ErrorMsg { get; set; }

        private UserService _svc;
        private readonly ILogger<ChangePwdModel> _logger;
        private readonly INotyfService _notfy;
        public ChangePwdModel(ILogger<ChangePwdModel> logger, UserService service, INotyfService notyf)
        {
            _logger = logger;
            _svc = service;
            _notfy = notyf;
        }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("LoginUser") != null)
            {
                if (HttpContext.Session.GetString("LoginUserType") != "Patient")
                {

                    var id = HttpContext.Session.GetString("LoginUser");
                    //id = "8bbe4522-ff24-49f9-bb94-6eff25e16f84";
                    Id = id;
                    user = _svc.GetUserById(id);
                    return Page();
                }
            }

            return Redirect("~/");
            
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
                    if (viewModel.newPwd.Equals(viewModel.confirmPwd))
                    {
                        string pwdWithSalt = viewModel.newPwd + user.PasswordSalt;
                        byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));
                        user.Password = Convert.ToBase64String(hashWithSalt);

                        // Update user to Db
                        Boolean valid = _svc.UpdateUser(user);
                        if (valid)
                        {
                            _logger.LogInformation("{actionStatus} User {userId} {userAction}.", "Successful", user.Id, "update user");
                            _notfy.Success("Change password Successfully");
                            return RedirectToPage("Profile");
                        }
                    }
                    else
                    {
                        _logger.LogInformation("{actionStatus} User {userId} {userAction}. The passwords do not match.", "Unsuccessful", user.Id, "update user");
                        _notfy.Error("New passwords do not match");
                        //ErrorMsg = "New password does not match";
                    }

                }
                else
                {
                    _logger.LogInformation("{actionStatus} User {userId} {userAction}. Password is incorrect.", "Unsuccessful", user.Id, "update user");
                    _notfy.Error("Current password is incorrect");
                    //ErrorMsg = "Current password is incorrect";
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
