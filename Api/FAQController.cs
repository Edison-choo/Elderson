using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Elderson.Models;
using Elderson.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Elderson.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class FAQController : ControllerBase
    {
        public FAQ newQuery{ get; set; }
        private readonly EldersonContext _context;
        private FAQService _svc;
        public FAQController(EldersonContext context, FAQService service)
        {
            _context = context;
            _svc = service;
        }
        [HttpGet]
        public ActionResult<List<FAQ>> Get()
        {
            List<FAQ> allQueries = new List<FAQ>();

            try
            {
                allQueries = _svc.GetAllQueries();
                var jsonStr = JsonSerializer.Serialize(allQueries.Select(x => new { x.Id, x.FullName, x.Topic, x.Question, x.UserId }));
                return Ok(jsonStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest();
            }

        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string Id)
        {
            try
            {
                var deleteFAQ = _svc.GetQueryById(Id);
                _svc.DeleteQuery(deleteFAQ);
            }
            catch (Exception ex)
            {
                Console.WriteLine("FAQController.DeleteQuery", ex);
            }
            return Ok();
        }
    }
}