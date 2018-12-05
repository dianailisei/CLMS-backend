using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Schedule.Business.Student;

namespace Schedule.Api.Controllers
{
    [Route("schedule/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService studentService;

        public StudentController(IStudentService studentService) =>
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

    }
}