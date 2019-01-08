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
    }
}
