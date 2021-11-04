using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmployeeManagement.DataModel
{
    public class DatabaseHelper
    {
        private static DatabaseHelper _instance;
        public static DatabaseHelper Instance
        {
            get
            {
                if (_instance == null) _instance = new DatabaseHelper();
                return _instance;
            }
        }

        private DatabaseHelper()
        {

        }

        public void CreateDbIfNotExists(IServiceScope scope, ILogger logger)
        {
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<EmployeeSystemContext>();
                context.Database.EnsureCreated();
                Initialize(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred creating the DB.");
            }
        }

        public void InitializeConnection(IServiceCollection services, string connString )
        {
            services.AddDbContext<EmployeeSystemContext>(options =>
                  options.UseSqlite(connString));

            //services.AddDbContext<EmployeeSystemContext>(options =>
            //        options.UseSqlServer(connString));
        }
        private void Initialize(EmployeeSystemContext context)
        {
            // Look for any employees.
            if (context.Employees.Any())
            {
                return;   // DB has been seeded
            }

            var employees = new Employee[]
            {
                new Employee{ID = 1, FirstName="Kayla",LastName="Yu",HiredDate=DateTime.Parse("2019-09-01")},
                new Employee{ID = 2, FirstName="Zhu",LastName="Zhu",HiredDate=DateTime.Parse("2021-09-01")}
            };

            context.Employees.AddRange(employees);
            context.SaveChanges();

            var tasks = new EmployeeTask[]
            {
                new EmployeeTask{TaskName="Hiring", EmployeeID = 1, StartTime = DateTime.Parse("2021-09-01"), Deadline = DateTime.Parse("2021-12-01")},
                new EmployeeTask{TaskName="Developing", EmployeeID = 2, StartTime = DateTime.Parse("2021-12-01"), Deadline = DateTime.Parse("2022-12-01")},
            };

            context.EmployeeTasks.AddRange(tasks);
            context.SaveChanges();
        }
    }
}
