using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elderson.Models;
using Elderson.Services;
using System.Text.Json;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.Extensions.Logging;

namespace Elderson
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly EldersonContext _context;
        private UserService _svc;
        private ChatService _chat_svc;
        private readonly INotyfService _notfy;
        private readonly ILogger<UserController> _logger;

        public UserController(EldersonContext context, UserService service, ChatService chatService, INotyfService notyf, ILogger<UserController> logger)
        {
            _context = context;
            _svc = service;
            _notfy = notyf;
            _chat_svc = chatService;
            _logger = logger;
        }

        // GET: api/<UserController>
        [HttpGet]
        public ActionResult<List<User>> Get()
        {
            List<User> alluser = new List<User>();

            try
            {
                alluser = _svc.GetAllUsers();
                var jsonStr = JsonSerializer.Serialize(alluser.Select(x => new { x.Id, x.Fullname, x.UserType, x.Phone}));
                return Ok(jsonStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest();
            }
            
        }

        [HttpGet("GetPatients", Name = "GetPatients")]
        public ActionResult<List<User>> GetPatients()
        {
            List<User> allpatients = new List<User>();

            try
            {
                allpatients = _svc.GetAllUsers();
                var jsonStr = JsonSerializer.Serialize(allpatients.Where(x => x.UserType == "Patient").Select(x => new { x.Id, x.Fullname, x.UserType, x.Phone }));
                return Ok(jsonStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest();
            }

        }

        //[HttpGet("{apiPatientname}", Name = "GetPatients")]
        //public ActionResult<List<User>> GetPatients()
        //{
        //    List<User> alluser = new List<User>();

        //    try
        //    {
        //        alluser = _svc.GetAllUsers();
        //        var jsonStr = JsonSerializer.Serialize(alluser.Where(x=> x.UserType == "Patient").Select(x => new { x.Id, x.Fullname, x.Email }));
        //        return Ok(jsonStr);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("instalmentController.getCarLoan", ex);
        //        return BadRequest();
        //    }

        //}

        [HttpGet("Signout", Name = "Signout")]
        public ActionResult Signout()
        {
            if (HttpContext.Session.GetString("LoginUser") != null)
            {
                HttpContext.Session.Remove("LoginUser");
                HttpContext.Session.Remove("LoginUserType");
                HttpContext.Session.Remove("LoginUserName");
                HttpContext.Session.Remove("ChatUser");
                _notfy.Success("Signout Successfully");
                return Ok();

            } else
            {
                return BadRequest();
            }
        }


        [HttpGet("ChatUser/{userId}", Name = "Message")]
        public ActionResult Message(string userId)
        {
            HttpContext.Session.SetString("ChatUser", userId);
            
            if (userId == "ISRChat")
            {
                HttpContext.Session.SetString("ChatUserName", "IT Support");
            } else
            {
                var name = _svc.GetAllUsers().Where(x => x.Id == userId).Select(x => x.Fullname).ToList()[0];
                HttpContext.Session.SetString("ChatUserName", name);
            }
            return Ok();
        }

        [HttpGet("PendingChatUser/{userId}", Name = "PendingMessage")]
        public ActionResult PendingMessage(string userId)
        {
            HttpContext.Session.SetString("ChatUser", userId);
            var name = _svc.GetAllUsers().Where(x => x.Id == userId).Select(x => x.Fullname).ToList()[0];
            HttpContext.Session.SetString("ChatUserName", name);
            Dictionary<string, List<Message>> allmessages = _chat_svc.GetAllChats("ISRChat");
            foreach (var msg in allmessages[userId])
            {
                msg.ToUserId = HttpContext.Session.GetString("LoginUser");
                _chat_svc.UpdateChat(msg);
            }
            
            return Ok();
        }

        [HttpGet("GetName/{userId}", Name ="GetName")]
        public ActionResult<String> GetName(string userId)
        {
            List<User> alluser = new List<User>();

            try
            {
                alluser = _svc.GetAllUsers();
                var jsonStr = JsonSerializer.Serialize(alluser.Where(x => x.Id == userId).Select(x => x.Fullname));
                return Ok(jsonStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest();
            }

        }

        //// GET api/<UserController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        public class CompositeObject
        {
            public User user { get; set; }
            public Patient PatientRole { get; set; }
            public string BirthDate { get; set; }

        }

        // POST api/<UserController>
        [HttpPost]
        public ActionResult<string> Post([FromForm] CompositeObject body)
        {
            Console.WriteLine(body.user.Gender+body.PatientRole.Nric+body.BirthDate);
            Console.WriteLine(ModelState.IsValid);

            if (!(ModelState.IsValid))
            {
                _notfy.Error("Error");
                Console.WriteLine("Test1");
                return BadRequest("Error");
            }

            User UpdatedUser;
            Patient UpdatedPatient;

            UpdatedUser = _svc.GetUserById(body.user.Id);
            if (_svc.GetUserByEmail(body.user.Email) != null && body.user.Email != UpdatedUser.Email)
            {
                _logger.LogInformation("{actionStatus} User {userId} {userAction}. Email is already being used.", "Unsuccessful", body.user.Id, "edit user");
                _notfy.Error("Email is already used");
                Console.WriteLine("Test2");
                return BadRequest("Error");
            }
            UpdatedUser.Fullname = body.user.Fullname;
            UpdatedUser.Email = body.user.Email;
            UpdatedUser.Phone = body.user.Phone;
            UpdatedUser.Gender = body.user.Gender;
            UpdatedUser.CountryCode = body.user.CountryCode;
            UpdatedUser.Birthdate = Convert.ToDateTime(body.BirthDate);
            Boolean valid = _svc.UpdateUser(UpdatedUser);

            UpdatedPatient = _svc.GetPatientById(UpdatedUser.Id);
            UpdatedPatient.Nric = body.PatientRole.Nric;
            UpdatedPatient.Relationship = body.PatientRole.Relationship;
            UpdatedPatient.EmergencyName = body.PatientRole.EmergencyName;
            UpdatedPatient.EmergencyNum = body.PatientRole.EmergencyNum;
            UpdatedPatient.HomeAddr = body.PatientRole.HomeAddr;
            UpdatedPatient.CountryCode = body.PatientRole.CountryCode;
            _svc.UpdatePatient(UpdatedPatient);

            if (valid)
            {
                _logger.LogInformation("{actionStatus} User {userId} {userAction}.", "Successful", body.user.Id, "edit user");
                _notfy.Success("Edit User Successfully");
                Console.WriteLine("Test3");
                return Ok("Success");

            }
            Console.WriteLine("Test4");;
            return BadRequest("Error");

        }

        //// PUT api/<UserController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<UserController>/5
        [HttpDelete("{userId}")]
        public ActionResult Delete(string userId)
        {
            try
            {
                var deleteUser = _svc.GetUserById(userId);

                switch (deleteUser.UserType)
                {
                    case "Patient":
                        _svc.DeletePatient(_svc.GetPatientById(userId));
                        break;
                    case "Doctor":
                        _svc.DeleteDoctor(_svc.GetDoctorById(userId));
                        break;
                    case "Administrator":
                        _svc.DeleteAdministrator(_svc.GetAdministratorById(userId));
                        break;
                }

                _svc.DeleteUser(deleteUser);
                _notfy.Success("Delete User Successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine("UserController.DeleteUser", ex);
                return BadRequest("Error");
            }
            return Ok("Success");
        }

    }
}
