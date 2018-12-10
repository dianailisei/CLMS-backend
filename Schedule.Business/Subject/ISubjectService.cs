using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Schedule.Business.Subject
{
    public interface ISubjectService
    {
        Task<SubjectDetailsModel> FindById(Guid id);

        Task<Guid> CreateNew(SubjectCreateModel newSubject);

        Task<Guid> Update(Guid id, SubjectCreateModel updatedSubject);

        Task Delete(Guid id);

        Task<List<SubjectDetailsModel>> GetAllSubjects();
    }
}
