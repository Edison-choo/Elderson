using Elderson.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elderson.Services
{
    public class IncidentService
    {
        private EldersonContext _context;

        public IncidentService(EldersonContext context)
        {
            _context = context;
        }
        public List<Incident> GetAllIncidents()
        {
            List<Incident> AllIncidents = new List<Incident>();
            AllIncidents = _context.Incidents.ToList();
            return AllIncidents;
        }
        public Incident GetIncidentById(int id)
        {
            Incident incident = _context.Incidents.Where(e => e.Id == id).FirstOrDefault();
            return incident;
        }

        private bool IncidentExists(int id)
        {
            return _context.Incidents.Any(e => e.Id == id);
        }

        public bool AddIncident(Incident incident)
        {
            if (IncidentExists(incident.Id))
            {
                return false;
            }
            _context.Add(incident);
            _context.SaveChanges();
            return true;
        }

        public bool UpdateIncident(Incident theincident)
        {
            bool updated = true;
            _context.Attach(theincident).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
                updated = true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IncidentExists(theincident.Id))
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

        public bool DeleteIncident(Incident theincident)
        {
            bool deleted = true;
            _context.Attach(theincident).State = EntityState.Modified;

            try
            {
                _context.Remove(theincident);
                _context.SaveChanges();
                deleted = true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IncidentExists(theincident.Id))
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
