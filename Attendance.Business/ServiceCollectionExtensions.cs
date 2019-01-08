using Attendance.Business.Presence;
using Attendance.Business.Session;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace Schedule.Business
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services)
        {
            services.AddMvc().AddFluentValidation(fv => {
                fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
            });

            services.AddScoped<ISessionService, SessionService>();
            services.AddScoped<IPresenceService, PresenceService>();


            /*services.AddTransient<IValidator<StudentCreateModel>, StudentCreateModelValidator>();

            services.AddScoped<IStudentService, StudentService>();*/

            return services;
        }
    }
}
