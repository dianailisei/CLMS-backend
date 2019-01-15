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

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> FindPresenceById(Guid id)
        {
            var presence = await _presenceService.FindById(id);
            return Ok(presence);
        }
        
        [HttpGet("student/{studId:guid}/laboratory/{labId:guid}")]
        public async Task<IActionResult> GetPresencesByStudentAndLaboratory(Guid studId, Guid labId)
        {
            var presences = await _presenceService.GetPresencesByStudentAndLaboratory(studId, labId);
            return Ok(presences);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePresence([FromBody] PresenceCreateModel presenceCreateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var presence = await _presenceService.Create(presenceCreateModel);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeletePresence(Guid id)
        {
            await _presenceService.Delete(id);
            return Ok();
        }

    }
}