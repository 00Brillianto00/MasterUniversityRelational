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
    public partial class CourseController : ControllerBase
    {
        private readonly ICourseService _coursesService;
        public CourseController(ICourseService courseService)
        {
            this._coursesService = courseService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CoursesData>>> Get()
        {
            var result = await _coursesService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CoursesData>> GetByID(Guid id)
        {
            var result = await _coursesService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<CoursesData>> Save([FromBody] CoursesData courseData)
        {
            var result = await _coursesService.Save(courseData);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CoursesData>> Update(Guid id, [FromBody] CoursesData coursesData)
        {
            if (id != coursesData.ID)
            {
                return BadRequest();
            }
            else
            {
                await _coursesService.Update(coursesData);
            }
            return Ok(coursesData);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var data = await _coursesService.GetByIdAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            else
            {
                await _coursesService.Delete(id);
            }
            return NoContent();
        }
    }
}
