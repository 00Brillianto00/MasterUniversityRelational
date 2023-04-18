using MasterUniversityRelational.API.Interfaces;
using MasterUniversityRelational.API.Models;
using System.Data;

namespace MasterUniversityRelational.API.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDataService _dataService;
        public DepartmentService(IDataService dataService)
        {
            this._dataService = dataService;
        }
        public async Task<IEnumerable<DepartmentData>> GetAllAsync()
        {
            try
            {
                var data = await _dataService.GetMany<DepartmentData>("sp_GetAllDepartments", CommandType.StoredProcedure);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Retrieving Data");
            }
        }

        public async Task<DepartmentData> GetByIdAsync(Guid id)
        {
            try
            {
                var data = await _dataService.GetOne<DepartmentData>("sp_GetDepartmentByID", new { id = id.ToString() }, CommandType.StoredProcedure);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Retrieving Data");
            }
        }

        public async Task<DepartmentData> Save(DepartmentData departmentData)
        {
            departmentData.ID = Guid.NewGuid();
            departmentData.IsDeleted = false;
            try
            {
                var data = await _dataService.GetScalar("sp_SaveDepartment", departmentData, false, CommandType.StoredProcedure);
                return departmentData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Saving Data");
            }
        }

        public async Task<DepartmentData> Update(DepartmentData departmentData)
        {
            try
            {
                var data = await _dataService.ExecuteNonQuery("sp_UpdateDepartment", departmentData, false, CommandType.StoredProcedure);
                return departmentData;
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
                var data = await _dataService.ExecuteNonQuery("sp_DeleteDepartment", new { id = id.ToString() }, false, CommandType.StoredProcedure);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Deleting Data");
            }
        }
    }
}
