using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using FluentValidation;
using Trivia.Business.Question;
using Trivia.Business.Answer;
using Trivia.Business.Question.Validations;
using Trivia.Business.Answer.Validations;

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

            services.AddTransient<IValidator<QuestionCreateModel>, QuestionCreateModelValidator>();
            services.AddTransient<IValidator<AnswerCreateModel>, AnswerCreateModelValidator>();

            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<IAnswerService, AnswerService>();

            return services;
        }
    }
}