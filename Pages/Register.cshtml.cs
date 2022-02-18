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
using Microsoft.AspNetCore.Http;

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
            [Required, RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^0-9a-zA-Z]).{8,}$", ErrorMessage = "Password needs at one special characters, one uppercase letter, one lowercase letter, one digit with length more than 8.")]
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
        public IActionResult OnGet()
        {
            //sendEmail("edisonchoo234@gmail.com", "c9d07563-9b76-4f8f-b2d9-211883a3b9c9", "1");
            if (HttpContext.Session.GetString("LoginUser") != null)
            {
                if (HttpContext.Session.GetString("LoginUserType") == "Doctor")
                {
                    return Redirect("~/DRR");
                }
                else if (HttpContext.Session.GetString("LoginUserType") == "ITSupport")
                {
                    return Redirect("~/ISR/Users");
                }
                else if (HttpContext.Session.GetString("LoginUserType") == "Administrator")
                {
                    return Redirect("~/Administrator/AdminHomePage");
                }
                else if (HttpContext.Session.GetString("LoginUserType") == "Pharmacist")
                {
                    return Redirect("~/Pharmacist");
                }
                else
                {
                    return Redirect("~/");
                }

            }

            return Page();
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
                var code = Guid.NewGuid().ToString();
                while (!(valid))
                {
                    string guid = Guid.NewGuid().ToString();
                    newUser.Id = guid;
                    newUser.CreatedAt = DateTime.Now;

                    newUser.IsVerified = "1";
                    valid = _svc.AddUser(newUser);
                }

                string typeGuid = Guid.NewGuid().ToString();
                PatientRole.Id = typeGuid;
                PatientRole.UserId = newUser.Id;
                _svc.AddPatient(PatientRole);

                _logger.LogInformation("{actionStatus} User {userId} {userAction}.", "Successful", newUser.Id, "register");
                _notfy.Success("Register Successfully. Verify your email to login.");

                //sendEmail(newUser.Email, newUser.Id, code);

                return RedirectToPage("emailVerification");
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

        public void sendEmail(string email, string id, string code)
        {
            MimeMessage message = new MimeMessage();

            MailboxAddress from = new MailboxAddress("Admin", "eldersonhelpdesk@gmail.com");
            message.From.Add(from);

            MailboxAddress to = new MailboxAddress("User", email);
            message.To.Add(to);

            message.Subject = "This is email subject";

            BodyBuilder bodyBuilder = new BodyBuilder();
            string link = $"location.href='https://localhost:44311/api/User/VerifyUser/{id}/{code}'";
            bodyBuilder.HtmlBody = $@"<div>
    <h2 style='margin - bottom:20px; '>Verify This Email Address</h2>
       <p> Hi Peter,</p>
          <p> Please click the button below to verify your email address </p>
             <p> If you did not sign up to Elderson, please ignore this email or contact us at eldersonheelpdesk@gmail.com </p>
                <p> Edison </p>
                <p> IT Support Team</p>
                   <button style = 'margin: 10px auto;'><a style='text-decoration:none;color:black;' href='https://localhost:44311/api/User/VerifyUser/{id}/{code}'> Verify Email</a></button>
                      </div> ";

            message.Body = bodyBuilder.ToMessageBody();

            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 465, true);
            client.Authenticate("", "");

            client.Send(message);
            client.Disconnect(true);
            client.Dispose();

        }
    }
}
