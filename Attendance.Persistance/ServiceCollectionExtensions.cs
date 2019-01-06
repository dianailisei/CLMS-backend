using Microsoft.Extensions.DependencyInjection;
using Attendance.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Attendance.Persistance
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPersistance(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AttendanceContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IReadRepository>(provider => provider.GetService<AttendanceContext>());
            services.AddScoped<IWriteRepository>(provider => provider.GetService<AttendanceContext>());

            return services;
        }
    }
}
