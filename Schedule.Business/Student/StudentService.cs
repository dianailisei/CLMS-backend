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
        private readonly IReadRepository readRepository;
        private readonly IWriteRepository writeRepository;

        public StudentService(IReadRepository readRepository, IWriteRepository writeRepository)
        {
            this.readRepository = readRepository;
            this.writeRepository = writeRepository;
        }

        public Task<List<StudentDetailsModel>> GetAll() => GetAllStudentsDetails().ToListAsync();

        public Task<StudentDetailsModel> FindById(Guid id) => GetAllStudentsDetails().SingleOrDefaultAsync(c => c.Id == id);

        public Task<StudentDetailsModel> Login(string email, string pwd) => GetAllStudentsDetails().FirstOrDefaultAsync(c => c.Email == email && c.Password == pwd);

        public async Task<Guid> CreateNew(StudentCreateModel newStudent)
        {
            var student = Domain.Entities.Student.Create(newStudent.FirstName, newStudent.LastName,
                newStudent.Email,newStudent.Password, newStudent.Group, newStudent.Year);

            await writeRepository.AddNewAsync(student);
            await writeRepository.SaveAsync();

            return student.Id;
        }

        public async Task<Guid> Update(Guid id, StudentCreateModel updatedStudent)
        {
            var exist = await readRepository.FindByIdAsync<Domain.Entities.Student>(id);
            if (exist != null)
            {
                exist.Update(updatedStudent.FirstName, updatedStudent.LastName,
                    updatedStudent.Email, updatedStudent.Password, updatedStudent.Group, updatedStudent.Year);
                await writeRepository.UpdateAsync(id, exist);
                await writeRepository.SaveAsync();
            }
            return exist.Id;
        }

        public async Task Delete(Guid id)
        {
            await writeRepository.DeleteByIdAsync<Domain.Entities.Student>(id);
            await writeRepository.SaveAsync();
        }

        private IQueryable<StudentDetailsModel> GetAllStudentsDetails() => readRepository.GetAll<Domain.Entities.Student>()
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
