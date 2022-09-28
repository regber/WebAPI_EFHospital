using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI_EFHospital.Model;

namespace WebAPI_EFHospital
{
    public class Context:DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<EmplSchedule> EmplSchedules { get; set; }
        public DbSet<Window> Windows { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Office> Offices { get; set; }


        public Context()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data source=Hospital.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().ToTable("People");
            modelBuilder.Entity<Employee>().ToTable("Employees");
            modelBuilder.Entity<Client>().ToTable("Clients");


            modelBuilder.Entity<Window>().HasOne(w => w.Registration).WithOne(r => r.Window).HasForeignKey<Registration>(r=>r.WindowId);
            modelBuilder.Entity<Window>().HasOne(w => w.Office).WithOne(o => o.Window).HasForeignKey<Office>(o => o.WindowId);
        }
    }
}
