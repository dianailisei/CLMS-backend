using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Schedule.Business.Teacher;

namespace Schedule.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService) =>
            this._teacherService = teacherService;

        [HttpGet]
        public async Task<IActionResult> GetTeachers()
        {
            var teachers = await _teacherService.GetAll();
            return Ok(teachers);
        }

        [HttpGet("{id:guid}", Name = "FindTeacherById")]
        public async Task<IActionResult> FindTeacherById(Guid id)
        {
            var teacher = await _teacherService.FindById(id);
            return Ok(teacher);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeacher([FromBody] TeacherCreateModel teacherCreateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var teacherId = await _teacherService.CreateNew(teacherCreateModel);
            return CreatedAtRoute("FindTeacherById", new { id = teacherId }, teacherCreateModel);
        }
    }
}