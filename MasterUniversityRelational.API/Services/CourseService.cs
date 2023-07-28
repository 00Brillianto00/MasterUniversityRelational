using MasterUniversityRelational.API.Interfaces;
using MasterUniversityRelational.API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
namespace MasterUniversityRelational.API.Services
{
    public partial class CourseService : ICourseService
    {
        private readonly IDataService _dataService;
        public CourseService(IDataService dataService)
        {
            this._dataService = dataService; 
        }

        public async Task<IEnumerable<CoursesData>> GetAllAsync()
        {
            try
            {
                var data = await _dataService.GetMany<CoursesData>("sp_GetAllCourses", CommandType.StoredProcedure);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Retrieving Data");
            }
        }

        public async Task<CoursesData> GetByIdAsync(Guid id)
        {
            try
            {
                var data = await _dataService.Get<CoursesData>("sp_GetCourseByID", new { id = id.ToString() }, CommandType.StoredProcedure);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Retrieving Data");
            }
        }

        public async Task<CoursesData> Save(CoursesData courseData)
        {
            courseData.ID = Guid.NewGuid();
            courseData.IsDeleted = false;
            try
            {
                var data = await _dataService.SaveOne("sp_SaveCourse", courseData, false, CommandType.StoredProcedure);
                return courseData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Saving Data");
            }
        }

        public async Task<CoursesData> Update(CoursesData coursesData)
        {
            try
            {
                var data = await _dataService.RunQuery("sp_UpdateCourse", coursesData , false, CommandType.StoredProcedure);
                return coursesData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Updating Data");
            }
        }
        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var data = await _dataService.RunQuery("sp_DeleteCourse", new { id = id.ToString() }, false, CommandType.StoredProcedure);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Deleting Data");
            }
        }

    }
}
