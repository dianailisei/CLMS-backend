using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Schedule.Business.Laboratory
{
    public interface ILaboratoryService 
    {
        Task<List<LaboratoryDetailsModel>> GetAll();

        Task<LaboratoryDetailsModel> FindById(Guid id);

        Task<Guid> CreateNew(Guid teacherId, Guid subjectId, LaboratoryCreateModel newLaboratory);

        Task<Guid> Update(Guid teacherId, Guid id, LaboratoryCreateModel newLaboratory);

        Task Delete(Guid id);
    }
}
