using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Schedule.Domain.Interfaces;
using Schedule.Domain.Entities;

namespace Schedule.Business.Student
{
    class StudentService
    {
        private readonly IRepository repository;

        /*public StudentService(IRepository repository) => this.repository = repository;

        public Task<List<StudentDetailsModel>> GetAll() => GetAllStudentsDetails().ToListAsync();

        public Task<StudentDetailsModel> FindById(Guid id) => GetAllStudentsDetails().SingleOrDefaultAsync(c => c.Id == id);

        public async Task<Guid> CreateNew(StudentCreateModel newStudent)
        {
            var student = Domain.Entities.Student.Create(
                );

            await this.repository.AddNewAsync(customer);
            await this.repository.SaveAsync();

            return customer.Id;
        }

        private IQueryable<StudentDetailsModel> GetAllStudentsDetails() => this.repository.GetAll<Domain.Entities.Student>()
            .Select(c => new CustomerDetailsModel
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                MoneySpent = c.MoneySpent
            });*/
    }
}
