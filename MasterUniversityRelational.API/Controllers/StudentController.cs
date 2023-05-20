namespace MasterUniversityRelational.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using MasterUniversityRelational.API.Interfaces;
    using MasterUniversityRelational.API.Models;
    using MasterUniversityRelational.API.Services;
    using Microsoft.AspNetCore.Mvc;
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IFacultyService _facultyService;
        private readonly IStudentService _studentService;
        public StudentController(IFacultyService facultyService, IStudentService studentService)
        {
            this._facultyService = facultyService;
            this._studentService = studentService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDetailData>>> Get()
        {
            var result = await _studentService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDetailData>> GetByID(Guid id)
        {
            var result = await _studentService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<StudentDetailData>> Save([FromBody] StudentDetailData studentData)
        {
            var checkFaculty = await _facultyService.GetByIdAsync(studentData.FacultyID);
            if (checkFaculty == null)
            {
                return BadRequest();
            }
            var result = await _studentService.Save(studentData);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<StudentDetailData>> Update(Guid id, [FromBody] StudentDetailData studentData)
        {
            var checkFaculty = await _facultyService.GetByIdAsync(studentData.FacultyID);
            if (id != studentData.ID || checkFaculty == null)
            {
                return BadRequest();
            }
            else
            {
                await _studentService.Update(studentData);
            }
            return Ok(studentData);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var data = await _studentService.GetByIdAsync(id);

            if (data == null)
            {
                return NotFound();
            }
            else
            {
                await _studentService.Delete(id);
            }
            return NoContent();
        }

        [HttpPost("testInsert/{testCases}")]
        public async Task<ActionResult<TestResult>> TestSave([FromBody] StudentDetailData studentData, int testCases)
        {
            var getFaculties = await _facultyService.GetAllAsync();
            List<FacultyData> Faculties = getFaculties.ToList();
            var result = await _studentService.TestCase(studentData, testCases, Faculties);
            return Ok(result);
        }

    }
}
