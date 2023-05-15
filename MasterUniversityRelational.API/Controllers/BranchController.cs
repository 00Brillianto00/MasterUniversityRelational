namespace MasterUniversityRelational.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MasterUniversityRelational.API.Interfaces;
    using MasterUniversityRelational.API.Models;
    using MasterUniversityRelational.API.Services;
    using Microsoft.AspNetCore.Mvc;
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IBranchService _branchService;
        public BranchController(IBranchService branchService)
        {
            this._branchService = branchService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BranchData>>> Get()
        {
            var result = await _branchService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BranchData>> GetByID(Guid id)
        {
            var result = await _branchService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<BranchData>> Save([FromBody] BranchData branchData)
        {
            var result = await _branchService.Save(branchData);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BranchData>> Update(Guid id, [FromBody] BranchData branchData)
        {
            if (id != branchData.ID)
            {
                return BadRequest();
            }
            else
            {
                await _branchService.Update(branchData);
            }
            return Ok(branchData);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var data = await _branchService.GetByIdAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            else
            {
                 await _branchService.Delete(id);
            }
            return NoContent();
        }
    }
}
