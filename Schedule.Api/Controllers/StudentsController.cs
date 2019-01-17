using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Schedule.Business.Student;

namespace Schedule.Api.Controllers
{
    [Route("schedule/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService studentService;

        public StudentsController(IStudentService studentService) =>
            this.studentService = studentService;

        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var studs = await studentService.GetAll();
            return Ok(studs);
        }

        [HttpGet("{id:guid}", Name = "FindStudentById")]
        public async Task<IActionResult> FindStudentById(Guid id)
        {
            var student = await studentService.FindById(id);
            return Ok(student);
        }

        [HttpGet("{email}/{pwd}")]
        public async Task<IActionResult> Login(string email, string pwd)
        {
            var student = await studentService.Login(email, pwd);
            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] StudentCreateModel studentCreateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var studentId = await studentService.CreateNew(studentCreateModel);
            return CreatedAtRoute("FindStudentById", new { id = studentId}, studentCreateModel);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateStudent(Guid id,[FromBody] StudentCreateModel studentUpdateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var studentId = await studentService.Update(id, studentUpdateModel);
            return CreatedAtRoute("FindStudentById", new { id = studentId }, studentUpdateModel);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            await studentService.Delete(id);
            return Ok();
        }

    }
}