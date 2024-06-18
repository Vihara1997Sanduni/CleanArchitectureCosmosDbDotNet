using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CleanArchitectureCosmosDb.Application.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using CleanArchitectureCosmosDb.Application.DTOs;

namespace CleanArchitectureCosmosDb.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProgramController : ControllerBase
    {
        private readonly ProgramService _programService;

        public ProgramController(ProgramService programService)
        {
            _programService = programService;
        }

        [HttpGet]
        [Authorize(Roles = "Employer, Candidate")]
        public async Task<IActionResult> GetPrograms()
        {
            var programs = await _programService.GetAllAsync();
            return Ok(programs);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Employer, Candidate")]
        public async Task<IActionResult> GetProgramById(string id)
        {
            var program = await _programService.GetByIdAsync(id);
            if (program == null) return NotFound();
            return Ok(program);
        }

        [HttpPost]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> AddProgram([FromBody] ProgramDto programDto)
        {
            await _programService.AddAsync(programDto);
            return CreatedAtAction(nameof(GetProgramById), new { id = programDto.Id }, programDto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> UpdateProgram(string id, [FromBody] ProgramDto programDto)
        {
            if (id != programDto.Id) return BadRequest();

            await _programService.UpdateAsync(programDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> DeleteProgram(string id)
        {
            await _programService.DeleteAsync(id);
            return NoContent();
        }
    }
}
