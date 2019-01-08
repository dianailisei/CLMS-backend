using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Schedule.Business.Subject;
using Schedule.Business.Teacher;
using Schedule.Persistance;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Schedule.IntegrationTests
{
    [TestClass]
    public class SubjectServiceTests
    {
        SubjectService subjectService;
        ScheduleContext context;
        [TestInitialize]
        public void Initialize()
        {
            string connectionString = "Server=den1.mssql7.gear.host; Database=dotnottests;User Id=dotnottests;Password=Ky4DwF?7-YQY;";
            var builder = new DbContextOptionsBuilder<ScheduleContext>().UseSqlServer(connectionString);
            context = new ScheduleContext(builder.Options);
            subjectService = new SubjectService(context, context);
        }

        [TestMethod]
        public async Task CreateNew_InsertOneSubject_ReturnSubjectId()
        {
            //Arrange
            TeacherService teacherService = new TeacherService(context, context);
            TeacherCreateModel model = new TeacherCreateModel()
            {
                FirstName = "Dragos",
                LastName = "Gavrilut",
                Email = "dg@gavrilut.ro",
                Password = "123asd1"
            };
            var teacherId = await teacherService.CreateNew(model);
            SubjectCreateModel subject = new SubjectCreateModel()
            {
                Name = "C++"
            };

            //Act
            var result = await subjectService.CreateNew(teacherId, subject);

            //Assert
            Assert.IsInstanceOfType(result, typeof(System.Guid));
        }

        [TestMethod]
        public async Task GetAllSubjects_ReturnListOfSubjects()
        {
            //Act
            var result = await subjectService.GetAllSubjects();
            
            //Assert
            Assert.IsInstanceOfType(result, typeof(List<SubjectDetailsModel>));
        }

        [TestMethod]
        public async Task FindById_ReturnSubjectDetailModel()
        {
            //Arrange
            var subjectsList = await subjectService.GetAllSubjects();
            var lastSubject = subjectsList[subjectsList.Count - 1];

            //Act
            var result = await subjectService.FindById(lastSubject.Id);

            //Assert
            Assert.IsInstanceOfType(result, typeof(SubjectDetailsModel));
        }

        [TestMethod]
        public async Task UpdateSubject_GivenUpdatedSubject_ReturnSubjectId()
        {
            //Arrange
            var subjectsList = await subjectService.GetAllSubjects();
            var lastSubject = subjectsList[subjectsList.Count - 1];

            var updatedSubject = new SubjectCreateModel()
            {
                Name = "Updated"
            };

            //Act
            var result = await subjectService.Update(lastSubject.Id, updatedSubject);

            //Assert
            Assert.IsInstanceOfType(result, typeof(System.Guid));
        }

        [TestMethod]
        public async Task DeleteSubject_GivenSubjectId_ReturnListWithoutDeletedSubject()
        {
            //Arrange
            var subjects = await subjectService.GetAllSubjects();
            var subjectsCount = subjects.Count;
            var lastSubject = subjects[subjects.Count - 1];

            //Act
            await subjectService.Delete(lastSubject.Id);
            var newSubjects = await subjectService.GetAllSubjects();
            var newSubjectsCount = newSubjects.Count;

            //Assert
            Assert.AreEqual(subjectsCount - 1, newSubjectsCount);
        }
    }
}
