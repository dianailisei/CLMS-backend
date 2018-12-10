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
    }
}