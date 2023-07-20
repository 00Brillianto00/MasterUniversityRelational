using MasterUniversityRelational.API.Models;

namespace MasterUniversityRelational.API.Interfaces
{
    public interface IPerformanceComparison
    {
        Task<TestResult> testInsert(int testCases, List<FacultyData> faculties, List<LecturerDetailData> lecturers, List<CoursesData>courses);
        Task<TestResult> testUpdate(int testCases, List<FacultyData> faculties, List<LecturerDetailData> lecturers, List<CoursesData> courses);
        Task<TestResult> testGet(int testCases);
        Task<TestResult> testDelete(int testCases);
        Task<List<TestResult>> getTopTestDataInsert(int testCase);
        Task<List<TestResult>> getTopTestDataUpdate(int testCase);
        Task<List<TestResult>> getTopTestDataGet(int testCase);
        Task<List<TestResult>> getTopTestDataDelete(int testCase);
        Task<List<GraphData>> getTopTestGraphDataInsert();
        Task<List<GraphData>> getTopTestGraphDataUpdate();
        Task<List<GraphData>> getTopTestGraphDataGet();
        Task<List<GraphData>> getTopTestGraphDataDelete();

    }
}
