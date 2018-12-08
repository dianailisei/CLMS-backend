using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Schedule.Business.Lecture;

namespace Schedule.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LecturesController : ControllerBase
    {
        private readonly ILectureService lectureService;

        public LecturesController(ILectureService lectureService) =>
            this.lectureService = lectureService;

        [HttpGet]
        public async Task<IActionResult> GetLectures()
        {
            var lectures = await lectureService.GetAll();
            return Ok(lectures);
        }

        [HttpGet("{id:guid}", Name = "FindLectureById")]
        public async Task<IActionResult> FindLectureById(Guid id)
        {
            var lecture = await lectureService.FindById(id);
            return Ok(lecture);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLecture([FromBody] LectureCreateModel lectureCreateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var lectureId = await lectureService.CreateNew(lectureCreateModel);
            return CreatedAtRoute("FindLectureById", new { id = lectureId }, lectureCreateModel);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateLecture(Guid id, [FromBody] LectureCreateModel lectureUpdateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var lectureId = await lectureService.Update(id, lectureUpdateModel);
            return CreatedAtRoute("FindLectureById", new { id = lectureId }, lectureUpdateModel);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteLecture(Guid id)
        {
            await lectureService.Delete(id);
            return Ok();
        }
    }
}