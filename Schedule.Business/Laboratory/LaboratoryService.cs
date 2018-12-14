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
        private readonly IReadRepository _readRepository;
        private readonly IWriteRepository _writeRepository;

        public LaboratoryService(IReadRepository readRepository, IWriteRepository writeRepository)
        {
            this._readRepository = readRepository;
            this._writeRepository = writeRepository;
        }

        public Task<List<LaboratoryDetailsModel>> GetAll() => GetAllLaboratoriesDetails().ToListAsync();

        public Task<LaboratoryDetailsModel> FindById(Guid id) => GetAllLaboratoriesDetails().SingleOrDefaultAsync(lab => lab.Id == id);

        public async Task<Guid> CreateNew(Guid teacherId, Guid subjectId, LaboratoryCreateModel newLaboratory)
        {
            var teacher = await _readRepository.FindByIdAsync<Domain.Entities.Teacher>(teacherId);
            var subject = await _readRepository.FindByIdAsync<Domain.Entities.Subject>(subjectId);
            var lab = Domain.Entities.Laboratory.Create(newLaboratory.Name, newLaboratory.Group, teacher,
                newLaboratory.Weekday, newLaboratory.StartHour, newLaboratory.EndHour, subject);
            await _writeRepository.AddNewAsync(lab);
            await _writeRepository.SaveAsync();

            return lab.Id;
        }

        private IQueryable<LaboratoryDetailsModel> GetAllLaboratoriesDetails()
        {
            return _readRepository
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
            var teacher = await _readRepository.FindByIdAsync<Domain.Entities.Teacher>(teacherId);
            var exist = await _readRepository.FindByIdAsync<Domain.Entities.Laboratory>(id);
            if (exist != null)
            {
                exist.Update(updatedLaboratory.Name, updatedLaboratory.Group,
                    teacher, updatedLaboratory.Weekday, updatedLaboratory.StartHour, updatedLaboratory.EndHour);
                await _writeRepository.UpdateAsync(id, exist);
                await _writeRepository.SaveAsync();
            }
            return exist.Id;
        }

        public async Task Delete(Guid id)
        {
            await _writeRepository.DeleteByIdAsync<Domain.Entities.Laboratory>(id);
            await _writeRepository.SaveAsync();
        }
    }
}
