using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elderson.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Elderson.Services
{
    public class FAQService : Controller
    {
        private EldersonContext _context;

        public FAQService(EldersonContext context)
        {
            _context = context;
        }

        public bool SubmitQuery(FAQ submitQuery)
        {
            _context.Add(submitQuery);
            _context.SaveChanges();
            return true;
        }
        public List<FAQ> GetAllQueries()
        {
            List<FAQ> GetAllQueries = new List<FAQ>();
            GetAllQueries = _context.FAQ.ToList();
            return GetAllQueries;
        }
        public FAQ GetQueryById(String id)
        {
            FAQ QueryId = _context.FAQ.Where(e => e.Id == id).FirstOrDefault();
            return QueryId;
        }
        private bool QueryExists(string id)
        {
            return _context.FAQ.Any(e => e.Id == id);
        }
        public bool DeleteQuery(FAQ deleteFAQ)
        {
            bool deleted = true;
            _context.Attach(deleteFAQ).State = EntityState.Modified;

            try
            {
                _context.Remove(deleteFAQ);
                _context.SaveChanges();
                deleted = true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QueryExists(deleteFAQ.Id))
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
