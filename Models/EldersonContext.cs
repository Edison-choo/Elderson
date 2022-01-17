using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Elderson.Models
{
    public class EldersonContext : DbContext
    {
        private readonly IConfiguration _config;
        public EldersonContext(IConfiguration configuration)
        {
            _config = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = _config.GetConnectionString("MyConn");
            optionsBuilder.UseSqlServer(connectionString);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Incident> Incidents { get; set; }
        public DbSet<FAQ> FAQs { get; set; }

        public DbSet<FAQ> FAQ { get; set; }
    }
}
