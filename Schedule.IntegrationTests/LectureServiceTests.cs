using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Schedule.Business.Lecture;
using Schedule.Business.Subject;
using Schedule.Persistance;
using Schedule.Business.Teacher;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Schedule.IntegrationTests
{
    [TestClass]
    public class LectureServiceTests
    {
        LectureService lectureService;
        ScheduleContext context;
        [TestInitialize]
        public void Initialize()
        {
            string connectionString = "Server=den1.mssql7.gear.host; Database=dotnottests;User Id=dotnottests;Password=Ky4DwF?7-YQY;";
            var builder = new DbContextOptionsBuilder<ScheduleContext>().UseSqlServer(connectionString);
            context = new ScheduleContext(builder.Options);
            lectureService = new LectureService(context, context);
        }

        [TestMethod]
        public async Task CreateNew_InsertOneLecture_ReturnLectureId()
        {
            //Arrange

            var teacherService = new TeacherService(context, context);
            var teachers = await teacherService.GetAll();
            var teacherId = teachers[teachers.Count - 1].Id;

            var subjectService = new SubjectService(context, context);
            var subjects = await subjectService.GetAllSubjects();
            var subjectId = subjects[subjects.Count - 1].Id;

            LectureCreateModel model = new LectureCreateModel()
            {
                Name = "Course .NET",
                Weekday = "Wednesday",
                StartHour = 8,
                EndHour = 10,
                HalfYear = "A"
            };

            //Act
            var result = await lectureService.CreateNew(teacherId, subjectId, model);

            //Assert
            Assert.IsInstanceOfType(result, typeof(System.Guid));
        }

        [TestMethod]
        public async Task GetAll_ReturnListOfLectures()
        {
            //Act
            var result = await lectureService.GetAll();

            //Assert
            Assert.IsInstanceOfType(result, typeof(List<LectureDetailsModel>));
        }

        [TestMethod]
        public async Task FindById_ReturnLectureDetailModel()
        {
            //Arrange
            var lecturesList = await lectureService.GetAll();
            var lastLecture = lecturesList[lecturesList.Count - 1];

            //Act
            var result = await lectureService.FindById(lastLecture.Id);

            //Assert
            Assert.IsInstanceOfType(result, typeof(LectureDetailsModel));
        }

        [TestMethod]
        public async Task UpdateLecture_GivenUpdatedLecture_ReturnLectureId()
        {
            //Arrange
            var teacherService = new TeacherService(context, context);
            var teachers = await teacherService.GetAll();
            var teacherId = teachers[teachers.Count - 1].Id;

            var lecturesList = await lectureService.GetAll();
            var lastLecture = lecturesList[lecturesList.Count - 1];

            var updatedLecture = new LectureCreateModel()
            {
                Name = lastLecture.Name,
                Weekday = "Friday",
                StartHour = 14,
                EndHour = 16,
                HalfYear = lastLecture.HalfYear
            };

            //Act
            var result = await lectureService.Update(teacherId, lastLecture.Id, updatedLecture);

            //Assert
            Assert.IsInstanceOfType(result, typeof(System.Guid));
        }

        [TestMethod]
        public async Task DeleteLecture_GivenLectureId_ReturnListWithoutDeletedLecture()
        {
            //Arrange
            var lectures = await lectureService.GetAll();
            var lecturesCount = lectures.Count;
            var lastLecture = lectures[lectures.Count - 1];

            //Act
            await lectureService.Delete(lastLecture.Id);
            var newLectures = await lectureService.GetAll();
            var newLecturesCount = newLectures.Count;

            //Assert
            Assert.AreEqual(lecturesCount - 1, newLecturesCount);
        }
    }
}
