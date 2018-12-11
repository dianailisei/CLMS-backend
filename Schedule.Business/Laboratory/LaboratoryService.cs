using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Schedule.Domain.Interfaces;

namespace Schedule.Business.Laboratory
{
    public sealed class LaboratoryService : ILaboratoryService
    {
        private readonly IRepository _repository;

        public LaboratoryService(IRepository repository) => _repository = repository;

        public Task<List<LaboratoryDetailsModel>> GetAll() => GetAllLaboratoriesDetails().ToListAsync();

        public Task<LaboratoryDetailsModel> FindById(Guid id) => GetAllLaboratoriesDetails().SingleOrDefaultAsync(lab => lab.Id == id);

        public async Task<Guid> CreateNew(Guid teacherId, Guid subjectId, LaboratoryCreateModel newLaboratory)
        {
            var teacher = await _repository.FindByIdAsync<Domain.Entities.Teacher>(teacherId);
            var subject = await _repository.FindByIdAsync<Domain.Entities.Subject>(subjectId);
            var lab = Domain.Entities.Laboratory.Create(newLaboratory.Name, newLaboratory.Group, teacher,
                newLaboratory.Weekday, newLaboratory.StartHour, newLaboratory.EndHour, subject);
            await _repository.AddNewAsync(lab);
            await _repository.SaveAsync();

            return lab.Id;
        }

        private IQueryable<LaboratoryDetailsModel> GetAllLaboratoriesDetails()
        {
            return _repository
                        .GetAll<Domain.Entities.Laboratory>().Select(lab => new LaboratoryDetailsModel
                        {
                        Id = lab.Id,
                        Name = lab.Name,
                        Weekday = lab.Weekday,
                        StartHour = lab.StartHour,
                        EndHour = lab.EndHour,
                        Group = lab.Group,
                        Teacher = lab.Teacher
                        });
        }

        public async Task<Guid> Update(Guid teacherId, Guid id, LaboratoryCreateModel updatedLaboratory)
        {
            var teacher = await _repository.FindByIdAsync<Domain.Entities.Teacher>(teacherId);
            var exist = await _repository.FindByIdAsync<Domain.Entities.Laboratory>(id);
            if (exist != null)
            {
                exist.Update(updatedLaboratory.Name, updatedLaboratory.Group,
                    teacher, updatedLaboratory.Weekday, updatedLaboratory.StartHour, updatedLaboratory.EndHour);
                await _repository.UpdateAsync(id, exist);
                await _repository.SaveAsync();
            }
            return exist.Id;
        }

        public async Task Delete(Guid id)
        {
            await _repository.DeleteByIdAsync<Domain.Entities.Laboratory>(id);
            await _repository.SaveAsync();
        }
    }
}
