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
    public class DepartmentController : ControllerBase
    {
        private readonly IFacultyService _facultyService;
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IFacultyService facultyService, IDepartmentService departmentService)
        {
            this._facultyService = facultyService;
            this._departmentService= departmentService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentData>>> Get()
        {
            var result = await _departmentService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentData>> GetByID(Guid id)
        {
            var result = await _departmentService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<DepartmentData>> Save([FromBody] DepartmentData departmentData)
        {
            var checkFaculty = await _facultyService.GetByIdAsync(departmentData.FacultyID);
            if (checkFaculty == null)
            {
                return BadRequest();
            }
            var result = await _departmentService.Save(departmentData);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DepartmentData>> Update(Guid id, [FromBody] DepartmentData departmentData)
        {
            var checkFaculty = await _facultyService.GetByIdAsync(departmentData.FacultyID);
            if (id != departmentData.ID || checkFaculty == null)
            {
                return BadRequest();
            }
            else
            {
                await _departmentService.Update(departmentData);
            }
            return Ok(departmentData);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var data = await _departmentService.GetByIdAsync(id);

            if (data == null)
            {
                return NotFound();
            }
            else
            {
                await _departmentService.Delete(id);
            }
            return NoContent();
        }
    }
}