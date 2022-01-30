﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elderson.Models;
using Elderson.Services;
using System.Text.Json;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace Elderson
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly EldersonContext _context;
        private UserService _svc;
        private readonly INotyfService _notfy;

        public UserController(EldersonContext context, UserService service, INotyfService notyf)
        {
            _context = context;
            _svc = service;
            _notfy = notyf;
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
                Console.WriteLine("instalmentController.getCarLoan", ex);
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

        [HttpGet("{apiname}", Name = "Signout")]
        public ActionResult Signout()
        {
            if (HttpContext.Session.GetString("LoginUser") != null)
            {
                HttpContext.Session.Remove("LoginUser");
                HttpContext.Session.Remove("LoginUserType");
                HttpContext.Session.Remove("ChatUser");
                _notfy.Success("Signout Successfully");
                return Ok();

            } else
            {
                return BadRequest();
            }
        }

        [HttpGet("{apiname}/{userId}", Name = "Message")]
        public ActionResult Message(string userId)
        {
            HttpContext.Session.SetString("ChatUser", userId);
            return Ok();
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
