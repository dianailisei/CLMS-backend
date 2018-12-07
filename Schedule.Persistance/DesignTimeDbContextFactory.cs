using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Schedule.Persistance
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ScheduleContext>
    {
        public ScheduleContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()+ "\\Schedule.Api\\")
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<ScheduleContext>();
            var connectionString = "Server=den1.mssql8.gear.host; Database=dotnot;User Id=dotnot;Password=Do75j23S!1!v;";
            builder.UseSqlServer(connectionString);
            return new ScheduleContext(builder.Options);
        }
    }
}
