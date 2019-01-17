using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Trivia.Business.Question;

namespace Trivia.Api.Controllers
{
    [Route("trivia/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionsController(IQuestionService questionService) => _questionService = questionService;

        [HttpGet]
        public async Task<IActionResult> GetQuestions()
        {
            var questions = await _questionService.GetAll();
            return Ok(questions);
        }

        [HttpGet("{id:guid}", Name = "FindQuestionById")]
        public async Task<IActionResult> FindQuestionById(Guid id)
        {
            var question = await _questionService.FindById(id);
            return Ok(question);
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuestionForLecture([FromBody] QuestionCreateModel questionCreateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var questionId = await _questionService.CreateNew(questionCreateModel);
            return CreatedAtRoute("FindQuestionById", new { id = questionId }, questionCreateModel);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateQuestion(Guid id, [FromBody] QuestionCreateModel questionUpdateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var laboratoryId = await _questionService.Update(id, questionUpdateModel);
            return CreatedAtRoute("FindQuestionById", new { id = laboratoryId }, questionUpdateModel);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteQuestion(Guid id)
        {
            await _questionService.Delete(id);
            return Ok();
        }

        //[HttpPost]
        //[Route("addanswer")]
        //public async Task<IActionResult> AddAnswerToQuestion(Guid questionId, [FromBody]AnswerCreateModel answer)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    var success = await _questionService.AddAnswer(questionId, answer);
        //    if (success)
        //    {
        //        var question = _questionService.FindById(questionId);
        //        return Ok(question);
        //    }
        //    return StatusCode(503, "Time has expired");
        //}
    }
}
