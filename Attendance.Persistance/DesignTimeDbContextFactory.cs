using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Attendance.Persistance
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AttendanceContext>
    {
        public AttendanceContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()+ "\\Attendance.Api\\")
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<AttendanceContext>();
            //var connectionString = @"Server=DESKTOP-99S221B;Database=Attendance;Trusted_Connection=True;";
            var connectionString = "Server=den1.mssql7.gear.host; Database=dotnotattendance;User Id=dotnotattendance;Password=Zn679H~81v7_;";
            builder.UseSqlServer(connectionString);
            return new AttendanceContext(builder.Options);
        }
    }
}
