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
    public class FormMedsController : ControllerBase
    {
        private readonly EldersonContext _context;
        private FormMedsService _svc;
        private InventoryService _invsvc;

        public FormMedsController(EldersonContext context, FormMedsService service, InventoryService service2)
        {
            _context = context;
            _svc = service;
            _invsvc = service2;
        }

        // GET: api/<FormMedsController>/form_id
        [HttpGet("{form_id}")]
        public ActionResult<List<FormMeds>> Get(string form_id)
        {
            List<FormMeds> allform = new List<FormMeds>();

            try
            {
                allform = _svc.GetFormMedsByFormId(form_id);
                var jsonStr = JsonSerializer.Serialize(allform.Select(x => new { x.Id, x.FormId, x.MedicationId, x.Quantity, x.MedName, x.MedType }));
                return Ok(jsonStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine("instalmentController.getCarLoan", ex);
                return BadRequest();
            }

        }

        [HttpGet("GetInvMeds", Name = "GetInvMeds")]
        public ActionResult<List<Medication>> GetInvMeds()
        {
            List<Medication> allmedications = new List<Medication>();

            try
            {
                allmedications = _invsvc.GetAllMedications();
                Console.WriteLine(allmedications);
                var jsonStr = JsonSerializer.Serialize(allmedications.Select(x => new { x.Id, x.MedName, x.MedType, x.MedAllergyIngredients }));
                return Ok(jsonStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine("instalmentController.getCarLoan", ex);
                return BadRequest();
            }

        }

        [HttpPost("{med_id}/{form_id}/{quantity}")]
        public ActionResult<List<FormMeds>> Post(string med_id, string form_id, int quantity)
        {
            FormMeds formMed = new FormMeds();
            formMed.Id = Guid.NewGuid().ToString();
            formMed.MedicationId = med_id;
            formMed.FormId = form_id;
            formMed.Quantity = quantity;
            Medication med = _invsvc.GetMedicationById(med_id);
            formMed.MedName = med.MedName;
            formMed.MedType = med.MedType;
            try
            {
                _svc.AddFormMeds(formMed);
            }
            catch (Exception ex)
            {
                Console.WriteLine("UserController.DeleteUser", ex);
            }
            return Ok();
        }

        // DELETE api/<FormMedsController>/id
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            try
            {
                var deleteFormmed = _svc.GetFormMedsById(id);
                _svc.DeleteFormMed(deleteFormmed);
            }
            catch (Exception ex)
            {
                Console.WriteLine("FormMedsController.DeleteFormMed", ex);
            }
            return Ok();
        }
    }
}
