using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Schedule.Business.Laboratory;
using Schedule.Business.Laboratory.Validations;
using Schedule.Business.Student;
using Schedule.Business.Teacher;
using Schedule.Business.Subject;
using Schedule.Business.Lecture;
using Schedule.Business.Lecture.Validations;
using Schedule.Business.Student.Validations;
using Schedule.Business.Subject.Validations;
using Schedule.Business.Teacher.Validations;

namespace Schedule.Business
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

            

            services.AddTransient<IValidator<StudentCreateModel>, StudentCreateModelValidator>();
            services.AddTransient<IValidator<TeacherCreateModel>, TeacherCreateModelValidator>();
            services.AddTransient<IValidator<SubjectCreateModel>, SubjectCreateModelValidator>();
            services.AddTransient<IValidator<LectureCreateModel>, LectureCreateModelValidator>();
            services.AddTransient<IValidator<LaboratoryCreateModel>, LaboratoryCreateModelValidator>();

            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ILaboratoryService, LaboratoryService>();
            services.AddScoped<ITeacherService, TeacherService>();
            services.AddScoped<ISubjectService, SubjectService>();
            services.AddScoped<ILectureService, LectureService>();

            return services;
        }
    }
}
