using MasterUniversityRelational.API.Interfaces;
using MasterUniversityRelational.API.Models;
using MasterUniversityRelational.API.Services.Databases;
using System.Data;
using System.Diagnostics;

namespace MasterUniversityRelational.API.Services
{
    public class PerformanceComparisonService : IPerformanceComparison
    {
        private Random rng = new Random();
        private readonly IDataService _dataService;
        public PerformanceComparisonService(IDataService dataService)
        {
            this._dataService = dataService;
        }
        public async Task<TestResult> testInsert(int testCases, List<FacultyData> faculties, List<LecturerDetailData> lecturers, List<CoursesData> courses)
        {
            Stopwatch stopWatch = new Stopwatch();
            TestResult result = new TestResult();

            stopWatch.Start();
            switch (testCases)
            {
                case 1000:
                    var studentDatas = await InsertStudent(10, faculties);
                    await InsertEnrollment(10,10,lecturers,courses,studentDatas);
                    break;
                case 5000:
                    break;
                case 10000:
                    break;
                case 50000:
                    break;
                case 100000:
                    break;
            }
            stopWatch.Stop();
            result.DataProcessed = testCases;
            result.Hours = stopWatch.Elapsed.Hours;
            result.Minutes = stopWatch.Elapsed.Minutes;
            result.Seconds = stopWatch.Elapsed.Seconds;
            result.MiliSeconds = stopWatch.Elapsed.Milliseconds;
            double seconds = (stopWatch.ElapsedMilliseconds / 1000.00);
            double averages = result.DataProcessed / seconds;
            result.AverageTime = "Averaging about "+averages.ToString("0.##") + " Datas Per Second";
            return result;
        }

        public async Task InsertEnrollment(int testCasesHeader,int testCasesDetail, List<LecturerDetailData> lecturers, List<CoursesData> courses, List<StudentDetailData>students)
        {
            Stopwatch stopWatch = new Stopwatch();
            TestResult result = new TestResult();
            EnrollmentData enrollmentHeader = new EnrollmentData();
            EnrollmentDetailData enrollmentDetail = new EnrollmentDetailData();
            try
            {
                stopWatch.Start();
                for (int x = 0; x < testCasesHeader; x++)
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

                    for (int y = 0; y < testCasesDetail; y++)
                    {
                        enrollmentDetail.ID = Guid.NewGuid();
                        enrollmentDetail.EnrollmentHeaderID = id;
                        enrollmentDetail.CourseID = courses[y].ID;
                        enrollmentDetail.LecturerID = lecturers[rng.Next(0, lecturers.Count)].LecturerID;
                        enrollmentDetail.AssignmentScore = rng.Next(1, 100);
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
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Running Test Cases");
            }
        }
        private string generatePhoneNum()
        {
            string firsTwoDigits = rng.Next(0, 99).ToString("00");
            string nextFourDigits = rng.Next(0, 1000).ToString("0000");
            string lastFourDigits = rng.Next(0, 1000).ToString("0000");
            string phoneNum = "08" + firsTwoDigits + "-" + nextFourDigits + "-" + lastFourDigits;
            return phoneNum;
        }

        private DateTime generateDoB()
        {
            DateTime startDate = new DateTime(1960, 1, 1);
            DateTime endDate = new DateTime(2000, 1, 1);
            int rangeDate = (endDate - startDate).Days;
            DateTime RandomDay = startDate.AddDays(rng.Next(rangeDate));
            return RandomDay;
        }
        public async Task<List<StudentDetailData>> InsertStudent(int testCases, List<FacultyData> faculties)
        {
            List<StudentDetailData> studentDatas = new List<StudentDetailData>();
            long StudentNumber = rng.NextInt64(1000000000, 9999999999);
            string firstName = "StudentFirstName_";
            string middleName = "StudentMiddleName_";
            string lastName = "StudentLastName_";
            string address = "JL Kemanggisan Raya";
            string country = "Indonesia";
            string province = "DKI Jakarta";
            string city = "Jakarta Barat";
            try
            {
                for (int x = 0; x <testCases; x++)
                {
                    StudentDetailData studentData = new StudentDetailData();
                    studentData.ID = Guid.NewGuid();
                    studentData.StudentID = Guid.NewGuid();
                    studentData.FacultyID = faculties[rng.Next(0, faculties.Count() - 1)].ID;
                    studentData.StudentNumber = StudentNumber++;
                    string getModifier = studentData.StudentNumber.ToString().Substring(StudentNumber.ToString().Length - 4, 4);
                    studentData.StudentEmail = firstName + "." + lastName + getModifier + "@Univ.ac.id";
                    studentData.StudentFirstName = firstName + getModifier;
                    studentData.StudentGPA = rng.NextDouble() * (4.0 - 1.0) + 1.0;
                    studentData.TotalCreditsEarned = rng.Next(0, 100);
                    if (middleName!= null)
                    {
                        studentData.StudentMiddleName = middleName + getModifier;
                    }
                    studentData.StudentLastName = lastName + getModifier;
                    studentData.EnrolledYear = rng.Next(2000, 2023).ToString();
                    studentData.StudentDateOfBirth = generateDoB();
                    studentData.StudentPhoneNumber = generatePhoneNum();
                    studentData.StudentStreetNumber = rng.Next(0, 50);
                    studentData.StudentPostalCode = rng.Next(1000, 9999);
                    studentData.StudentCountry = country;
                    studentData.StudentProvince = province;
                    studentData.StudentStreetName = address;
                    studentData.StudentCity= city;
                    studentData.IsDeleted = false;
                    studentDatas.Add(studentData);  
                    await _dataService.GetScalar("sp_SaveStudent", studentData, false, CommandType.StoredProcedure);
                }
                return studentDatas;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Saving Data");
            }
        }
    }
}
