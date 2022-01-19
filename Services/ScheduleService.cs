using Elderson.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elderson.Services
{
    public class ScheduleService
    {
        private EldersonContext _context;

        public ScheduleService(EldersonContext context)
        {
            _context = context;
        }

        private bool ScheduleExist(string id)
        {
            return _context.Schedule.Any(e => e.Id == id);
        }

        public bool AddSchedule(Schedule schedule)
        {
            if (ScheduleExist(schedule.Id))
            {
                return false;
            }
            _context.Add(schedule);
            _context.SaveChanges();
            return true;
        }
        public List<Schedule> GetAllSchedule()
        {
            List<Schedule> AllSchedule = new List<Schedule>();
            AllSchedule = _context.Schedule.ToList();
            return AllSchedule;
        }

        public Schedule GetScheduleById(String id)
        {
            Schedule schedule = _context.Schedule.Where(e => e.Id == id).FirstOrDefault();
            return schedule;
        }

        public bool DeleteSchedule (Schedule schedule)
        {
            bool deleted = true;
            _context.Attach(schedule).State = EntityState.Modified;

            try
            {
                _context.Remove(schedule);
                _context.SaveChanges();
                deleted = true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScheduleExist(schedule.Id))
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


        //Wye Keong Services :)
        public List<Schedule> GetOneDoctorSchedule(string doctorID)
        {
            List<Schedule> mySchedules = new List<Schedule>();
            mySchedules = _context.Schedule.Where(d => (d.DoctorId == doctorID) && (d.StartDateTime > DateTime.Now)).OrderBy(d => d.StartDateTime).ToList();
            return mySchedules;
        }
        public bool DoctorScheduleExists(string doctorID)
        {
            return _context.Schedule.Any(e => e.DoctorId == doctorID);
        }
        public Schedule GetScheduleByDateTime(DateTime datetime)
        {
            Schedule schedule = new Schedule();
            schedule = _context.Schedule.Where(s => s.StartDateTime == datetime).FirstOrDefault();
            return schedule;
        }
        public bool UpdateScheduleStatus(Schedule schedule)
        {
            bool updated = false;
            _context.Attach(schedule).State = EntityState.Modified;
            try
            {
                _context.SaveChanges();
                updated = true;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return updated;
        }
    }
}
