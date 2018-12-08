using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Schedule.Business.Laboratory
{
    public interface ILaboratoryService 
    {
        Task<List<LaboratoryDetailsModel>> GetAll();

        Task<LaboratoryDetailsModel> FindById(Guid id);

        Task<Guid> CreateNew(LaboratoryCreateModel newLaboratory);

        Task Delete(Guid id);

        Task<Guid> Update(Guid id, LaboratoryCreateModel updatedLaboratory);
    }
}
