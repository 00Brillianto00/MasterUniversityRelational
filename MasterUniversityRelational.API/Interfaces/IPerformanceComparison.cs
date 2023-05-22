using MasterUniversityRelational.API.Models;

namespace MasterUniversityRelational.API.Interfaces
{
    public interface IPerformanceComparison
    {
        Task<TestResult> testInsert(int testCases, List<FacultyData> faculties, List<LecturerDetailData> lecturers, List<CoursesData>courses);
        Task<TestResult> testUpdate(int testCases, List<FacultyData> faculties, List<LecturerDetailData> lecturers, List<CoursesData> courses);
    }
}
