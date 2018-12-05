﻿using Microsoft.Extensions.DependencyInjection;
using Schedule.Business.Student;

namespace Schedule.Business
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services)
        {
            // Here we add scopes on servives
            // Example: services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IStudentService, StudentService>();

            return services;
        }
    }
}
