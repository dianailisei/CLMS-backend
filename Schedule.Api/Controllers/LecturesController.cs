using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Schedule.Business.Lecture;

namespace Schedule.Api.Controllers
{
    [Route("schedule/[controller]")]
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

        [HttpPost("/subjects/{subjectId:guid}/teachers/{lecturerId}/[controller]")]
        public async Task<IActionResult> CreateLecture(Guid lecturerId, Guid subjectId, [FromBody] LectureCreateModel lectureCreateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var lectureId = await lectureService.CreateNew(lecturerId, subjectId, lectureCreateModel);
            return CreatedAtRoute("FindLectureById", new { id = lectureId }, lectureCreateModel);
        }

        [HttpPut("/teachers/{lecturerId}/[controller]")]
        public async Task<IActionResult> UpdateLecture(Guid lecturerId, Guid id, [FromBody] LectureCreateModel lectureUpdateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var lectureId = await lectureService.Update(lecturerId, id, lectureUpdateModel);
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