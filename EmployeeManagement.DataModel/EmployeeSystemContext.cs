using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.DataModel
{
    public class EmployeeSystemContext : DbContext
    {
        public EmployeeSystemContext (DbContextOptions<EmployeeSystemContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<EmployeeTask> EmployeeTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable(nameof(Employee))
                .HasMany(c => c.Tasks);
            modelBuilder.Entity<EmployeeTask>().ToTable(nameof(EmployeeTask));
        }
    }
}
