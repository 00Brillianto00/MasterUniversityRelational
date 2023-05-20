using MasterUniversityRelational.API.Models;

namespace MasterUniversityRelational.API.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentDetailData>> GetAllAsync();
        Task<StudentDetailData> GetByIdAsync(Guid id);
        Task<StudentDetailData> Save(StudentDetailData studentData);
        Task<StudentDetailData> Update(StudentDetailData studentData);
        Task<bool> Delete(Guid id);
        Task<TestResult> TestCase(StudentDetailData studentData, int testCases, List<FacultyData> faculties);
    }
}
