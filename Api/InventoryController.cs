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
        private FormMedsService _formmed_svc;
        private PrescriptionService _pres_svc;

        public InventoryController(EldersonContext context, InventoryService service, FormMedsService formMedService, PrescriptionService prescripService)
        {
            _context = context;
            _svc = service;
            _formmed_svc = formMedService;
            _pres_svc = prescripService;
        }

        // GET: api/<InventoryController>
        [HttpGet]
        public ActionResult<List<Medication>> Get()
        {
            List<MedInventory> allinventory = new List<MedInventory>();
            List<Medication> allmedications = new List<Medication>();

            try
            {
                allmedications = _svc.GetAllMedications();
                var jsonString = JsonSerializer.Serialize(allmedications.Select(y => new { y.Id, y.MedName, y.MedAbbreviation, y.MedType, y.MedSupplierAbb, CurrentAmt = _svc.GetInvMedicationById(y.Id).CurrentAmt, Price = "$" + _svc.GetInvMedicationById(y.Id).Price }));
                return Ok(jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine("instalmentController.getCarLoan", ex);
                return BadRequest();
            }

        }

        [HttpGet("GetPrescriptions", Name = "GetPrescriptions")]
        public ActionResult<List<Medication>> GetPrescriptions()
        {
            List<Prescription> allPrescriptions = new List<Prescription>();
            List<FormMeds> formMedsByID = new List<FormMeds>();

            try
            {
                allPrescriptions = _pres_svc.GetPrescriptionsNew();

                var jsonString = JsonSerializer.Serialize(allPrescriptions.Select(y => new { y.Id, y.FormId, y.PatientId, y.PatientName, y.Status, y.DoctorName, Date = y.Date.ToShortDateString() }));
                return Ok(jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine("instalmentController.getCarLoan", ex);
                return BadRequest();
            }

        }

        [HttpGet("GetOldPrescriptions", Name = "GetOldPrescriptions")]
        public ActionResult<List<Medication>> GetOldPrescriptions()
        {
            List<Prescription> allPrescriptions = new List<Prescription>();
            List<FormMeds> formMedsByID = new List<FormMeds>();

            try
            {
                allPrescriptions = _pres_svc.GetPrescriptionsOld();

                var jsonString = JsonSerializer.Serialize(allPrescriptions.Select(y => new { y.Id, y.FormId, y.PatientId, y.PatientName, y.Status, y.DoctorName, y.Date }));
                return Ok(jsonString);
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

        public MedInventory oldInv { get; set; }
        [HttpPost("{medication_id}/{quantity}")]
        public ActionResult<List<FormMeds>> Post(string medication_id, int quantity)
        {
            oldInv = _svc.GetInvMedicationById(medication_id);
            MedInventory inventory = new MedInventory();
            inventory.Id = medication_id;
            inventory.MedicationId = medication_id;
            inventory.CurrentAmt = quantity;
            inventory.MinimumAmt = oldInv.MinimumAmt;
            inventory.Price = oldInv.Price;
            Medication med = _svc.GetMedicationById(medication_id);
            try
            {
                _svc.UpdateInventory(inventory, med);
            }
            catch (Exception ex)
            {
                throw;
            }
            return Ok();
        }


        [HttpDelete("{Id}")]
        public ActionResult Delete(string Id)
        {
            try
            {
                var deleteInventory = _svc.GetInvMedicationById(Id);
                var deleteMedication = _svc.GetMedicationById(Id);
                _svc.DeleteInventory(deleteInventory, deleteMedication);
                _svc.DeleteMedication(deleteMedication);
            }
            catch (Exception ex)
            {
                Console.WriteLine("InventoryController.DeleteMedication", ex);
                return BadRequest("Error");
            }
            return Ok("Success");
        }

    }
}
