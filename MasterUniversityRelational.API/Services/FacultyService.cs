using MasterUniversityRelational.API.Interfaces;
using MasterUniversityRelational.API.Models;
using MasterUniversityRelational.API.Services.Databases;
using System.Data;

namespace MasterUniversityRelational.API.Services
{
    public partial class FacultyService : IFacultyService
    {
        private readonly IDataService _dataService;
        public FacultyService(IDataService dataService)
        {
            this._dataService = dataService;
        }
        public async Task<IEnumerable<FacultyData>> GetAllAsync()
        {
            try
            {
                var data = await _dataService.GetMany<FacultyData>("sp_GetAllFaculties", CommandType.StoredProcedure);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Retrieving Data");
            }
        }

        public async Task<FacultyData> GetByIdAsync(Guid id)
        {
            try
            {
                var data = await _dataService.GetOne<FacultyData>("sp_GetFacultyByID", new { id = id.ToString() }, CommandType.StoredProcedure);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Retrieving Data");
            }
        }

        public async Task<FacultyData> Save(FacultyData facultyData)
        {
            facultyData.ID = Guid.NewGuid();
            facultyData.IsDeleted = false;
            try
            {
                var data = await _dataService.GetScalar("sp_SaveFaculty", facultyData, false, CommandType.StoredProcedure);
                return facultyData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Saving Data");
            }
        }

        public async Task<FacultyData> Update(FacultyData facultyData)
        {
            try
            {
                var data = await _dataService.ExecuteNonQuery("sp_UpdateFaculty", facultyData, false, CommandType.StoredProcedure);
                return facultyData;
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
                var data = await _dataService.ExecuteNonQuery("sp_DeleteFaculty", new { id = id.ToString() }, false, CommandType.StoredProcedure);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Deleting Data");
            }
        }
    }
}
