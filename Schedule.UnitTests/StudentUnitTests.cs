using Microsoft.VisualStudio.TestTools.UnitTesting;
using Schedule.Business.Student;
using Schedule.Domain.Entities;
using Schedule.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule.UnitTests
{
    class MockContext : IReadRepository, IWriteRepository
    {
        public bool deleted = false;
        ObservableCollection<Entity> data;
        public Guid defGuid;
        public MockContext()
        {
            data = new ObservableCollection<Entity>();
        }
        Task IWriteRepository.AddNewAsync<TEntity>(TEntity entity)
        {
            var stud = Student.Create(((Student)(object)entity).FirstName,
                                      ((Student)(object)entity).LastName,
                                      ((Student)(object)entity).Email,
                                      ((Student)(object)entity).Password,
                                      ((Student)(object)entity).Group,
                                      ((Student)(object)entity).Year);
            defGuid = stud.Id;
            data.Add((TEntity)(object)stud);
            return Task.FromResult(stud.Id);
        }

        Task IWriteRepository.DeleteByIdAsync<TEntity>(Guid id)
        {
            foreach (var stud in data)
            {
                if (((Student)(object)stud).Id == id)
                {
                    data.Remove(stud);
                    deleted = true;
                    return Task.CompletedTask;
                }
            }
            return Task.CompletedTask;
        }

        Task<TEntity> IReadRepository.FindByIdAsync<TEntity>(Guid id)
        {
            foreach (var stud in data)
            {
                if (((Student)(object)stud).Id == id)
                    return Task.FromResult((TEntity)(object)stud);
            }
            return null;
        }

        IQueryable<TEntity> IReadRepository.GetAll<TEntity>()
        {
            IQueryable<TEntity> entities = new List<TEntity>().AsQueryable();
            foreach(var stud in data)
            {
                entities.Append((TEntity)(object)stud);
            }
            return entities;
        }

        Task IWriteRepository.SaveAsync()
        {
            return Task.CompletedTask;
        }

        Task IWriteRepository.UpdateAsync<TEntity>(Guid id, TEntity entity)
        {
            var count = 0;
            foreach (var stud in data)
            {
                count++;
                if (((Student)(object)stud).Id == id)
                {
                    ((Student)(object)entity).FirstName = ((StudentCreateModel)(object)entity).FirstName;
                    ((Student)(object)entity).LastName = ((StudentCreateModel)(object)entity).LastName;
                    ((Student)(object)entity).Email = ((StudentCreateModel)(object)entity).Email;
                    ((Student)(object)entity).Password = ((StudentCreateModel)(object)entity).Password;
                    ((Student)(object)entity).Group = ((StudentCreateModel)(object)entity).Group;
                    ((Student)(object)entity).Year = ((StudentCreateModel)(object)entity).Year;
                    return Task.FromResult(stud.Id);
                }
            }
            return Task.FromResult(count);
        }
    }


    [TestClass]
    public class StudentUnitTests
    {
        StudentService studentService;
        MockContext context;
        Guid defaultId;

        [TestInitialize]
        public async Task Initialize()
        {
            context = new MockContext();
            studentService = new StudentService(context, context);
            StudentCreateModel createModel = new StudentCreateModel()
            {
                FirstName = "ASD",
                LastName = "ZXC",
                Email = "asdf@afd.com",
                Group = "A1",
                Password = "asdasdaasd",
                Year = 3
            };
            defaultId = await studentService.CreateNew(createModel);
        }
        [TestMethod]
        public async Task Create_WhenValidParameters()
        {
            StudentCreateModel createModel = new StudentCreateModel()
            {
                FirstName = "ASDasd",
                LastName = "ZXCasd",
                Email = "aasdassdf@afd.com",
                Group = "A3",
                Password = "asdasdasdaasd",
                Year = 1
            };

            var result = await studentService.CreateNew(createModel);
            Assert.AreEqual(defaultId.GetType(), result.GetType());
        }

        [TestMethod]
        public async Task Update_WhenGivenIdandNewData()
        {
            var student = new StudentCreateModel()
            {
                FirstName = "ASDasd",
                LastName = "ZXasdC",
                Email = "asdf@afasdd.com",
                Group = "A1",
                Password = "asdaaassdaasd",
                Year = 1
            };
            var result = await studentService.Update(new Guid(), student);
            Assert.AreEqual(defaultId, result);
        }

        //[TestMethod]
        //public void FindById_WhenGivenId()
        //{
        //    var result = studentService.FindById(context.foundId);
        //    Assert.AreEqual(context.foundId, result.Id);
        //}

        [TestMethod]
        public async Task DeleteById_WhenGivenValidId()
        {
            await studentService.Delete(defaultId);
            Assert.IsTrue(context.deleted);
        }
    }
}
