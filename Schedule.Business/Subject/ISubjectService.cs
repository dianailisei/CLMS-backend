using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Schedule.Business.Subject
{
    public interface ISubjectService
    {
        Task<SubjectDetailsModel> FindById(Guid id);

        Task<Guid> CreateNew(Guid teacherGuid, SubjectCreateModel newSubject);

        Task<List<SubjectDetailsModel>> GetAllByTeacherId(Guid teacherGuid);

        Task<List<SubjectDetailsModel>> GetSubjectsByStudent(Guid studentGuid);

        Task<List<SubjectDetailsModel>> GetAllSubjects();

        Task<List<SubjectDetailsModel>> GetSubjectsByTeacher(Guid teacherId);

        Task<Guid> Update(Guid id, SubjectCreateModel updatedSubject);

        Task Delete(Guid id);

        Task DeleteAllByTeacherId(Guid id);

    }
}
