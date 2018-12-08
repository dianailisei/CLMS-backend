using Microsoft.AspNetCore.Mvc;
using Schedule.Business.Subject;
using System;
using System.Threading.Tasks;

namespace Schedule.Api.Controllers
{
    [Route("api/subjects")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectService subjectService;

        public SubjectsController(ISubjectService subjectService) => this.subjectService = subjectService;

        [HttpGet]
        public async Task<IActionResult> GetSubjects()
        {
            var subjects = await this.subjectService.GetAllSubjects();
            return Ok(subjects);
        }

        [HttpGet("{id:guid}", Name = "FindSubjectById")]
        public async Task<IActionResult> FindSubjectById(Guid id)
        {
            var subject = await this.subjectService.FindById(id);
            return Ok(subject);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubject([FromBody] SubjectCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var subjectId = await this.subjectService.CreateNew(model);
            return CreatedAtRoute("FindSubjectById", new { id = subjectId }, model);
        }


    }
}