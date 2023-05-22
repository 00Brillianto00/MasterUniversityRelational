using MasterUniversityRelational.API.Interfaces;
using MasterUniversityRelational.API.Models;
using System.Data;
using System.Diagnostics;
using System.Security.Cryptography;

namespace MasterUniversityRelational.API.Services
{
    public class EnrollmentDetailService : IEnrollmentDetailService
    {
        private Random rng = new Random();
        private readonly IDataService _dataService;
        public EnrollmentDetailService(IDataService dataService)
        {
            this._dataService = dataService;
        }
        public async Task<IEnumerable<EnrollmentDetailData>> GetAllAsync()
        {
            try
            {
                var data = await _dataService.GetMany<EnrollmentDetailData>("sp_GetAllEnrollmentDetails", CommandType.StoredProcedure);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Retrieving Data");
            }
        }

        public async Task<EnrollmentDetailData> GetByIdAsync(Guid id)
        {
            try
            {
                var data = await _dataService.GetOne<EnrollmentDetailData>("sp_GetEnrollmentDetailByID", new { id = id.ToString() }, CommandType.StoredProcedure);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Retrieving Data");
            }
        }

        public async Task<EnrollmentDetailData> Save(EnrollmentDetailData enrollmentData)
        {
            enrollmentData.ID = Guid.NewGuid();
            //enrollmentData.StudentID = Guid.NewGuid();
            enrollmentData.IsDeleted = false;
            try
            {
                var data = await _dataService.GetScalar("sp_SaveEnrollmentDetail", enrollmentData, false, CommandType.StoredProcedure);
                return enrollmentData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Saving Data");
            }
        }

        public async Task<EnrollmentDetailData> Update(EnrollmentDetailData enrollmentData)
        {
            try
            {
                var data = await _dataService.ExecuteNonQuery("sp_UpdateEnrollmentDetail", enrollmentData, false, CommandType.StoredProcedure);
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
                var data = await _dataService.ExecuteNonQuery("sp_DeleteEnrollmentDetail", new { id = id.ToString() }, false, CommandType.StoredProcedure);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Deleting Data");
            }
        }
    }  
}
