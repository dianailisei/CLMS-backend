using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;

namespace Trivia.Business
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services)
        {
            // Here we add scopes on servives
            // Example: services.AddScoped<ICustomerService, CustomerService>();
            // services.Ad
            services.AddMvc().AddFluentValidation(fv => {
                fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
            });

            return services;
        }
    }
}