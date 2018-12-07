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
        private readonly IRepository repository;

        public SubjectService(IRepository repository) => this.repository = repository;

        public async Task<List<SubjectDetailsModel>> GetAllSubjects() => await GetAllSubjectsDetails().Where(s => s.Name != null).ToListAsync();

        public async Task<SubjectDetailsModel> FindById(Guid id) => await GetAllSubjectsDetails().SingleOrDefaultAsync(s => s.Id == id);

        public async Task<Guid> CreateNew(SubjectCreateModel newSubject)
        {
            var subject = Schedule.Domain.Entities.Subject.Create(
                name: newSubject.Name,
                laboratories: newSubject.Laboratories,
                lectures: newSubject.Lectures);

            await this.repository.AddNewAsync(subject);
            await this.repository.SaveAsync();

            return subject.Id;
        }

        private IQueryable<SubjectDetailsModel> GetAllSubjectsDetails() => repository.GetAll<Schedule.Domain.Entities.Subject>()
                             .Select(s => new SubjectDetailsModel
                             {
                                 Id = s.Id,
                                 Name = s.Name,
                                 Laboratories = s.Laboratories,
                                 Lectures = s.Lectures
                             });
    }
}
