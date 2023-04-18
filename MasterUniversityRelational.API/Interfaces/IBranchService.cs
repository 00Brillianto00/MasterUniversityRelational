using MasterUniversityRelational.API.Models;

namespace MasterUniversityRelational.API.Interfaces
{
    public interface IBranchService
    {
        Task<IEnumerable<BranchData>> GetAllAsync();
        Task<BranchData> GetByIdAsync(Guid id);
        Task<BranchData> Save(BranchData branchData);
        Task<BranchData> Update(BranchData branchData);
        Task<bool> Delete(Guid id);
    }
}
