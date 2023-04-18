using MasterUniversityRelational.API.Models;

namespace MasterUniversityRelational.API.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentData>> GetAllAsync();
        Task<StudentData> GetByIdAsync(Guid id);
        Task<StudentData> Save(StudentData studentData);
        Task<StudentData> Update(StudentData studentData);
        Task<bool> Delete(Guid id);
    }
}
