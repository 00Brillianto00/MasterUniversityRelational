using MasterUniversityRelational.API.Models;

namespace MasterUniversityRelational.API.Interfaces
{
    public interface IFacultyService
    {
        Task<IEnumerable<FacultyData>> GetAllAsync();
        Task<FacultyData> GetByIdAsync(Guid id);
        Task<FacultyData> Save(FacultyData facultyData);
        Task<FacultyData> Update(FacultyData facultyData);
        Task<bool> Delete(Guid id);
    }
}
