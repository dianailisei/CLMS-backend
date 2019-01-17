using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Schedule.Domain.Interfaces;

namespace Schedule.Business.Lecture
{
    public sealed class LectureService : ILectureService
    {
        private readonly IReadRepository _readRepository;
        private readonly IWriteRepository _writeRepository;

        public LectureService(IReadRepository _readRepository, IWriteRepository _writeRepository)
        {
            this._readRepository = _readRepository;
            this._writeRepository = _writeRepository;
        }

        public Task<List<LectureDetailsModel>> GetAll() => GetAllLecturesDetails().ToListAsync();
        public async Task<List<LectureDetailsModel>> GetLecturesByStudent(Guid id)
        {
            var student = await _readRepository.FindByIdAsync<Domain.Entities.Student>(id);
            
            var lectures = await GetAllLecturesDetails().Where(l => l.HalfYear.Equals(student.Group[0].ToString()) && l.ParentSubject.Year == student.Year).ToListAsync();

            return lectures;
        }

        public Task<LectureDetailsModel> FindById(Guid id) => GetAllLecturesDetails().SingleOrDefaultAsync(c => c.Id == id);

        public async Task<Guid> CreateNew(Guid teacherId, Guid subjectId, LectureCreateModel newLecture)
        {
            var teacher = await _readRepository.FindByIdAsync<Domain.Entities.Teacher>(teacherId);
            var subject = await _readRepository.FindByIdAsync<Domain.Entities.Subject>(subjectId);
            var lecture = Domain.Entities.Lecture.Create(newLecture.Name, newLecture.Weekday,
                newLecture.StartHour, newLecture.EndHour, newLecture.HalfYear, teacher, subject);

            await _writeRepository.AddNewAsync(lecture);
            await _writeRepository.SaveAsync();

            return lecture.Id;
        }

        public async Task<Guid> Update(Guid teacherId, Guid id, LectureCreateModel updatedLecture)
        {
            var teacher = await _readRepository.FindByIdAsync<Domain.Entities.Teacher>(teacherId);
            var exist = await _readRepository.FindByIdAsync<Domain.Entities.Lecture>(id);
            if (exist != null)
            {
                exist.Update(updatedLecture.Name, updatedLecture.Weekday,
                    updatedLecture.StartHour, updatedLecture.EndHour, updatedLecture.HalfYear, teacher);
                await _writeRepository.UpdateAsync(id, exist);
                await _writeRepository.SaveAsync();
            }
            return exist.Id;
        }

        public async Task Delete(Guid id)
        {
            await _writeRepository.DeleteByIdAsync<Domain.Entities.Lecture>(id);
            await _writeRepository.SaveAsync();
        }

        private IQueryable<LectureDetailsModel> GetAllLecturesDetails() => _readRepository.GetAll<Domain.Entities.Lecture>()
            .Select(l => new LectureDetailsModel
            {
                Id = l.Id,
                Name = l.Name,
                Weekday = l.Weekday,
                StartHour = l.StartHour,
                EndHour = l.EndHour,
                HalfYear = l.HalfYear,
                Lecturer = l.Lecturer,
                ParentSubject = l.ParentSubject
            });
    }
}
