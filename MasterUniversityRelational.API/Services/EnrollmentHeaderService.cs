using MasterUniversityRelational.API.Interfaces;
using MasterUniversityRelational.API.Models;
using System.Data;

namespace MasterUniversityRelational.API.Services
{
    public class EnrollmentHeaderService : IEnrollmentHeaderService
    {
        private readonly IDataService _dataService;
        public EnrollmentHeaderService(IDataService dataService)
        {
            this._dataService = dataService;
        }
        public async Task<IEnumerable<EnrollmentData>> GetAllAsync()
        {
            try
            {
                var data = await _dataService.GetMany<EnrollmentData>("sp_GetAllEnrollmentHeaders", CommandType.StoredProcedure);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Retrieving Data");
            }
        }

        public async Task<EnrollmentData> GetByIdAsync(Guid id)
        {
            try
            {
                var data = await _dataService.Get<EnrollmentData>("sp_GetEnrollmentHeaderByID", new { id = id.ToString() }, CommandType.StoredProcedure);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Retrieving Data");
            }
        }

        public async Task<EnrollmentData> Save(EnrollmentData enrollmentData)
        {
            enrollmentData.ID = Guid.NewGuid();
            //enrollmentData.StudentID = Guid.NewGuid();
            enrollmentData.IsDeleted = false;
            try
            {
                var data = await _dataService.SaveOne("sp_SaveEnrollmentHeader", enrollmentData, false, CommandType.StoredProcedure);
                return enrollmentData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Saving Data");
            }
        }

        public async Task<EnrollmentData> Update(EnrollmentData enrollmentData)
        {
            try
            {
                var data = await _dataService.RunQuery("sp_UpdateEnrollmentHeader", enrollmentData, false, CommandType.StoredProcedure);
                return enrollmentData;
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
                var data = await _dataService.RunQuery("sp_DeleteEnrollmentHeader", new { id = id.ToString() }, false, CommandType.StoredProcedure);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Deleting Data");
            }
        }
    }
}
