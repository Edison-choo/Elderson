using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}