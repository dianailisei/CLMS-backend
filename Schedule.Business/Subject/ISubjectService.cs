using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Schedule.Business.Subject
{
    public interface ISubjectService
    {
        Task<SubjectDetailsModel> FindById(Guid id);

        Task<Guid> CreateNew(Guid teacherGuid, SubjectCreateModel newSubject);

        Task<List<SubjectDetailsModel>> GetAllSubjects();

        Task<Guid> Update(Guid id, SubjectCreateModel updatedSubject);

        Task Delete(Guid id);

    }
}
