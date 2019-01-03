using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Schedule.Business.Teacher;
using Schedule.Persistance;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Schedule.IntegrationTests
{
    [TestClass]
    public class TeacherServiceTests
    {
        TeacherService teacherService;
        ScheduleContext context;
        [TestInitialize]
        public void Initialize()
        {
            string connectionString = "Server=den1.mssql7.gear.host; Database=dotnottests;User Id=dotnottests;Password=Ky4DwF?7-YQY;";
            var builder = new DbContextOptionsBuilder<ScheduleContext>().UseSqlServer(connectionString);
            context = new ScheduleContext(builder.Options);
            teacherService = new TeacherService(context, context);
        }

        [TestMethod]
        public async Task CreateNew_InsertOneTeacher_ReturnTeacherId()
        {
            //Arrange
            TeacherCreateModel model = new TeacherCreateModel()
            {
                FirstName = "Dragos",
                LastName = "Gavrilut",
                Email = "dg@gavrilut.ro",
                Password = "123asd1"
            };

            //Act
            var result = await teacherService.CreateNew(model);

            //Assert
            Assert.IsInstanceOfType(result, typeof(System.Guid));
        }

        [TestMethod]
        public async Task GetAll_ReturnListOfTeachers()
        {
            //Act
            var result = await teacherService.GetAll();

            //Assert
            Assert.IsInstanceOfType(result, typeof(List<TeacherDetailsModel>));
        }

        [TestMethod]
        public async Task FindById_ReturnTeacherDetailModel()
        {
            //Arrange
            var teachersList = await teacherService.GetAll();
            var lastTeacher = teachersList[teachersList.Count - 1];

            //Act
            var result = await teacherService.FindById(lastTeacher.Id);

            //Assert
            Assert.IsInstanceOfType(result, typeof(TeacherDetailsModel));
        }

        [TestMethod]
        public async Task UpdateTeacher_GivenUpdatedTeacher_ReturnTeacherId()
        {
            //Arrange
            var teachersList = await teacherService.GetAll();
            var lastTeacher = teachersList[teachersList.Count - 1];

            var updatedTeacher = new TeacherCreateModel()
            {
                FirstName = lastTeacher.FirstName,
                LastName = lastTeacher.LastName,
                Email = "updated@email.com",
                Password = lastTeacher.Password
            };

            //Act
            var result = await teacherService.Update(lastTeacher.Id, updatedTeacher);

            //Assert
            Assert.IsInstanceOfType(result, typeof(System.Guid));
        }

        [TestMethod]
        public async Task DeleteTeacher_GivenTeacherId_ReturnListWithoutDeletedTeacher()
        {
            //Arrange
            var teachers = await teacherService.GetAll();
            var teachersCount = teachers.Count;
            var lastTeacher = teachers[teachers.Count - 1];

            //Act
            await teacherService.Delete(lastTeacher.Id);
            var newTeachers = await teacherService.GetAll();
            var newTeachersCount = newTeachers.Count;

            //Assert
            Assert.AreEqual(teachersCount - 1, newTeachersCount);
        }
    }
}
