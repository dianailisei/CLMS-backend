﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Schedule.Domain.Interfaces;

namespace Schedule.Business.Lecture
{
    public sealed class LectureService : ILectureService
    {
        private readonly IRepository repository;

        public LectureService(IRepository repository) => this.repository = repository;

        public Task<List<LectureDetailsModel>> GetAll() => GetAllLecturesDetails().ToListAsync();

        public Task<LectureDetailsModel> FindById(Guid id) => GetAllLecturesDetails().SingleOrDefaultAsync(c => c.Id == id);

        public async Task<Guid> CreateNew(Guid teacherId, Guid subjectId, LectureCreateModel newLecture)
        {
            var teacher = await repository.FindByIdAsync<Domain.Entities.Teacher>(teacherId);
            var subject = await repository.FindByIdAsync<Domain.Entities.Subject>(subjectId);
            var lecture = Domain.Entities.Lecture.Create(newLecture.Name, newLecture.Weekday,
                newLecture.StartHour, newLecture.EndHour, newLecture.HalfYear, teacher, subject);

            await repository.AddNewAsync(lecture);
            await repository.SaveAsync();

            return lecture.Id;
        }

        public async Task<Guid> Update(Guid teacherId, Guid id, LectureCreateModel updatedLecture)
        {
            var teacher = await repository.FindByIdAsync<Domain.Entities.Teacher>(teacherId);
            var exist = await repository.FindByIdAsync<Domain.Entities.Lecture>(id);
            if (exist != null)
            {
                exist.Update(updatedLecture.Name, updatedLecture.Weekday,
                    updatedLecture.StartHour, updatedLecture.EndHour, updatedLecture.HalfYear, teacher);
                await repository.UpdateAsync(id, exist);
                await repository.SaveAsync();
            }
            return exist.Id;
        }

        public async Task Delete(Guid id)
        {
            await repository.DeleteByIdAsync<Domain.Entities.Lecture>(id);
            await repository.SaveAsync();
        }

        private IQueryable<LectureDetailsModel> GetAllLecturesDetails() => repository.GetAll<Domain.Entities.Lecture>()
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
