using Microsoft.VisualStudio.TestTools.UnitTesting;
using Schedule.Domain.Entities;

namespace Schedule.UnitTests
{
    [TestClass]
    public class TeacherUnitTests
    {
        [TestMethod]
        public void Create_WhenGivenValidArguments_ShouldReturnNewTeacherWithGivenArguments()
        {
            //Act
            var teacher = Teacher.Create("Florin", "Olariu", "florin@olariu.ro", "parolagrea");

            //Assert
            Assert.AreEqual("Olariu", teacher.LastName);
        }

        [TestMethod]
        public void Update_WhenGivenValidArguments_ShouldUpdateExistingTeacher()
        {
            //Arrange
            var teacher = Teacher.Create("Florin", "Olariu", "florin@olariu.ro", "parolagrea");

            //Act
            teacher.Update(teacher.FirstName, teacher.LastName, "florinolariu@centric.eu", teacher.Password);

            //Assert
            Assert.AreEqual("florinolariu@centric.eu", teacher.Email);
        }
    }
}
