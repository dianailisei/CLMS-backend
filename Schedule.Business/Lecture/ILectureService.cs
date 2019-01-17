using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Schedule.Business.Lecture
{
    public interface ILectureService
    {
        Task<Guid> CreateNew(Guid teacherId, Guid subjectId, LectureCreateModel newLecture);
        Task Delete(Guid id);
        Task<LectureDetailsModel> FindById(Guid id);
        Task<List<LectureDetailsModel>> GetAll();
        Task<List<LectureDetailsModel>> GetLecturesByStudent(Guid id);
        Task<Guid> Update(Guid teacherId, Guid id, LectureCreateModel updatedLecture);
    }
}