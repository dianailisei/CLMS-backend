using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Schedule.Business.Subject;
using Schedule.Business.Teacher;
using Schedule.Persistance;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Schedule.UnitTests
{
    [TestClass]
    public class SubjectsControllerUnitTest
    {
        SubjectService subjectService;
        ScheduleContext context;
        [TestInitialize]
        public void Initialize()
        {
            var builder = new DbContextOptionsBuilder<ScheduleContext>().UseSqlServer("Server=localhost;Database=CLMS_tests;Trusted_Connection=True;");
            context = new ScheduleContext(builder.Options);
            subjectService = new SubjectService(context, context);
        }

        [TestMethod]
        public async Task CreateNew_InsertOneSubject_ReturnSubjectId()
        {
            TeacherService teacherService = new TeacherService(context,context);
            TeacherCreateModel model = new TeacherCreateModel()
            {
                FirstName = "Andrei",
                LastName = "Arusoaie",
                Email = "andrei@arusoaie.ro",
                Password = "123asd"
            };
            var teacherId = await teacherService.CreateNew(model);
            SubjectCreateModel subject = new SubjectCreateModel()
            {
                Name = "Python"
            };
            var result = await subjectService.CreateNew(teacherId, subject);
            Assert.IsInstanceOfType(result, typeof(System.Guid));
        }

        [TestMethod]
        public async Task GetAllSubjects_ReturnListOfSubjects()
        {
            var result = await subjectService.GetAllSubjects();
            Assert.IsInstanceOfType(result, typeof(List<SubjectDetailsModel>));
        }

        [TestMethod]
        public async Task FindById_ReturnSubjectDetailModel()
        {
            var subjectsList = await subjectService.GetAllSubjects();
            var lastSubject = subjectsList[subjectsList.Count - 1];
            var result = await subjectService.FindById(lastSubject.Id);
            Assert.IsInstanceOfType(result, typeof(SubjectDetailsModel));
        }

        [TestMethod]
        public async Task UpdateSubject_GivenUpdatedSubject_ReturnSubjectId()
        {
            var subjectsList = await subjectService.GetAllSubjects();
            var lastSubject = subjectsList[subjectsList.Count - 1];
            var subjectDetails = await subjectService.FindById(lastSubject.Id);
            subjectDetails.Name = "Updated";
            var updatedSubject = new SubjectCreateModel()
            {
                Name = subjectDetails.Name
            };
            var result = await subjectService.Update(subjectDetails.Id, updatedSubject);
            Assert.IsInstanceOfType(result, typeof(System.Guid));
        }

        [TestMethod]
        public async Task DeleteSubject_GivenSubjectId_ReturnListWithoutDeletedSubject()
        {
            var subjects = await subjectService.GetAllSubjects();
            var subjectsCount = subjects.Count;
            var lastSubject = subjects[subjects.Count - 1];
            await subjectService.Delete(lastSubject.Id);
            var newSubjects = await subjectService.GetAllSubjects();
            var newSubjectsCount = newSubjects.Count;
            Assert.AreEqual(subjectsCount - 1, newSubjectsCount);
        }
    }
}
