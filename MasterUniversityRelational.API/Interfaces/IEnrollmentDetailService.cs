using MasterUniversityRelational.API.Models;

namespace MasterUniversityRelational.API.Interfaces
{
    public interface IEnrollmentDetailService
    {
        Task<IEnumerable<EnrollmentDetailData>> GetAllAsync();
        Task<EnrollmentDetailData> GetByIdAsync(Guid id);
        Task<EnrollmentDetailData> Save(EnrollmentDetailData enrollmentDetailData);
        Task<EnrollmentDetailData> Update(EnrollmentDetailData enrollmentDetailData);
        Task<bool> Delete(Guid id);
        //Task<TestResult> TestCase( int testCases);
    }
}
