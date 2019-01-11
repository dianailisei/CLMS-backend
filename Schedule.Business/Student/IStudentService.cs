using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Schedule.Business.Student
{
    public interface IStudentService
    {
        Task<List<StudentDetailsModel>> GetAll();

        Task<StudentDetailsModel> FindById(Guid id);

        Task<StudentDetailsModel> Login(string email, string pwd);

        Task<Guid> CreateNew(StudentCreateModel newStudent);

        Task<Guid> Update(Guid id, StudentCreateModel updatedStudent);

        Task Delete(Guid id);
    }
}
