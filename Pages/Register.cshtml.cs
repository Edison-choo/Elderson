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

namespace Elderson.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public User newUser { get; set; }
        [BindProperty]
        //[Required, Compare(nameof(CreateUserModel.newUser.Password))]
        public string confirmPwd { get; set; }
        private UserService _svc;
        public RegisterModel(UserService service)
        {
            _svc = service;
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

                // Add user to Db
                var valid = false;
                while (!(valid))
                {
                    string guid = Guid.NewGuid().ToString();
                    newUser.Id = guid;
                    newUser.CreatedAt = DateTime.Now;
                    valid = _svc.AddUser(newUser);
                }

                

                return RedirectToPage("Index");
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
                   <button style = 'margin: 10px auto;'> Verify Email </button>
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