using Elderson.Models;
using Elderson.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpPost("{med_id}/{form_id}/{quantity}")]
        public ActionResult<List<Form>> Post(string med_id, string form_id, int quantity)
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
    }
}
