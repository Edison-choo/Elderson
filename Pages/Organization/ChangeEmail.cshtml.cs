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
using MimeKit;
using MailKit.Net.Smtp;

namespace Elderson.Pages.Organization
{
    public class ChangeEmailModel : PageModel
    {
        public User user { get; set; }
        [BindProperty]
        public string Id { get; set; }
        [BindProperty]
        [Required]
        public string currentPwd { get; set; }
        [BindProperty]
        [Required, RegularExpression(@"^([a-zA-Z0-9]+)([\.{1}])?([a-zA-Z0-9]+)\@(?:gmail|GMAIL)([\.])(?:com|COM)$", ErrorMessage = "The email format is invalid. Only gmail is allowed.")]
        public string email { get; set; }
        [BindProperty]
        public string ErrorMsg { get; set; }

        private UserService _svc;
        private readonly ILogger<ChangeEmailModel> _logger;
        private readonly INotyfService _notfy;
        public ChangeEmailModel(ILogger<ChangeEmailModel> logger, UserService service, INotyfService notyf)
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

                SHA512 hashing = SHA512.Create();
                string checkPwdWithSalt = currentPwd + user.PasswordSalt;
                byte[] checkHashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(checkPwdWithSalt));

                if (user.Password.Equals(Convert.ToBase64String(checkHashWithSalt)))
                {
                    user.Email = email;
                    var code = Guid.NewGuid().ToString();
                    user.IsVerified = code;
                    Boolean valid = _svc.UpdateUser(user);
                    if (valid)
                    {
                        sendEmail(user.Email, user.Id, code);
                        _logger.LogInformation("{actionStatus} User {userId} {userAction}.", "Successful", user.Id, "update user");
                        _notfy.Success("Change password Successfully");

                        if (HttpContext.Session.GetString("LoginUser") != null)
                        {
                            HttpContext.Session.Remove("LoginUser");
                            HttpContext.Session.Remove("LoginUserType");
                            HttpContext.Session.Remove("LoginUserName");
                            HttpContext.Session.Remove("ChatUser");
                            _notfy.Success("Signout Successfully");

                        }
                        return RedirectToPage("/");
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
            client.Authenticate("eldersonhelpdesk@gmail.com", "Elderson123");

            client.Send(message);
            client.Disconnect(true);
            client.Dispose();

        }
    }
}
