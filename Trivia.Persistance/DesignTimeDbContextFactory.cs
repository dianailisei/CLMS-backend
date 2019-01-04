using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Trivia.Persistance
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<TriviaContext>
    {
        public TriviaContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(Directory.GetCurrentDirectory()).ToString() + "\\Trivia.Api\\")
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<TriviaContext>();
            var connectionString = "Server=den1.mssql7.gear.host; Database=dotnottrivia;User Id=dotnottrivia;Password=Pn89d!co1dS_;";
            builder.UseSqlServer(connectionString);
            return new TriviaContext(builder.Options);
        }
    }
}
