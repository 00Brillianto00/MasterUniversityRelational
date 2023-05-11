using MasterUniversityRelational.API.Interfaces;
using MasterUniversityRelational.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace MasterUniversityRelational.API.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class EnrollmentHeaderController : ControllerBase
    {
        private readonly IEnrollmentHeaderService _enrollmentHeaderService;
        private readonly IStudentService _studentService;
        public EnrollmentHeaderController(IEnrollmentHeaderService enrollmentService, IStudentService studentService)
        {
            this._enrollmentHeaderService = enrollmentService;
            this._studentService = studentService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnrollmentData>>> Get()
        {
            var result = await _enrollmentHeaderService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EnrollmentData>> GetByID(Guid id)
        {
            var result = await _enrollmentHeaderService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<EnrollmentData>> Save([FromBody] EnrollmentData enrollmentData)
        {
            var checkStudent = await _studentService.GetByIdAsync(enrollmentData.StudentID);
            if (checkStudent == null)
            {
                return BadRequest();
            }
            var result = await _enrollmentHeaderService.Save(enrollmentData);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EnrollmentData>> Update(Guid id, [FromBody] EnrollmentData enrollmentData)
        {
            var checkStudent = await _studentService.GetByIdAsync(enrollmentData.StudentID);
            if (id != enrollmentData.ID || checkStudent == null)
            {
                return BadRequest();
            }
            else
            {
                await _enrollmentHeaderService.Update(enrollmentData);
            }
            return Ok(enrollmentData);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var data = await _enrollmentHeaderService.GetByIdAsync(id);

            if (data == null)
            {
                return NotFound();
            }
            else
            {
                await _enrollmentHeaderService.Delete(id);
            }
            return NoContent();
        }
    }
}
