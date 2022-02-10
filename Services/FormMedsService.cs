using Elderson.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elderson.Services
{
    public class FormMedsService
    {
        private EldersonContext _context;
        public FormMedsService(EldersonContext context)
        {
            _context = context;
        }

        public bool AddFormMeds(FormMeds formMeds)
        {
            _context.Add(formMeds);
            _context.SaveChanges();
            return true;
        }
    }
}
