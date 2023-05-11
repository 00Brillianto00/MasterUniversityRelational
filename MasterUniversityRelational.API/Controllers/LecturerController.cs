using MasterUniversityRelational.API.Interfaces;
using MasterUniversityRelational.API.Models;
using MasterUniversityRelational.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace MasterUniversityRelational.API.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class LecturerController :ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly ILecturerService _lecturerService;
        public LecturerController(IDepartmentService departmentservice, ILecturerService lecturerService)
        {
            this._departmentService = departmentservice;
            this._lecturerService = lecturerService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LecturerDetailData>>> Get()
        {
            var result = await _lecturerService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LecturerDetailData>> GetByID(Guid id)
        {
            var result = await _lecturerService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<LecturerDetailData>> Save([FromBody] LecturerDetailData lecturerData)
        {
            var checkDepartment = await _departmentService.GetByIdAsync(lecturerData.DepartmentID);
            if (checkDepartment == null)
            {
                return BadRequest();
            }
            var result = await _lecturerService.Save(lecturerData);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<LecturerDetailData>> Update(Guid id, [FromBody] LecturerDetailData lecturerData)
        {
            var checkDepartment = await _departmentService.GetByIdAsync(lecturerData.DepartmentID);
            if (id != lecturerData.ID || checkDepartment == null)
            {
                return BadRequest();
            }
            else
            {
                await _lecturerService.Update(lecturerData);
            }
            return Ok(lecturerData);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var data = await _lecturerService.GetByIdAsync(id);

            if (data == null)
            {
                return NotFound();
            }
            else
            {
                await _lecturerService.Delete(id);
            }
            return NoContent();
        }
    }
}
