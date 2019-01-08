using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Schedule.Business.Laboratory;
using Schedule.Business.Subject;
using Schedule.Persistance;
using Schedule.Business.Teacher;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Schedule.IntegrationTests
{
    [TestClass]
    public class LaboratoryServiceTests
    {
        LaboratoryService laboratoryService;
        ScheduleContext context;
        [TestInitialize]
        public void Initialize()
        {
            string connectionString = "Server=den1.mssql7.gear.host; Database=dotnottests;User Id=dotnottests;Password=Ky4DwF?7-YQY;";
            var builder = new DbContextOptionsBuilder<ScheduleContext>().UseSqlServer(connectionString);
            context = new ScheduleContext(builder.Options);
            laboratoryService = new LaboratoryService(context, context);
        }

        [TestMethod]
        public async Task CreateNew_InsertOneLaboratory_ReturnLaboratoryId()
        {
            //Arrange

            var teacherService = new TeacherService(context, context);
            var teachers = await teacherService.GetAll();
            var teacherId = teachers[teachers.Count - 1].Id;

            var subjectService = new SubjectService(context, context);
            var subjects = await subjectService.GetAllSubjects();
            var subjectId = subjects[subjects.Count - 1].Id;

            LaboratoryCreateModel model = new LaboratoryCreateModel()
            {
                Name = "Laboratory .NET",
                Weekday = "Wednesday",
                StartHour = 10,
                EndHour = 12,
                Group = "A2"
            };

            //Act
            var result = await laboratoryService.CreateNew(teacherId, subjectId, model);

            //Assert
            Assert.IsInstanceOfType(result, typeof(System.Guid));
        }

        [TestMethod]
        public async Task GetAll_ReturnListOfLaboratorys()
        {
            //Act
            var result = await laboratoryService.GetAll();

            //Assert
            Assert.IsInstanceOfType(result, typeof(List<LaboratoryDetailsModel>));
        }

        [TestMethod]
        public async Task FindById_ReturnLaboratoryDetailModel()
        {
            //Arrange
            var laboratorysList = await laboratoryService.GetAll();
            var lastLaboratory = laboratorysList[laboratorysList.Count - 1];

            //Act
            var result = await laboratoryService.FindById(lastLaboratory.Id);

            //Assert
            Assert.IsInstanceOfType(result, typeof(LaboratoryDetailsModel));
        }

        [TestMethod]
        public async Task UpdateLaboratory_GivenUpdatedLaboratory_ReturnLaboratoryId()
        {
            //Arrange
            var teacherService = new TeacherService(context, context);
            var teachers = await teacherService.GetAll();
            var teacherId = teachers[teachers.Count - 1].Id;

            var laboratorysList = await laboratoryService.GetAll();
            var lastLaboratory = laboratorysList[laboratorysList.Count - 1];

            var updatedLaboratory = new LaboratoryCreateModel()
            {
                Name = lastLaboratory.Name,
                Weekday = "Friday",
                StartHour = 16,
                EndHour = 18,
                Group = lastLaboratory.Group
            };

            //Act
            var result = await laboratoryService.Update(teacherId, lastLaboratory.Id, updatedLaboratory);

            //Assert
            Assert.IsInstanceOfType(result, typeof(System.Guid));
        }

        [TestMethod]
        public async Task DeleteLaboratory_GivenLaboratoryId_ReturnListWithoutDeletedLaboratory()
        {
            //Arrange
            var laboratorys = await laboratoryService.GetAll();
            var laboratorysCount = laboratorys.Count;
            var lastLaboratory = laboratorys[laboratorys.Count - 1];

            //Act
            await laboratoryService.Delete(lastLaboratory.Id);
            var newLaboratorys = await laboratoryService.GetAll();
            var newLaboratorysCount = newLaboratorys.Count;

            //Assert
            Assert.AreEqual(laboratorysCount - 1, newLaboratorysCount);
        }
    }
}
