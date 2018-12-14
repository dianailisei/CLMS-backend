using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Schedule.Business.Laboratory;
using Schedule.Business.Teacher;
using Schedule.Persistance;

namespace Schedule.UnitTests
{
    [TestClass]
    class LaboratoryUnitTests
    {
        private LaboratoryService _labService;
        private TeacherService _teacherService;

        [TestInitialize]
        public void Initialize()
        {
            var builder = new DbContextOptionsBuilder<ScheduleContext>().UseSqlServer("Server=den1.mssql8.gear.host; Database=dotnot;User Id=dotnot;Password=Do75j23S!1!v;");
            ScheduleContext context = new ScheduleContext(builder.Options);
            _labService = new LaboratoryService(context, context);
            _teacherService = new TeacherService(context, context);
        }
        [TestMethod]
        public async void GetAll_InsertLaboratory_ReturnLaboratory()
        {
            // Arrange
            var teacher = new TeacherCreateModel()
            {
                FirstName = "Ion",
                LastName = "Popescu",
                Password = "1234"
            };

            await _teacherService.CreateNew(teacher);

            var laboratory = new LaboratoryCreateModel()
            {
                Name = "Laborator2",
                Group = "A1",
                Weekday = "Thursday",
                StartHour = 12,
                EndHour = 14
            };

            // Act
             // await _labService.CreateNew(laboratory);
            

            // Assert
           

        }
    }
}
