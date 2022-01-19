using Elderson.Models;
using Elderson.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Elderson.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly EldersonContext _context;
        private InventoryService _svc;

        public InventoryController(EldersonContext context, InventoryService service)
        {
            _context = context;
            _svc = service;
        }

        // GET: api/<InventoryController>
        [HttpGet]
        public ActionResult<List<MedInventory>> Get()
        {
            List<MedInventory> allinventory = new List<MedInventory>();
            //List<Medication> allmedications = new List<Medication>();

            try
            {
                allinventory = _svc.GetAllInventories();
                //allmedications = _svc.GetAllMedications();
                var jsonStr = JsonSerializer.Serialize(allinventory.Select(x => new { x.Id, x.MinimumAmt, x.CurrentAmt, x.Price}));
                //var jsonString = JsonSerializer.Serialize(allmedications.Select(y => new { y.MedName, y.MedAbbreviation, y.MedType, y.MedSupplierAbb, y.MedDescription }));
                return Ok(jsonStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine("instalmentController.getCarLoan", ex);
                return BadRequest();
            }

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
        //[HttpDelete("{userId}")]
        //public ActionResult Delete(string userId)
        //{
        //    try
        //    {
        //        var deleteUser = _svc.GetUserById(userId);
        //        _svc.DeleteUser(deleteUser);

        //        switch (deleteUser.UserType)
        //        {
        //            case "Patient":
        //                _svc.DeletePatient(_svc.GetPatientById(userId));
        //                break;
        //            case "Doctor":
        //                _svc.DeleteDoctor(_svc.GetDoctorById(userId));
        //                break;
        //            case "Administrator":
        //                _svc.DeleteAdministrator(_svc.GetAdministratorById(userId));
        //                break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("UserController.DeleteUser", ex);
        //    }
        //    return Ok();
        //}

    }
}
