using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Schedule.Business.Teacher
{
    public interface ITeacherService
    {
        Task<List<TeacherDetailsModel>> GetAll();

        Task<TeacherDetailsModel> FindById(Guid id);

        Task<Guid> CreateNew(TeacherCreateModel newTeacher);
    }
}
