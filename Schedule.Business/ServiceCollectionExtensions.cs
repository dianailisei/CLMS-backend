using Microsoft.Extensions.DependencyInjection;
using Schedule.Business.Subject;

namespace Schedule.Business
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services)
        {
            // Here we add scopes on servives
            // Example: services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ISubjectService, SubjectService>();

            return services;
        }
    }
}
