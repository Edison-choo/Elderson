using Elderson.Models;
using Microsoft.EntityFrameworkCore;
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

        public bool AddEntry(PatientDetails newEntry)
        {
            if (EntryExists(newEntry.Id))
            {
                return false;
            }
            _context.Add(newEntry);
            _context.SaveChanges();
            return true;
        }
        public List<PatientDetails> GetAllEntries()
        {
            List<PatientDetails> AllEntries = new List<PatientDetails>();
            AllEntries = _context.PatientDetails.ToList();
            return AllEntries;
        }
        public PatientDetails GetEntryById(String id)
        {
            PatientDetails EntryId = _context.PatientDetails.Where(e => e.Id == id).FirstOrDefault();
            return EntryId;
        }
        private bool EntryExists(string id)
        {
            return _context.PatientDetails.Any(e => e.Id == id);
        }

        public bool UpdateEntry(PatientDetails updateEntry)
        {
            bool updated = true;
            _context.Attach(updateEntry).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
                updated = true;

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntryExists(updateEntry.Id))
                {
                    updated = false;
                }
                else
                {
                    throw;
                }
            }
            return updated;


        }
    }
}
