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
        private readonly IReadRepository _readRepository;
        private readonly IWriteRepository _writeRepository;

        public TeacherService(IReadRepository readRepository, IWriteRepository writeRepository)
        {
            this._readRepository = readRepository;
            this._writeRepository = writeRepository;
        }

        public Task<List<TeacherDetailsModel>> GetAll() => GetAllTeachersDetails().ToListAsync();

        public Task<TeacherDetailsModel> FindById(Guid id) => GetAllTeachersDetails().SingleOrDefaultAsync(c => c.Id == id);

        public async Task<Guid> CreateNew(TeacherCreateModel newTeacher)
        {
            var teacher = Domain.Entities.Teacher.Create(newTeacher.FirstName, newTeacher.LastName,
                newTeacher.Email, newTeacher.Password);

            await _writeRepository.AddNewAsync(teacher);
            await _writeRepository.SaveAsync();

            return teacher.Id;
        }

        public async Task<Guid> Update(Guid id, TeacherCreateModel updatedTeacher)
        {
            var exist = await _readRepository.FindByIdAsync<Domain.Entities.Teacher>(id);
            if (exist != null)
            {
                exist.Update(updatedTeacher.FirstName, updatedTeacher.LastName,
                    updatedTeacher.Email, updatedTeacher.Password);
                await _writeRepository.UpdateAsync(id, exist);
                await _writeRepository.SaveAsync();
            }
            return exist.Id;
        }

        public async Task Delete(Guid id)
        {
            var teacher = await GetAllTeachersDetails().Include(t => t.Subjects).ThenInclude(t => t.Laboratories)
                .Include(t => t.Subjects).ThenInclude(t => t.Lectures).Where(t => t.Id == id).FirstOrDefaultAsync();


            await _writeRepository.DeleteByIdAsync<Domain.Entities.Teacher>(id);
            await _writeRepository.SaveAsync();
        }

        private IQueryable<TeacherDetailsModel> GetAllTeachersDetails() => _readRepository.GetAll<Domain.Entities.Teacher>()
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
