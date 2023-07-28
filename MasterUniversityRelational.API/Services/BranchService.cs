using MasterUniversityRelational.API.Interfaces;
using MasterUniversityRelational.API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MasterUniversityRelational.API.Services
{
    public partial class BranchService : IBranchService
    {
        private readonly IDataService _dataService;
        public BranchService(IDataService dataService)
        {
            this._dataService = dataService;
        }

        public async Task<IEnumerable<BranchData>> GetAllAsync()
        {
            try
            {
                var data = await _dataService.GetMany<BranchData>("sp_GetAllBranches", CommandType.StoredProcedure);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Retrieving Data");
            }
        }

        public async Task<BranchData> GetByIdAsync(Guid id)
        {
            try
            {
                var data = await _dataService.Get<BranchData>("sp_GetBranchByID", new { id = id.ToString() }, CommandType.StoredProcedure);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Retrieving Data");
            }
        }

        public async Task<BranchData> Save(BranchData branchData)
        {
            branchData.ID = Guid.NewGuid();
            branchData.IsDeleted = false;
            try
            {
                var data = await _dataService.SaveOne("sp_SaveBranch", branchData, false, CommandType.StoredProcedure);
                return branchData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Saving Data");
            }
        }

        public async Task<BranchData> Update(BranchData branchData)
        {
            try
            {
                var data = await _dataService.RunQuery("sp_UpdateBranch", branchData, false, CommandType.StoredProcedure);
                return branchData;
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
                var data = await _dataService.RunQuery("sp_DeleteBranch", new { id = id.ToString() }, false, CommandType.StoredProcedure);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Deleting Data");
            }
        }
    }
}
