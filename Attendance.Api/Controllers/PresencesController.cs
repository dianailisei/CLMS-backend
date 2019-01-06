using System;
using System.Threading.Tasks;
using Attendance.Business.Presence;
using Attendance.Business.Presence.Models;
using Microsoft.AspNetCore.Mvc;

namespace Attendance.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PresencesController : ControllerBase
    {
        private readonly IPresenceService _presenceService;

        public PresencesController(IPresenceService presenceService) =>
            _presenceService = presenceService;

        [HttpGet]
        public async Task<IActionResult> GetPresences()
        {
            var presence = await _presenceService.GetAll();
            return Ok(presence);
        }

        [HttpGet("{id:guid}", Name = "FindPresenceById")]
        public async Task<IActionResult> FindPresenceById(Guid id)
        {
            var presence = await _presenceService.FindById(id);
            return Ok(presence);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePresence([FromBody] PresenceCreateModel presenceCreateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var presenceId = await _presenceService.Create(presenceCreateModel);
            return CreatedAtRoute("FindPresenceById", new { id = presenceId }, presenceCreateModel);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeletePresence(Guid id)
        {
            await _presenceService.Delete(id);
            return Ok();
        }

    }
}