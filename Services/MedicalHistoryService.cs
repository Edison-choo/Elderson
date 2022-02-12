using Elderson.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elderson.Services
{
    public class MedicalHistoryService
    {
        private EldersonContext _context;
        public MedicalHistoryService(EldersonContext context)
        {
            _context = context;
        }
        //public List<MedicalHistory> GetMedicalHistoryOfUser(string id)
        //{

        //}
    }
}
