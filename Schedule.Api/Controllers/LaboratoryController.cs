using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Schedule.Business.Laboratory;

namespace Schedule.Api.Controllers
{
    [Route("schedule/[controller]")]
    [ApiController]
    public class LaboratoryController : ControllerBase
    {
        private readonly ILaboratoryService _laboratoryService;

        public LaboratoryController(ILaboratoryService laboratoryService) => _laboratoryService = laboratoryService;

        [HttpGet]
        public async Task<IActionResult> GetLaboratories()
        {
            var labs = await _laboratoryService.GetAll();
            return Ok(labs);
        }

        [HttpGet("{id:guid}", Name = "FindLaboratoryById")]
        public async Task<IActionResult> FindLaboratoryById(Guid id)
        {
            var lab = await _laboratoryService.FindById(id);
            return Ok(lab);
        }

        [HttpPost("/subjects/{subjectId:guid}/teachers/{teacherId}/[controller]")]
        public async Task<IActionResult> CreateLaboratory(Guid teacherId, Guid subjectId, [FromBody] LaboratoryCreateModel laboratoryCreateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var labId = await _laboratoryService.CreateNew(teacherId, subjectId, laboratoryCreateModel);
            return CreatedAtRoute("FindLaboratoryById", new {id = labId}, laboratoryCreateModel);
        }

        [HttpPut("/teachers/{teacherId}/[controller]")]
        public async Task<IActionResult> UpdateLaboratory(Guid teacherId, Guid id, [FromBody] LaboratoryCreateModel laboratoryUpdateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var laboratoryId = await _laboratoryService.Update(teacherId, id, laboratoryUpdateModel);
            return CreatedAtRoute("FindLaboratoryById", new { id = laboratoryId }, laboratoryUpdateModel);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteLaboratory(Guid id)
        {
            await _laboratoryService.Delete(id);
            return Ok();
        }
    }
}