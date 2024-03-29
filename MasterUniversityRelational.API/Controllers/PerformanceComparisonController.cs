﻿using MasterUniversityRelational.API.Interfaces;
using MasterUniversityRelational.API.Models;
using MasterUniversityRelational.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.Xml;

namespace MasterUniversityRelational.API.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class PerformanceComparisonController : ControllerBase
    {
        private readonly IEnrollmentHeaderService _enrollmentHeaderService;
        private readonly IEnrollmentDetailService _enrollmentDetailService;
        private readonly ILecturerService _lecturerService;
        private readonly ICourseService _courseService;
        private readonly IStudentService _studentService;
        private readonly IFacultyService _facultyService;
        private readonly IPerformanceComparison _performanceComparison;

        public PerformanceComparisonController(IEnrollmentDetailService enrollmentDetailService, ILecturerService lecturerService, ICourseService courseService, IEnrollmentHeaderService enrollmentHeaderService, IStudentService studentService, IFacultyService facultyService, IPerformanceComparison performanceComparison)
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

            //var getStudents = await _studentService.GetAllAsync();
           // List<StudentDetailData> students = getStudents.ToList();

            var getFaculties = await _facultyService.GetAllAsync();
            List<FacultyData> faculties = getFaculties.ToList();

            TestResult result = new TestResult();
            try
            {
                 result = await _performanceComparison.testInsert(testCases, faculties, lecturers, courses);
                 return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpPut("testUpdate/{testCases}")]
        public async Task<ActionResult<TestResult>> TestUpdate(int testCases)
        {
            var getCourses = await _courseService.GetAllAsync();
            List<CoursesData> courses = getCourses.ToList();
            var getLecturers = await _lecturerService.GetAllAsync();
            List<LecturerDetailData> lecturers = getLecturers.ToList();
            var getFaculties = await _facultyService.GetAllAsync();
            List<FacultyData> faculties = getFaculties.ToList();
            TestResult tes = new TestResult();
            try
            {
                tes = await _performanceComparison.testUpdate(testCases, faculties, lecturers, courses); 
                return Ok(tes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpGet("testGet/{testCases}")]
        public async Task<ActionResult<TestResult>> TestGet(int testCases)
        {
            try
            {
                var result = await _performanceComparison.testGet(testCases);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpDelete("testDelete/{testCases}")]
        public async Task<ActionResult<TestResult>> TestDelete(int testCases)
        {
            try{

                var result = await _performanceComparison.testDelete(testCases);
                return Ok(result);
            }
            catch(Exception ex)
            {
                //Response.StatusCode = 400;
                return BadRequest(ex.Message.ToString());
                //return Content(Response.StatusCode, ex.Message.ToString());
            }
        }

        [HttpGet("GetTopInsertData/{testCases}")]
        public async Task<ActionResult<TestResult>> GetTopDataInsert(int testCases)
        {
            var result = await _performanceComparison.getTopTestDataInsert(testCases);
            return Ok(result);
        }

        [HttpGet("GetTopUpdateData/{testCases}")]
        public async Task<ActionResult<TestResult>> GetTopDataUpdate(int testCases)
        {
            var result = await _performanceComparison.getTopTestDataUpdate(testCases);
            return Ok(result);
        }

        [HttpGet("GetTopGetData/{testCases}")]
        public async Task<ActionResult<TestResult>> GetTopDataGet(int testCases)
        {
            var result = await _performanceComparison.getTopTestDataGet(testCases);
            return Ok(result);
        }

        [HttpGet("GetTopDeleteData/{testCases}")]
        public async Task<ActionResult<TestResult>> GetTopDataDelete(int testCases)
        {
            var result = await _performanceComparison.getTopTestDataDelete(testCases);
            return Ok(result);
        }

        [HttpGet("GetTopInsertDataGraph/")]
        public async Task<ActionResult<GraphData>> GetTopDataInsertGraph()
        {
            var result = await _performanceComparison.getTopTestGraphDataInsert();
            return Ok(result);
        }

        [HttpGet("GetTopUpdateDataGraph/")]
        public async Task<ActionResult<GraphData>> GetTopDataUpdateGraph()
        {
            var result = await _performanceComparison.getTopTestGraphDataUpdate();
            return Ok(result);
        }

        [HttpGet("GetTopGetDataGraph/")]
        public async Task<ActionResult<GraphData>> GetTopDataGetGraph()
        {
            var result = await _performanceComparison.getTopTestGraphDataGet();
            return Ok(result);
        }


        [HttpGet("GetTopDeleteDataGraph/")]
        public async Task<ActionResult<GraphData>> GetTopDataDeleteGraph()
        {
            var result = await _performanceComparison.getTopTestGraphDataDelete();
            return Ok(result);
        }

    }
}
