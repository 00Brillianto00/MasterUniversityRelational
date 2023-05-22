using MasterUniversityRelational.API.Interfaces;
using MasterUniversityRelational.API.Models;
using MasterUniversityRelational.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace MasterUniversityRelational.API.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class PerformanceComparison : ControllerBase
    {
        private readonly IEnrollmentHeaderService _enrollmentHeaderService;
        private readonly IEnrollmentDetailService _enrollmentDetailService;
        private readonly ILecturerService _lecturerService;
        private readonly ICourseService _courseService;
        private readonly IStudentService _studentService;
        private readonly IFacultyService _facultyService;
        private readonly IPerformanceComparison _performanceComparison;

        public PerformanceComparison(IEnrollmentDetailService enrollmentDetailService, ILecturerService lecturerService, ICourseService courseService, IEnrollmentHeaderService enrollmentHeaderService, IStudentService studentService, IFacultyService facultyService, IPerformanceComparison performanceComparison)
        {
            this._enrollmentHeaderService = enrollmentHeaderService;
            this._enrollmentDetailService = enrollmentDetailService;
            this._lecturerService = lecturerService;
            this._courseService = courseService;
            this._studentService = studentService;
            this._facultyService = facultyService;
            this._performanceComparison = performanceComparison;
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

            var getFaculties = await _facultyService.GetAllAsync();
            List<FacultyData> faculties = getFaculties.ToList();

            var result = await _performanceComparison.testInsert(testCases, faculties, lecturers, courses);
            return Ok(result);
        }


    }
}
