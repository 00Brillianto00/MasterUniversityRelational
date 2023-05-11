using MasterUniversityRelational.API.Interfaces;
using MasterUniversityRelational.API.Models;
using System.Data;

namespace MasterUniversityRelational.API.Services
{
    public class LecturerService : ILecturerService
    {
        private readonly IDataService _dataService;
        public LecturerService(IDataService dataService)
        {
            this._dataService = dataService;
        }
        public async Task<IEnumerable<LecturerDetailData>> GetAllAsync()
        {
            try
            {
                var data = await _dataService.GetMany<LecturerDetailData>("sp_GetAllLecturers", CommandType.StoredProcedure);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Retrieving Data");
            }
        }

        public async Task<LecturerDetailData> GetByIdAsync(Guid id)
        {
            try
            {
                var data = await _dataService.GetOne<LecturerDetailData>("sp_GetLecturerByID", new { id = id.ToString() }, CommandType.StoredProcedure);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Retrieving Data");
            }
        }

        public async Task<LecturerDetailData> Save(LecturerDetailData LecturerData)
        {
            LecturerData.ID = Guid.NewGuid();
            LecturerData.LecturerID = Guid.NewGuid();
            LecturerData.IsDeleted = false;
            try
            {
                var data = await _dataService.GetScalar("sp_SaveLecturer", LecturerData, false, CommandType.StoredProcedure);
                return LecturerData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Saving Data");
            }
        }

        public async Task<LecturerDetailData> Update(LecturerDetailData studentData)
        {
            try
            {
                var data = await _dataService.ExecuteNonQuery("sp_UpdateLecturer", studentData, false, CommandType.StoredProcedure);
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
                var data = await _dataService.ExecuteNonQuery("sp_DeleteLecturer", new { id = id.ToString() }, false, CommandType.StoredProcedure);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Deleting Data");
            }
        }
    }
}
