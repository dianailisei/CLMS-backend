using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Schedule.Domain.Interfaces;

namespace Schedule.Business.Student
{
    public sealed class StudentService : IStudentService
    {
        private readonly IRepository repository;

        public StudentService(IRepository repository) => this.repository = repository;

        public Task<List<StudentDetailsModel>> GetAll() => GetAllStudentsDetails().ToListAsync();

        public Task<StudentDetailsModel> FindById(Guid id) => GetAllStudentsDetails().SingleOrDefaultAsync(c => c.Id == id);

        public async Task<Guid> CreateNew(StudentCreateModel newStudent)
        {
            var student = Domain.Entities.Student.Create(newStudent.FirstName, newStudent.LastName,
                newStudent.Email,newStudent.Password, newStudent.Group, newStudent.Year);

            await repository.AddNewAsync(student);
            await repository.SaveAsync();

            return student.Id;
        }

        private IQueryable<StudentDetailsModel> GetAllStudentsDetails() => repository.GetAll<Domain.Entities.Student>()
            .Select(s => new StudentDetailsModel
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Email = s.Email,
                Password = s.Password,
                Group = s.Group,
                Year = s.Year
            });
    }
}
