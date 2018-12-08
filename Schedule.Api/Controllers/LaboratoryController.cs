using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Schedule.Business.Laboratory;

namespace Schedule.Api.Controllers
{
    [Route("api/[controller]")]
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

        [HttpPost]
        public async Task<IActionResult> CreateLaboratory([FromBody] LaboratoryCreateModel laboratoryCreateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var labId = await _laboratoryService.CreateNew(laboratoryCreateModel);
            return CreatedAtRoute("FindLaboratoryById", new {id = labId}, laboratoryCreateModel);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateLecture(Guid id, [FromBody] LaboratoryCreateModel laboratoryUpdateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var labId = await _laboratoryService.Update(id, laboratoryUpdateModel);
            return CreatedAtRoute("FindLaboratoryById", new { id = labId }, laboratoryUpdateModel);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteLecture(Guid id)
        {
            await _laboratoryService.Delete(id);
            return Ok();
        }
    }
}