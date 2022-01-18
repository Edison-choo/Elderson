using Elderson.Models;
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
    }
}
