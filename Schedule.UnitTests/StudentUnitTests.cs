using Microsoft.VisualStudio.TestTools.UnitTesting;
using Schedule.Domain.Entities;

namespace Schedule.UnitTests
{
    [TestClass]
    public class StudentUnitTests
    {
        [TestMethod]
        public void Create_WhenGivenValidArguments_ShouldReturnNewStudentWithGivenArguments()
        {
            //Act
            var student = Student.Create("Codrut", "Iftimie", "codrut.iftimie@gmail.com", "complicat", "A1", 3);

            //Assert
            Assert.AreEqual("Codrut", student.FirstName);
        }

        [TestMethod]
        public void Update_WhenGivenValidArguments_ShouldUpdateExistingStudent()
        {
            //Arrange
            var student = Student.Create("Codrut", "Iftimie", "codrut.iftimie@gmail.com", "complicat", "A1", 3);

            //Act
            student.Update(student.FirstName, student.LastName, student.Email, student.Password, "A2", student.Year);

            //Assert
            Assert.AreEqual("A2",student.Group);
        }
    }
}
