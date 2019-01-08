﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Trivia.Business.Answer;

namespace Trivia.Api.Controllers
{
    [Route("trivia/[controller]")]
    [ApiController]
    public class AnswersController : ControllerBase
    {
        private readonly IAnswerService _answerService;

        public AnswersController(IAnswerService answerService) => _answerService = answerService;

        [HttpGet]
        public async Task<IActionResult> GetAnswers()
        {
            var answers = await _answerService.GetAll();
            return Ok(answers);
        }

        [HttpGet("{id:guid}", Name = "FindAnswerById")]
        public async Task<IActionResult> FindAnswerById(Guid id)
        {
            var answer = await _answerService.FindById(id);
            return Ok(answer);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnswer([FromBody] AnswerCreateModel answerCreateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var answerId = await _answerService.CreateNew(answerCreateModel);
            return CreatedAtRoute("FindAnswerById", new { id = answerId }, answerCreateModel);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAnswer(Guid id)
        {
            await _answerService.Delete(id);
            return Ok();
        }
    }
}