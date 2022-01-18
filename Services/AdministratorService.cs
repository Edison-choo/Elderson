using Elderson.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elderson.Services
{
    public class AdministratorService
    {
        private EldersonContext _context;

        public AdministratorService(EldersonContext context)
        {
            _context = context;
        }
        public bool AddEntry(PatientDetails patientDetails)
        {
            _context.Add(patientDetails);
            _context.SaveChanges();
            return true;
        }
    }
}
