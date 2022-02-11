using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Elderson.Models;
using Elderson.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Elderson.Pages
{
    public class ProfileModel : PageModel
    {
        [BindProperty]
        public User user { get; set; }
        [BindProperty]
        public string Id { get; set; }
        
        [BindProperty]
        public ViewModel viewModel { get; set; }
        public class ViewModel
        {
            [Required]
            public string currentPwd { get; set; }
            [Required]
            public string newPwd { get; set; }

            [Required]
            [Compare(nameof(newPwd), ErrorMessage = "Passwords do not match.")]
            public string confirmPwd { get; set; }
        }
        [BindProperty]
        public string ErrorMsg { get; set; }
        [BindProperty]
        public Patient PatientRole { get; set; }
        [BindProperty]
        public string Birthdate { get; set; }
        public User UpdatedUser { get; set; }
        public Patient UpdatedPatient { get; set; }

        private UserService _svc;
        private readonly ILogger<ProfileModel> _logger;
        private readonly INotyfService _notfy;
        public ProfileModel(ILogger<ProfileModel> logger, UserService service, INotyfService notyf)
        {
            _logger = logger;
            _svc = service;
            _notfy = notyf;
        }
        public void OnGet()
        {
            //Id = HttpContext.Session.GetString("LoginUser");
            Id = "73d9b865-0275-48e1-9d1d-4d0a2d45e792";
            user = _svc.GetUserById(Id);
            PatientRole = _svc.GetPatientById(Id);
            Birthdate = user.Birthdate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        

        public IActionResult OnPostUpdatePassword()
        {
            if (IsValid(viewModel))
            {
                user = _svc.GetUserById(Id);
                SHA512 hashing = SHA512.Create();
                string checkPwdWithSalt = viewModel.currentPwd + user.PasswordSalt;
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

                    }

                }
                else
                {
                    _logger.LogInformation("{actionStatus} User {userId} {userAction}. Password is incorrect.", "Unsuccessful", user.Id, "update user");
                    _notfy.Error("Current password is incorrect");
                }
                return RedirectToPage("Profile");
            }
            else
            {
                return RedirectToPage("Profile");
            }
        }

        private bool IsValid<T>(T inputModel)
        {
            var property = this.GetType().GetProperties().Where(x => x.PropertyType == inputModel.GetType()).FirstOrDefault();

            var hasErros = ModelState.Values
                .Where(value => value.GetType().GetProperty("Key").GetValue(value).ToString().Contains(property.Name))
                .Any(value => value.Errors.Any());

            Console.WriteLine(ModelState.Values
                .Where(value => value.GetType().GetProperty("Key").GetValue(value).ToString().Contains(property.Name)).Select(value => value.Errors));

            return !hasErros;
        }
    }
}
