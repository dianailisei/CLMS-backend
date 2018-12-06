﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Schedule.Domain.Interfaces;

namespace Schedule.Business.Laboratory
{
    class LaboratoryService : ILaboratoryService
    {
        private readonly IRepository _repository;

        public LaboratoryService(IRepository repository) => _repository = repository;

        public Task<List<LaboratoryDetailsModel>> GetAll() => GetAllLaboratoriesDetails().ToListAsync();
        

        public Task<LaboratoryDetailsModel> FindById(Guid id) => GetAllLaboratoriesDetails().SingleOrDefaultAsync(lab => lab.Id == id);

        public async Task<Guid> CreateNew(LaboratoryCreateModel newLaboratory)
        {
            var lab = Domain.Entities.Laboratory.Create(newLaboratory.Name, newLaboratory.Group, newLaboratory.Teacher,
                newLaboratory.Weekday, newLaboratory.StartHour, newLaboratory.EndHour);
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
    }
}