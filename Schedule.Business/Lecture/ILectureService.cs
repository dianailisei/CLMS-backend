using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Schedule.Business.Lecture
{
    public interface ILectureService
    {
        Task<Guid> CreateNew(LectureCreateModel newLecture);
        Task Delete(Guid id);
        Task<LectureDetailsModel> FindById(Guid id);
        Task<List<LectureDetailsModel>> GetAll();
        Task<Guid> Update(Guid id, LectureCreateModel updatedLecture);
    }
}