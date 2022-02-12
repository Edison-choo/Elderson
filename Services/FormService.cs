using Elderson.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elderson.Services
{
    public class FormService
    {
        private EldersonContext _context;
        public FormService(EldersonContext context)
        {
            _context = context;
        }

        private bool FormExist(string id)
        {
            return _context.Form.Any(e => e.Id == id);
        }

        public bool TemplateFormExist(string id)
        {
            return _context.Form.Any(e => e.Id == id);
        }

        public bool AddForm(Form form)
        {
            if (FormExist(form.Id))
            {
                return false;
            }
            _context.Add(form);
            _context.SaveChanges();
            return true;
        }

        public List<Form> GetAllForm(String doctor_id)
        {
            List<Form> allform = new List<Form>();
            allform = _context.Form.ToList();
            return allform;
        }

        public Form GetFormById(String id)
        {
            Form form = _context.Form.Where(e => e.Id == id).FirstOrDefault();
            return form;
        }

        public bool DeleteForm(Form form)
        {
            bool deleted = true;
            _context.Attach(form).State = EntityState.Modified;

            try
            {
                _context.Remove(form);
                _context.SaveChanges();
                deleted = true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FormExist(form.Id))
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

        public bool updateForm(Form form)
        {
            bool updated = true;
            _context.Attach(form).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
                updated = true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FormExist(form.Id))
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
