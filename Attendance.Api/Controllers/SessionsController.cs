using System;
using System.Threading.Tasks;
using Attendance.Business.Session;
using Attendance.Business.Session.Models;
using Microsoft.AspNetCore.Mvc;

namespace Attendance.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionsController : ControllerBase
    {
            private readonly ISessionService _sessionService;

            public SessionsController(ISessionService sessionService) =>
                _sessionService = sessionService;

            [HttpGet]
            public async Task<IActionResult> GetSessions()
            {
                var session = await _sessionService.GetAll();
                return Ok(session);
            }

            [HttpGet("{id:guid}", Name = "FindSessionById")]
            public async Task<IActionResult> FindSessionById(Guid id)
            {
                var session = await _sessionService.FindById(id);
                return Ok(session);
            }

            [HttpPost]
            public async Task<IActionResult> CreateSession([FromBody] SessionCreateModel sessionCreateModel)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var sessionId = await _sessionService.CreateNew(sessionCreateModel);

                return CreatedAtRoute("FindSessionById", new { id = sessionId }, sessionId);
            }

            [HttpPut("{id:guid}")]
            public async Task<IActionResult> UpdateSession(Guid id, [FromBody] SessionUpdateModel sessionUpdateModel)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var sessionId = await _sessionService.Update(id, sessionUpdateModel);
                return NoContent();
            }

            [HttpDelete("{id:guid}")]
            public async Task<IActionResult> Delete(Guid id)
            {
                await _sessionService.Delete(id);
                return Ok();
            }

        }
    }