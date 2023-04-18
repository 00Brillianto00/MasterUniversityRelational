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
    public class FacultyController : ControllerBase
    {
        private readonly IFacultyService _facultyService;
        private readonly IBranchService _branchService;
        public FacultyController(IFacultyService facultyService, IBranchService branchService)
        {
            this._facultyService = facultyService;
            this._branchService = branchService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FacultyData>>> Get()
        {
            var result = await _facultyService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FacultyData>> GetByID(Guid id)
        {
            var result = await _facultyService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<FacultyData>> Save([FromBody] FacultyData facultyData)
        {
            var checkBranch = await _branchService.GetByIdAsync(facultyData.BranchID);
            if (checkBranch == null)
            {
                return BadRequest();
            }
            var result = await _facultyService.Save(facultyData);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<FacultyData>> Update(Guid id, [FromBody] FacultyData facultyData)
        {
            var checkBranch = await _branchService.GetByIdAsync(facultyData.BranchID);
            if (id != facultyData.ID || checkBranch == null)
            {
                return BadRequest();
            }
            else
            {
              await _facultyService.Update(facultyData);
            }
            return Ok(facultyData);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var data = await _facultyService.GetByIdAsync(id);

            if (data == null)
            {
                return NotFound();
            }
            else
            {
               await _facultyService.Delete(id);
            }
            return NoContent();
        }
    }
}
