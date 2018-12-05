using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.Business.Student
{
    public interface IStudentService
    {
        Task<List<StudentDetailsModel>> GetAll();

        Task<StudentDetailsModel> FindById(Guid id);

        Task<Guid> CreateNew(StudentCreateModel newStudent);
    }
}
