using MasterUniversityRelational.API.Models;

namespace MasterUniversityRelational.API.Interfaces
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentData>> GetAllAsync();
        Task<DepartmentData> GetByIdAsync(Guid id);
        Task<DepartmentData> Save(DepartmentData departmentData);
        Task<DepartmentData> Update(DepartmentData departmentData);
        Task<bool> Delete(Guid id);
    }
}
