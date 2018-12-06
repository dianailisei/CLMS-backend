using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Schedule.Domain.Interfaces;

namespace Schedule.Business.Teacher
{
    public sealed class TeacherService : ITeacherService
    {
        private readonly IRepository _repository;

        public TeacherService(IRepository repository) => _repository = repository;

        public Task<List<TeacherDetailsModel>> GetAll() => GetAllTeachersDetails().ToListAsync();

        public Task<TeacherDetailsModel> FindById(Guid id) => GetAllTeachersDetails().SingleOrDefaultAsync(c => c.Id == id);

        public async Task<Guid> CreateNew(TeacherCreateModel newTeacher)
        {
            var teacher = Domain.Entities.Teacher.Create(newTeacher.FirstName, newTeacher.LastName,
                newTeacher.Email, newTeacher.Password);

            await _repository.AddNewAsync(teacher);
            await _repository.SaveAsync();

            return teacher.Id;
        }

        private IQueryable<TeacherDetailsModel> GetAllTeachersDetails() => _repository.GetAll<Domain.Entities.Teacher>()
            .Select(t => new TeacherDetailsModel
            {
                Id = t.Id,
                FirstName = t.FirstName,
                LastName = t.LastName,
                Email = t.Email,
                Password = t.Password,
                Subjects = t.Subjects
            });
    }
}
