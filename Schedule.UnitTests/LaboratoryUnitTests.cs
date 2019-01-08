using Microsoft.VisualStudio.TestTools.UnitTesting;
using Schedule.Domain.Entities;

namespace Schedule.UnitTests
{
    [TestClass]
    public class LaboratoryUnitTests
    {
        [TestMethod]
        public void Create_WhenGivenValidArguments_ShouldReturnNewLaboratoryWithGivenArguments()
        {
            //Arrange
            var teacher = Teacher.Create("Florin", "Olariu", "florin@olariu.ro", "parolagrea");
            var subject = Subject.Create(teacher, "Introducere in .NET");

            //Act
            var laboratory = Laboratory.Create("Laborator .NET", "A2", teacher, "Wednesday", 10, 12, subject);

            //Assert
            Assert.AreEqual("Laborator .NET", laboratory.Name);
        }

        [TestMethod]
        public void Update_WhenGivenValidArguments_ShouldUpdateExistingLaboratory()
        {
            //Arrange
            var teacher = Teacher.Create("Florin", "Olariu", "florin@olariu.ro", "parolagrea");
            var subject = Subject.Create(teacher, "Introducere in .NET");
            var laboratory = Laboratory.Create("Laborator .NET", "A2", teacher, "Wednesday", 10, 12, subject);

            //Act
            laboratory.Update(laboratory.Name, laboratory.Group, laboratory.Teacher, "Friday", laboratory.StartHour, laboratory.EndHour);

            //Assert
            Assert.AreEqual("Friday", laboratory.Weekday);
        }
    }
}
