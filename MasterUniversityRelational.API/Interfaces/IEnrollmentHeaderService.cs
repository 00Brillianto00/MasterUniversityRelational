using MasterUniversityRelational.API.Models;

namespace MasterUniversityRelational.API.Interfaces
{
    public interface IEnrollmentHeaderService
    {
        Task<IEnumerable<EnrollmentData>> GetAllAsync();
        Task<EnrollmentData> GetByIdAsync(Guid id);
        Task<EnrollmentData> Save(EnrollmentData enrollmentData);
        Task<EnrollmentData> Update(EnrollmentData enrollmentData);
        Task<bool> Delete(Guid id);
    }
}
