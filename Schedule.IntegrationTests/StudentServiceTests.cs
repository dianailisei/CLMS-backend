using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Schedule.Business.Student;
using Schedule.Persistance;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Schedule.IntegrationTests
{
    [TestClass]
    public class StudentServiceTests
    {
        StudentService studentService;
        ScheduleContext context;
        [TestInitialize]
        public void Initialize()
        {
            string connectionString = "Server=den1.mssql7.gear.host; Database=dotnottests;User Id=dotnottests;Password=Ky4DwF?7-YQY;";
            var builder = new DbContextOptionsBuilder<ScheduleContext>().UseSqlServer(connectionString);
            context = new ScheduleContext(builder.Options);
            studentService = new StudentService(context, context);
        }

        [TestMethod]
        public async Task CreateNew_InsertOneStudent_ReturnStudentId()
        {
            //Arrange
            StudentCreateModel model = new StudentCreateModel()
            {
                FirstName = "Codrut",
                LastName = "Iftimie",
                Email = "codrut.iftimie@gmail.com",
                Password = "complicat",
                Group = "A1",
                Year = 3
            };

            //Act
            var result = await studentService.CreateNew(model);

            //Assert
            Assert.IsInstanceOfType(result, typeof(System.Guid));
        }

        [TestMethod]
        public async Task GetAll_ReturnListOfStudents()
        {
            //Act
            var result = await studentService.GetAll();

            //Assert
            Assert.IsInstanceOfType(result, typeof(List<StudentDetailsModel>));
        }

        [TestMethod]
        public async Task FindById_ReturnStudentDetailModel()
        {
            //Arrange
            var studentsList = await studentService.GetAll();
            var lastStudent = studentsList[studentsList.Count - 1];

            //Act
            var result = await studentService.FindById(lastStudent.Id);

            //Assert
            Assert.IsInstanceOfType(result, typeof(StudentDetailsModel));
        }

        [TestMethod]
        public async Task UpdateStudent_GivenUpdatedStudent_ReturnStudentId()
        {
            //Arrange
            var studentsList = await studentService.GetAll();
            var lastStudent = studentsList[studentsList.Count - 1];

            var updatedStudent = new StudentCreateModel()
            {
                FirstName = lastStudent.FirstName,
                LastName = lastStudent.LastName,
                Email = "updated@email.com",
                Password = lastStudent.Password,
                Group = "A2",
                Year = lastStudent.Year
            };

            //Act
            var result = await studentService.Update(lastStudent.Id, updatedStudent);

            //Assert
            Assert.IsInstanceOfType(result, typeof(System.Guid));
        }

        [TestMethod]
        public async Task DeleteStudent_GivenStudentId_ReturnListWithoutDeletedStudent()
        {
            //Arrange
            var students = await studentService.GetAll();
            var studentsCount = students.Count;
            var lastStudent = students[students.Count - 1];

            //Act
            await studentService.Delete(lastStudent.Id);
            var newStudents = await studentService.GetAll();
            var newStudentsCount = newStudents.Count;

            //Assert
            Assert.AreEqual(studentsCount - 1, newStudentsCount);
        }
    }
}
