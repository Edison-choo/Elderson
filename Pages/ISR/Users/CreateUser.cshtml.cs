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
using Microsoft.Extensions.Logging;

namespace Elderson.Pages.Users
{
    public class CreateUserModel : PageModel
    {
        [BindProperty]
        public User newUser { get; set; }
        [BindProperty]
        public Administrator AdminRole { get; set; }
        [BindProperty]
        public Doctor DoctorRole { get; set; }
        [BindProperty]
        public Patient PatientRole { get; set; }
        [BindProperty]
        [Required]
        public string pwd { get; set; }
        [BindProperty]
        [Required]
        public string confirmPwd { get; set; }
        private UserService _svc;
        private readonly ILogger<CreateUserModel> _logger;
        public CreateUserModel(ILogger<CreateUserModel> logger, UserService service)
        {
            _svc = service;
            _logger = logger;
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
                newUser.Password = pwd;

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

                if (_svc.GetUserByEmail(newUser.Email) != null)
                {
                    _logger.LogInformation($"Create user {newUser.Id} unsuccessfullly. Email is already being used.");
                    return Page();
                }

                // Add user to Db
                var valid = false;
                while (!(valid))
                {
                    string guid = Guid.NewGuid().ToString();
                    newUser.Id = guid;
                    newUser.CreatedAt = DateTime.Now;
                    valid = _svc.AddUser(newUser);
                }

                string typeGuid = Guid.NewGuid().ToString();
                switch (newUser.UserType)
                {
                    case "Patient":
                        PatientRole.Id = typeGuid;
                        PatientRole.UserId = newUser.Id;
                        _svc.AddPatient(PatientRole);
                        break;
                    case "Doctor":
                        DoctorRole.Id = typeGuid;
                        DoctorRole.UserId = newUser.Id;
                        _svc.AddDoctor(DoctorRole);
                        break;
                    case "Administrator":
                        AdminRole.Id = typeGuid;
                        AdminRole.UserId = newUser.Id;
                        _svc.AddAdministrator(AdminRole);
                        break;
                    case "Pharmacist":
                        break;
                    case "ITSupport":
                        break;
                }

                _logger.LogInformation($"Create user {newUser.Id} successfullly.");
                return RedirectToPage("Index");
            }
            //var error = ModelState.Values.SelectMany(v => v.Errors.Select(b => b.Exception));
            //foreach (var i in error)
            //{
            //    _logger.LogInformation(i);

            //}
            return Page();
        }
    }
}
