using MasterUniversityRelational.API.Interfaces;
using MasterUniversityRelational.API.Models;
using System.Data;

namespace MasterUniversityRelational.API.Services
{
    public partial class StudentService : IStudentService
    {
        private readonly IDataService _dataService;
        public StudentService (IDataService dataService)
        {
            this._dataService = dataService;
        }
        public async Task<IEnumerable<StudentDetailData>> GetAllAsync()
        {
            try
            {
                var data = await _dataService.GetMany<StudentDetailData>("sp_GetAllStudents", CommandType.StoredProcedure);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Retrieving Data");
            }
        }

        public async Task<StudentDetailData> GetByIdAsync(Guid id)
        {
            try
            {
                var data = await _dataService.GetOne<StudentDetailData>("sp_GetStudentByID", new { id = id.ToString() }, CommandType.StoredProcedure);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Retrieving Data");
            }
        }

        public async Task<StudentDetailData> Save(StudentDetailData studentData)
        {
            studentData.ID = Guid.NewGuid();
            studentData.StudentID = Guid.NewGuid();
            studentData.IsDeleted = false;
            try
            {
                var data = await _dataService.GetScalar("sp_SaveStudent", studentData, false, CommandType.StoredProcedure);
                return studentData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Saving Data");
            }
        }

        public async Task<StudentDetailData> Update(StudentDetailData studentData)
        {
            try
            {
                var data = await _dataService.ExecuteNonQuery("sp_UpdateStudent", studentData, false, CommandType.StoredProcedure);
                return studentData;
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
                var data = await _dataService.ExecuteNonQuery("sp_DeleteStudent", new { id = id.ToString() }, false, CommandType.StoredProcedure);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Deleting Data");
            }
        }
    }
}
