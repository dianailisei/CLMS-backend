using Microsoft.Extensions.DependencyInjection;
using Schedule.Business.Laboratory;
using Schedule.Business.Student;
using Schedule.Business.Teacher;

namespace Schedule.Business
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services)
        {
            // Here we add scopes on servives
            // Example: services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ILaboratoryService, LaboratoryService>();
            services.AddScoped<ITeacherService, TeacherService>();

            return services;
        }
    }
}
