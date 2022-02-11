using Elderson.Models;
using Microsoft.EntityFrameworkCore;
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

        private bool FormMedExist(string id)
        {
            return _context.Schedule.Any(e => e.Id == id);
        }

        public List<FormMeds> GetFormMedsByFormId(string form_id)
        {
            List<FormMeds> allformmeds = new List<FormMeds>();
            allformmeds = _context.FormMeds.Where(e => e.FormId == form_id).ToList();
            return allformmeds;
        }

        public FormMeds GetFormMedsById(string id)
        {
            FormMeds formmed = new FormMeds();
            formmed = _context.FormMeds.Where(e => e.Id == id).FirstOrDefault();
            return formmed;
        }

        public bool AddFormMeds(FormMeds formMeds)
        {
            _context.Add(formMeds);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteFormMed(FormMeds formMeds)
        {
            bool deleted = true;
            _context.Attach(formMeds).State = EntityState.Modified;

            try
            {
                _context.Remove(formMeds);
                _context.SaveChanges();
                deleted = true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FormMedExist(formMeds.Id))
                {
                    deleted = false;
                }
                else
                {
                    throw;
                }
            }
            return deleted;
        }
    }
}
