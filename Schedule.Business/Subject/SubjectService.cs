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

        public async Task<List<SubjectDetailsModel>> GetAllSubjects() => await GetAllSubjectsDetails().ToListAsync();

        public async Task<SubjectDetailsModel> FindById(Guid id) => await GetAllSubjectsDetails().SingleOrDefaultAsync(s => s.Id == id);

        public async Task<Guid> CreateNew(SubjectCreateModel newSubject)
        {
            var subject = Domain.Entities.Subject.Create(
                name: newSubject.Name);

            await this.repository.AddNewAsync(subject);
            await this.repository.SaveAsync();

            return subject.Id;
        }

        public async Task<Guid> Update(Guid id, SubjectCreateModel updatedSubject)
        {
            var subject = await repository.FindByIdAsync<Domain.Entities.Subject>(id);
            if (subject != null)
            {
                subject.Update(updatedSubject.Name, updatedSubject.Lectures,
                    updatedSubject.Laboratories);
                await repository.UpdateAsync(id, subject);
                await repository.SaveAsync();
            }
            return subject.Id;
        }

        public async Task Delete(Guid id)
        {
            await repository.DeleteByIdAsync<Domain.Entities.Subject>(id);
            await repository.SaveAsync();
        }

        private IQueryable<SubjectDetailsModel> GetAllSubjectsDetails() => repository.GetAll<Domain.Entities.Subject>()
                             .Select(s => new SubjectDetailsModel
                             {
                                 Id = s.Id,
                                 Name = s.Name,
                                 Laboratories = s.Laboratories,
                                 Lectures = s.Lectures
                             });
    }
}
