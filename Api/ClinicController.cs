using Elderson.Models;
using Elderson.Pages;
using Elderson.Services;
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
    public class ClinicController : ControllerBase
    {
        private readonly EldersonContext _context;
        private UserService _svc;
        public ClinicController(EldersonContext context, UserService service)
        {
            _context = context;
            _svc = service;
        }
        [HttpGet("{apiname}/{search}/{page}", Name = "getPage")]
        public ActionResult<List<Clinic>> Get(string search, int page)
        {
            List<Clinic> allClinics = _svc.GetAllClinic();
            try
            {
                var jsonStr = JsonSerializer.Serialize(allClinics.Select(c => new { c.Name, c.Address, c.CountryCode, c.Phone, }));
                return Ok(jsonStr);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
