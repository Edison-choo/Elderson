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
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Logging;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace Elderson.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public User newUser { get; set; }
        [BindProperty]
        public Patient PatientRole { get; set; }
        //[BindProperty]
        //[Required]
        //public string pwd { get; set; }
        //[BindProperty]
        //[Required]
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
        private readonly ILogger<RegisterModel> _logger;
        private readonly INotyfService _notfy;
        public RegisterModel(ILogger<RegisterModel> logger,UserService service, INotyfService notyf)
        {
            _svc = service;
            _logger = logger;
            _notfy = notyf;
        }
        public void OnGet()
        {
            //sendEmail("edisonchoo234@gmail.com");
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
                newUser.UserType = "Patient";

                if (_svc.GetUserByEmail(newUser.Email) != null)
                {
                    _logger.LogInformation("{actionStatus} User {userAction}. Email is already being used.", "Unsuccessful", "register");
                    _notfy.Success("Email is already used");
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
                PatientRole.Id = typeGuid;
                PatientRole.UserId = newUser.Id;
                _svc.AddPatient(PatientRole);

                _logger.LogInformation("{actionStatus} User {userId} {userAction}.", "Successful", newUser.Id, "register");
                _notfy.Success("Register Successfully");

                return RedirectToPage("Login");
            } else
            {
                string messages = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                Console.WriteLine(messages);
                _notfy.Error("Error");
            }

            return Page();
        }

        public void sendEmail(string email)
        {
            MimeMessage message = new MimeMessage();

            MailboxAddress from = new MailboxAddress("Admin", "eldersonhelpdesk@gmail.com");
            message.From.Add(from);

            MailboxAddress to = new MailboxAddress("User", email);
            message.To.Add(to);

            message.Subject = "This is email subject";

            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = @"<div>
    <h2 style='margin - bottom:20px; '>Verify This Email Address</h2>
       <p> Hi Peter,</p>
          <p> Please click the button below to verify your email address </p>
             <p> If you did not sign up to Elderson, please ignore this email or contact us at eldersonheelpdesk@gmail.com </p>
                <p> Edison </p>
                <p> IT Support Team</p>
                   <button style = 'margin: 10px auto;' onclick='location.href = 'http://www.example.com''> Verify Email </button>
                      </div> ";

            message.Body = bodyBuilder.ToMessageBody();

            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 465, true);
            client.Authenticate("eldersonhelpdesk@gmail.com", "Elderson123");

            client.Send(message);
            client.Disconnect(true);
            client.Dispose();

        }
    }
}
