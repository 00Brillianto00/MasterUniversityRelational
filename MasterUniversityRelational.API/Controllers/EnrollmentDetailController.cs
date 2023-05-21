using MasterUniversityRelational.API.Interfaces;
using MasterUniversityRelational.API.Models;
using MasterUniversityRelational.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace MasterUniversityRelational.API.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class EnrollmentDetailController : ControllerBase
    {
        private readonly IEnrollmentHeaderService _enrollmentHeaderService;
        private readonly IEnrollmentDetailService _enrollmentDetailService;
        private readonly ILecturerService _lecturerService;
        private readonly ICourseService _courseService;
        private readonly IStudentService _studentService;

        public EnrollmentDetailController(IEnrollmentDetailService enrollmentDetailService, ILecturerService lecturerService, ICourseService courseService, IEnrollmentHeaderService enrollmentHeaderService, IStudentService studentService)
        {
            this._enrollmentHeaderService = enrollmentHeaderService;
            this._enrollmentDetailService = enrollmentDetailService;
            this._lecturerService = lecturerService;
            this._courseService = courseService;
            this._studentService = studentService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnrollmentDetailData>>> Get()
        {
            var result = await _enrollmentDetailService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EnrollmentDetailData>> GetByID(Guid id)
        {
            var result = await _enrollmentDetailService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<EnrollmentDetailData>> Save([FromBody] EnrollmentDetailData enrollmentDetailData)
        {
            var checkHeader = await _enrollmentHeaderService.GetByIdAsync(enrollmentDetailData.EnrollmentHeaderID);
            var checkCourse = await _courseService.GetByIdAsync(enrollmentDetailData.CourseID);
            var checkLecturer = await _lecturerService.GetByIdAsync(enrollmentDetailData.LecturerID);
            if ( checkCourse == null || checkLecturer == null || checkHeader == null)
            {
                return BadRequest();
            }
            var result = await _enrollmentDetailService.Save(enrollmentDetailData);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EnrollmentDetailData>> Update(Guid id, [FromBody] EnrollmentDetailData enrollmentDetailData)
        {
            var checkHeader = await _enrollmentHeaderService.GetByIdAsync(enrollmentDetailData.EnrollmentHeaderID);
            var checkCourse = await _courseService.GetByIdAsync(enrollmentDetailData.CourseID);
            var checkLecturer = await _lecturerService.GetByIdAsync(enrollmentDetailData.LecturerID);
            if (id != enrollmentDetailData.ID || checkCourse == null || checkLecturer == null ||  checkHeader == null)
            {
                return BadRequest();
            }
            else
            {
                await _enrollmentDetailService.Update(enrollmentDetailData);
            }
            return Ok(enrollmentDetailData);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var data = await _enrollmentDetailService.GetByIdAsync(id);

            if (data == null)
            {
                return NotFound();
            }
            else
            {
                await _enrollmentDetailService.Delete(id);
            }
            return NoContent();
        }

        [HttpPost("testInsert/{testCases}")]
        public async Task<ActionResult<TestResult>> TestSave(int testCases)
        {
            
            var getCourses = await _courseService.GetAllAsync();
            List<CoursesData> courses = getCourses.ToList();

            var getLecturers = await _lecturerService.GetAllAsync();
            List<LecturerDetailData> lecturers = getLecturers.ToList();

            var getStudents = await _studentService.GetAllAsync();
            List<StudentDetailData> students = getStudents.ToList(); 
            var result = await _enrollmentDetailService.TestCase(testCases, courses, lecturers,students);
            return Ok(result);
        }


    }
}
