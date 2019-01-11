using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Schedule.Business.Teacher
{
    public interface ITeacherService
    {
        Task<List<TeacherDetailsModel>> GetAll();

        Task<TeacherDetailsModel> FindById(Guid id);

        Task<TeacherDetailsModel> Login(string email, string pwd);

        Task<Guid> CreateNew(TeacherCreateModel newTeacher);

        Task<Guid> Update(Guid id, TeacherCreateModel updatedTeacher);

        Task Delete(Guid id);
    }
}
