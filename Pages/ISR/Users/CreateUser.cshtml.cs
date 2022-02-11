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
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Elderson.Pages.Users
{
    public class CreateUserModel : PageModel
    {
        [BindProperty]
        public User newUser { get; set; }
        [BindProperty]
        public Elderson.Models.Administrator AdminRole { get; set; }
        [BindProperty]
        public Doctor DoctorRole { get; set; }
        [BindProperty]
        public Doctor Photo { get; set; }
        [BindProperty]
        public Patient PatientRole { get; set; }
        [BindProperty]
        public Clinic newClinic { get; set; }
        public List<Clinic> allClinic { get; set; }
        [BindProperty]
        public List<SelectListItem> clinics { get; set; }
        //[BindProperty]
        //[Required]
        //public string pwd { get; set; }
        //[BindProperty]
        //[Required, Compare(nameof(pwd), ErrorMessage ="Passwords do not match")]
        //public string confirmPwd { get; set; }
        [BindProperty]
        public ViewModel viewModel { get; set; }
        public class ViewModel
        {
            [Required]
            public string pwd { get; set; }

            [Required]
            [Compare(nameof(pwd), ErrorMessage = "Passwords do not match.")]
            public string confirmPwd { get; set; }
        }
        private UserService _svc;
        private readonly ILogger<CreateUserModel> _logger;
        private readonly INotyfService _notfy;
        private readonly IWebHostEnvironment webHostEnvironment;
        public CreateUserModel(ILogger<CreateUserModel> logger, UserService service, INotyfService notyf, IWebHostEnvironment hostEnvironment)
        {
            _svc = service;
            _logger = logger;
            _notfy = notyf;
            webHostEnvironment = hostEnvironment;
        }
        public void OnGet()
        {
            allClinic = _svc.GetAllClinic();
            clinics = new List<SelectListItem>();
            foreach (var i in allClinic)
            {
                clinics.Add(new SelectListItem { Text = i.Name, Value = i.Id });
            }
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                //HttpContext.Session.SetString("SSName", MyEmployee.Name);
                //HttpContext.Session.SetString("SSDept", MyEmployee.Department);
                newUser.Password = viewModel.pwd;

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
                    _logger.LogInformation("{actionStatus} User {userId} {userAction}. Email is already being used.", "Unsuccessful", newUser.Id, "create user");
                    _notfy.Error("Email is already used");
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
                        var image = photoUpload();
                        if (image != "")
                        {
                            DoctorRole.Photo = image;
                        }
                        if (newClinic.Id == null)
                        {
                            newClinic.Id = Guid.NewGuid().ToString();
                            _svc.AddClinic(newClinic);
                        }
                        DoctorRole.ClinicId = newClinic.Id;

                        
                        DoctorRole.Id = typeGuid;
                        DoctorRole.UserId = newUser.Id;
                        
                        _svc.AddDoctor(DoctorRole);
                        break;
                    case "Administrator":
                        if (newClinic.Id == null)
                        {
                            newClinic.Id = Guid.NewGuid().ToString();
                            _svc.AddClinic(newClinic);
                        }
                        AdminRole.ClinicId = newClinic.Id;


                        AdminRole.Id = typeGuid;
                        AdminRole.UserId = newUser.Id;
                        _svc.AddAdministrator(AdminRole);
                        break;
                    case "Pharmacist":
                        break;
                    case "ITSupport":
                        break;
                }

                _logger.LogInformation("{actionStatus} User {userId} {userAction}.", "Successful", newUser.Id, "create user");
                _notfy.Success("Create User Successfully");
                return RedirectToPage("Index");
            } else
            {
                _notfy.Error("Error");
            }
            //var error = ModelState.Values.SelectMany(v => v.Errors.Select(b => b.Exception));
            //foreach (var i in error)
            //{
            //    _logger.LogInformation(i);

            //}
            return Page();
        }

        public string photoUpload()
        {
            var files = HttpContext.Request.Form.Files;
            if (files.Count != 0)
            {
                var uploads = Path.Combine(webHostEnvironment.WebRootPath, "uploads\\images");
                var extensions = Path.GetExtension(files[0].FileName);

                var fileName = Guid.NewGuid().ToString().Replace("-", "") + extensions;

                using (var filestream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                {
                    files[0].CopyTo(filestream);
                }
                Console.WriteLine(DateTime.Now.ToString().Replace(" ", "") + extensions);
                return fileName;
            }
            return "";
        }
    }
}
