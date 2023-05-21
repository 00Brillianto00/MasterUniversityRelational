using MasterUniversityRelational.API.Interfaces;
using MasterUniversityRelational.API.Models;
using System.Data;
using System.Diagnostics;
using System.Security.Cryptography;

namespace MasterUniversityRelational.API.Services
{
    public class EnrollmentDetailService : IEnrollmentDetailService
    {
        private Random rng = new Random();
        private readonly IDataService _dataService;
        public EnrollmentDetailService(IDataService dataService)
        {
            this._dataService = dataService;
        }
        public async Task<IEnumerable<EnrollmentDetailData>> GetAllAsync()
        {
            try
            {
                var data = await _dataService.GetMany<EnrollmentDetailData>("sp_GetAllEnrollmentDetails", CommandType.StoredProcedure);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Retrieving Data");
            }
        }

        public async Task<EnrollmentDetailData> GetByIdAsync(Guid id)
        {
            try
            {
                var data = await _dataService.GetOne<EnrollmentDetailData>("sp_GetEnrollmentDetailByID", new { id = id.ToString() }, CommandType.StoredProcedure);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Retrieving Data");
            }
        }

        public async Task<EnrollmentDetailData> Save(EnrollmentDetailData enrollmentData)
        {
            enrollmentData.ID = Guid.NewGuid();
            //enrollmentData.StudentID = Guid.NewGuid();
            enrollmentData.IsDeleted = false;
            try
            {
                var data = await _dataService.GetScalar("sp_SaveEnrollmentDetail", enrollmentData, false, CommandType.StoredProcedure);
                return enrollmentData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Saving Data");
            }
        }

        public async Task<EnrollmentDetailData> Update(EnrollmentDetailData enrollmentData)
        {
            try
            {
                var data = await _dataService.ExecuteNonQuery("sp_UpdateEnrollmentDetail", enrollmentData, false, CommandType.StoredProcedure);
                return enrollmentData;
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
                var data = await _dataService.ExecuteNonQuery("sp_DeleteEnrollmentDetail", new { id = id.ToString() }, false, CommandType.StoredProcedure);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Deleting Data");
            }
        }

        public async Task<TestResult> TestCase(int testCases, List<CoursesData> courses, List<LecturerDetailData> lecturers, List<StudentDetailData> students)
        {
            Stopwatch stopWatch = new Stopwatch();
            TestResult result = new TestResult();
            EnrollmentData enrollmentHeader= new EnrollmentData();
            EnrollmentDetailData enrollmentDetail = new EnrollmentDetailData();
            try
            {
                stopWatch.Start();
                for (int x = 0; x < testCases; x++)
                {
                    Guid id = Guid.NewGuid();
                    enrollmentHeader.ID = id;
                    enrollmentHeader.StudentID = students[x].ID;
                    enrollmentHeader.IsDeleted = false;
                    double countAverage = 0.0;
                    enrollmentHeader.GPAPerSemester = 0;
                    enrollmentHeader.TotalCoursePerSemester = 10;
                    enrollmentHeader.TotalCostPerSemester = 0;
                    enrollmentHeader.TotalCreditsPerYear = 0;
                    enrollmentHeader.Year = rng.Next(2000, 2023).ToString();
                    int STYPE = rng.Next(1, 2);
                    if (STYPE == 1)
                    {
                        enrollmentHeader.SemesterType = "ODD";
                    }
                    else
                    {
                        enrollmentHeader.SemesterType = "EVEN";
                    }

                    try
                    {
                        await _dataService.GetScalar("sp_SaveEnrollmentHeader", enrollmentHeader, false, CommandType.StoredProcedure);

                    }
                    catch (Exception e)
                    {
                        throw new Exception("Error When Saving Header");
                    }

                    for (int y=0; y < courses.Count();  y++)
                    {
                        enrollmentDetail.ID = Guid.NewGuid();
                        enrollmentDetail.EnrollmentHeaderID = id;
                        enrollmentDetail.CourseID = courses[y].ID;
                        enrollmentDetail.LecturerID = lecturers[rng.Next(0, lecturers.Count)].LecturerID;
                        enrollmentDetail.AssignmentScore = rng.Next(1,100);
                        enrollmentDetail.MidExamScore = rng.Next(1, 100);
                        enrollmentDetail.FinalExamScore = rng.Next(1, 100);
                        enrollmentDetail.CourseAverageScore = (enrollmentDetail.AssignmentScore + enrollmentDetail.MidExamScore + enrollmentDetail.FinalExamScore) / 3.0;
                        try
                        {
                            await _dataService.GetScalar("sp_SaveEnrollmentDetail", enrollmentDetail, false, CommandType.StoredProcedure);
                        }
                        catch (Exception e)
                        {
                            throw new Exception("Error When Saving Header");
                        }
                    }
                }
                result.DataProcessed = testCases*courses.Count();
                stopWatch.Stop();
                result.Hours = stopWatch.Elapsed.Hours;
                result.Minutes = stopWatch.Elapsed.Minutes;
                result.Seconds = stopWatch.Elapsed.Seconds;
                result.MiliSeconds = stopWatch.Elapsed.Milliseconds;
                double seconds = (stopWatch.ElapsedMilliseconds / 1000.00);
                double averages = result.DataProcessed / seconds;
                result.AverageTime = averages.ToString("0.##") + " Datas Per Second";
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Running Test Cases");
            }
        }
    }  
}
