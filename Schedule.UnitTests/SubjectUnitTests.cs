using Microsoft.VisualStudio.TestTools.UnitTesting;
using Schedule.Domain.Entities;

namespace Schedule.UnitTests
{
    [TestClass]
    public class SubjectUnitTests
    {
        [TestMethod]
        public void Create_WhenGivenValidArguments_ShouldReturnNewSubjectWithGivenArguments()
        {
            //Arrange
            var teacher = Teacher.Create("Florin", "Olariu", "florin@olariu.ro", "parolagrea");


            //Act
            var subject = Subject.Create(teacher, "Introducere in .NET");

            //Assert
            Assert.AreEqual("Introducere in .NET", subject.Name);
        }

        [TestMethod]
        public void Update_WhenGivenValidArguments_ShouldUpdateExistingSubject()
        {
            //Arrange
            var teacher = Teacher.Create("Florin", "Olariu", "florin@olariu.ro", "parolagrea");
            var subject = Subject.Create(teacher, "Introducere in .NET");

            //Act
            subject.Update("I.NET");

            //Assert
            Assert.AreEqual("I.NET", subject.Name);
        }
    }
}
