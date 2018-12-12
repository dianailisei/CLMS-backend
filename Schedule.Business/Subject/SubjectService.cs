using Microsoft.EntityFrameworkCore;
using Schedule.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule.Business.Subject
{
    public sealed class SubjectService : ISubjectService
    {
        private readonly IReadRepository _readRepository;
        private readonly IWriteRepository _writeRepository;

        public SubjectService(IReadRepository readRepository, IWriteRepository writeRepository)
        {
            this._readRepository = readRepository;
            this._writeRepository = writeRepository;
        }

        public async Task<List<SubjectDetailsModel>> GetAllSubjects() => await GetAllSubjectsDetails().ToListAsync();

        public async Task<SubjectDetailsModel> FindById(Guid id) => await GetAllSubjectsDetails().SingleOrDefaultAsync(s => s.Id == id);

        public async Task<Guid> CreateNew(Guid teacherGuid, SubjectCreateModel newSubject)
        {
            var teacher = await _readRepository.FindByIdAsync<Domain.Entities.Teacher>(teacherGuid);
            var subject = Domain.Entities.Subject.Create(teacher, newSubject.Name);

            await this._writeRepository.AddNewAsync(subject);
            await this._writeRepository.SaveAsync();

            return subject.Id;
        }

        public async Task<Guid> Update(Guid id, SubjectCreateModel updatedSubject)
        {
            var subject = await _readRepository.FindByIdAsync<Domain.Entities.Subject>(id);
            if (subject != null)
            {
                subject.Update(updatedSubject.Name);
                await _writeRepository.UpdateAsync(id, subject);
                await _writeRepository.SaveAsync();
            }
            return subject.Id;
        }

        public async Task Delete(Guid id)
        {
            await _writeRepository.DeleteByIdAsync<Domain.Entities.Subject>(id);
            await _writeRepository.SaveAsync();
        }

        private IQueryable<SubjectDetailsModel> GetAllSubjectsDetails() => _readRepository.GetAll<Domain.Entities.Subject>()
            .Select(s => new SubjectDetailsModel
            {
                Id = s.Id,
                Name = s.Name,
                Laboratories = s.Laboratories,
                Lectures = s.Lectures,
                HeadOfDepartment = s.HeadOfDepartment
            });
        private IQueryable<SubjectDetailsModel> GetAllSubjectsDetailsByTeacherId(Guid teacherId) => _readRepository
            .GetAll<Domain.Entities.Subject>()
            .Where(s => s.HeadOfDepartment.Id == teacherId).Select(s => new SubjectDetailsModel
            {
                Id = s.Id,
                Name = s.Name,
                Laboratories = s.Laboratories,
                Lectures = s.Lectures,
                HeadOfDepartment = s.HeadOfDepartment
            });

        public async Task<List<SubjectDetailsModel>> GetAllByTeacherId(Guid teacherGuid)
        {
            return await GetAllSubjectsDetailsByTeacherId(teacherGuid).ToListAsync();
        }

        public async Task DeleteAllByTeacherId(Guid teacherGuid)
        {
            var subjects = await GetAllByTeacherId(teacherGuid);
            foreach (var subject in subjects)
                await _writeRepository.DeleteByIdAsync<Domain.Entities.Subject>(subject.Id);
            await _writeRepository.SaveAsync();
        }
    }
}
