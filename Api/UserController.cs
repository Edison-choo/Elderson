using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elderson.Models;
using Elderson.Services;

namespace Elderson
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly EldersonContext _context;
        private UserService _svc;

        public UserController(EldersonContext context, UserService service)
        {
            _context = context;
            _svc = service;
        }

        // GET: api/<UserController>
        [HttpGet]
        public ActionResult<List<User>> Get()
        {
            List<User> alluser = new List<User>();

            try
            {
                alluser = _svc.GetAllUsers();
            }
            catch (Exception ex)
            {
                Console.WriteLine("instalmentController.getCarLoan", ex);
            }
            return Ok(alluser);
        }

        //// GET api/<UserController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<UserController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

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
                _svc.DeleteUser(deleteUser);

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
            }
            catch (Exception ex)
            {
                Console.WriteLine("UserController.DeleteUser", ex);
            }
            return Ok();
        }

    }
}
