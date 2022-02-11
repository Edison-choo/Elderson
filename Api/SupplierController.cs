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
    public class SupplierController : Controller
    {


        private readonly EldersonContext _context;
        private SupplierService _svc;
        public SupplierController(EldersonContext context, SupplierService ser)
        {
            _context = context;
            _svc = ser;
        }

        [HttpDelete("{Id}")]
        public ActionResult Delete(string Id)
        {
            try
            {
                var deleteSupplier = _svc.GetSupplierbyId(Id);
                _svc.DeleteSupplier(deleteSupplier);
            }
            catch (Exception ex)
            {
                Console.WriteLine("SupplierController.DeleteSupplier", ex);
                return BadRequest("Error");
            }
            return Ok("Success");
        }

        

        // GET: api/<InventoryController>
        [HttpGet]
        public ActionResult<List<Supplier>> Get()
        {
            List<Supplier> allsuppliers = new List<Supplier>();

            try
            {
                allsuppliers = _svc.GetAllSuppliers();
                var jsonStr = JsonSerializer.Serialize(allsuppliers.Select(x => new { x.Id, x.SupplierAbbreviation, x.SupplierName, x.SupplierPhone, x.SupplierEmail, x.SuppplierWebsite }));
                return Ok(jsonStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine("instalmentController.getCarLoan", ex);
                return BadRequest();
            }

        }
    }
}
