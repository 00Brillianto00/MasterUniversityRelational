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
            List<StudentDetailData> studentDatas = new List<StudentDetailData>();
            stopWatch.Start();
            switch (testCases)
            {
                case 1000:
                    studentDatas = await InsertStudent(10, faculties);
                    await InsertEnrollment(10,10,lecturers,courses,studentDatas);
                    break;
                case 5000:
                    studentDatas = await InsertStudent(50, faculties);
                    await InsertEnrollment(10, 10, lecturers, courses, studentDatas);
                    break;
                case 10000:
                    studentDatas = await InsertStudent(100, faculties);
                    await InsertEnrollment(10, 10, lecturers, courses, studentDatas);
                    break;
                case 50000:
                    studentDatas = await InsertStudent(500, faculties);
                    await InsertEnrollment(10, 10, lecturers, courses, studentDatas);
                    break;
                case 100000:
                    studentDatas = await InsertStudent(1000, faculties);
                    await InsertEnrollment(10, 10, lecturers, courses, studentDatas);
                    break;
            }
            stopWatch.Stop();
            result.DataProcessed = testCases;
            result.Hours = stopWatch.Elapsed.Hours;
            result.Minutes = stopWatch.Elapsed.Minutes;
            result.Seconds = stopWatch.Elapsed.Seconds;
            result.MiliSeconds = stopWatch.Elapsed.Milliseconds;
            double seconds = (stopWatch.ElapsedMilliseconds / 1000.00);
            double averages;
            string averageDesc;
            if(result.Seconds == 0)
            {
                averages = result.DataProcessed / result.MiliSeconds;
                averageDesc = " Datas per Milisecond";
            }
            else
            {
                averages = result.DataProcessed / seconds;
                averageDesc = " Datas per Second";
            }
            result.AverageTime = "Averaging about " + averages.ToString("0.##") + averageDesc;
            return result;
        }

        public async Task<TestResult> testUpdate(int testCases, List<FacultyData> faculties, List<LecturerDetailData> lecturers, List<CoursesData> courses)
        {
            Stopwatch stopWatch = new Stopwatch();
            TestResult result = new TestResult();
            List<StudentDetailData> studentDatas = new List<StudentDetailData>();
            List<StudentDetailData> newStudentDatas = new List<StudentDetailData>();
            stopWatch.Start();
            switch (testCases)
            {
                case 1000:
                    studentDatas = await GetStudentData(10);
                    newStudentDatas = await updateStudent(studentDatas.Count(), faculties, studentDatas);
                    await UpdateEnrollment(10, 10, courses, newStudentDatas);
                    break;
                case 5000:
                    studentDatas = await GetStudentData(50);
                    newStudentDatas = await updateStudent(studentDatas.Count(), faculties, studentDatas);
                    await UpdateEnrollment(10, 10, courses, newStudentDatas);
                    break;
                case 10000:
                    studentDatas = await GetStudentData(100);
                    newStudentDatas = await updateStudent(studentDatas.Count(), faculties, studentDatas);
                    await UpdateEnrollment(10, 10, courses, newStudentDatas);
                    break;
                case 50000:
                    studentDatas = await GetStudentData(500);
                    newStudentDatas = await updateStudent(studentDatas.Count(), faculties, studentDatas);
                    await UpdateEnrollment(10, 10, courses, newStudentDatas);
                    break;
                case 100000:
                    studentDatas = await GetStudentData(1000);
                    newStudentDatas = await updateStudent(studentDatas.Count(), faculties, studentDatas);
                    await UpdateEnrollment(10, 10, courses, newStudentDatas);
                    break;
            }
            stopWatch.Stop();
            result.DataProcessed = testCases;
            result.Hours = stopWatch.Elapsed.Hours;
            result.Minutes = stopWatch.Elapsed.Minutes;
            result.Seconds = stopWatch.Elapsed.Seconds;
            result.MiliSeconds = stopWatch.Elapsed.Milliseconds;
            double seconds = (stopWatch.ElapsedMilliseconds / 1000.00);
            double averages;
            string averageDesc;
            if (result.Seconds == 0)
            {
                averages = result.DataProcessed / result.MiliSeconds;
                averageDesc = " Datas per Milisecond";
            }
            else
            {
                averages = result.DataProcessed / seconds;
                averageDesc = " Datas per Second";
            }
            result.AverageTime = "Averaging about " + averages.ToString("0.##") + averageDesc;
            return result;
        }

        public async Task<TestResult> testGet(int testCases)
        {
            Stopwatch stopWatch = new Stopwatch();
            TestResult result = new TestResult();
            stopWatch.Start();
            var data = await _dataService.GetMany<EnrollmentDataModel>("sp_GetTopEnrollmentDataModel", new { topData = testCases }, CommandType.StoredProcedure);
            stopWatch.Stop();
            result.DataProcessed = testCases;
            result.Hours = stopWatch.Elapsed.Hours;
            result.Minutes = stopWatch.Elapsed.Minutes;
            result.Seconds = stopWatch.Elapsed.Seconds;
            result.MiliSeconds = stopWatch.Elapsed.Milliseconds;
            double seconds = (stopWatch.ElapsedMilliseconds / 1000.00);
            double averages;
            string averageDesc;
            if (result.Seconds == 0)
            {
                averages = result.DataProcessed / result.MiliSeconds;
                averageDesc = " Datas per Milisecond";
            }
            else
            {
                averages = result.DataProcessed / seconds;
                averageDesc = " Datas per Second";
            }
            result.AverageTime = "Averaging about " + averages.ToString("0.##") + averageDesc;
            return result;
        }

        public async Task<List<StudentDetailData>> GetStudentData(int testCase)
        {
            try
            {
                var GetStudentData = await _dataService.GetMany<StudentDetailData>("sp_GetTopStudent", new { testcase = testCase }, CommandType.StoredProcedure);
                var result = GetStudentData.ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Retrieving Data");
            }
        }

        public async Task<List<EnrollmentData>> GetEnrollmentData(Guid studentID)
        {
            try
            {
                var getEnrollmentHeader = await _dataService.GetMany<EnrollmentData>("sp_GetEnrollmentHeaderByStudentID", new { studentID = studentID }, CommandType.StoredProcedure);
                var result = getEnrollmentHeader.ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Retrieving Data");
            }
        }

        public async Task<List<EnrollmentDetailData>> GetEnrollmentDetailData(Guid headerID)
        {
            try
            {
                var getEnrollmentDetail = await _dataService.GetMany<EnrollmentDetailData>("sp_GetEnrollmentDetailByHeaderID", new { headerID = headerID }, CommandType.StoredProcedure);
                var result = getEnrollmentDetail.ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Retrieving Data");
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
        public async Task InsertEnrollment(int testCasesHeader, int testCasesDetail, List<LecturerDetailData> lecturers, List<CoursesData> courses, List<StudentDetailData> students)
        {
            try
            {
                for (int x = 0; x < students.Count; x++)
                { 
                    for (int y = 0; y < testCasesHeader; y++)
                    {
                        EnrollmentData enrollmentHeader = new EnrollmentData();
                        Guid id = Guid.NewGuid();
                        enrollmentHeader.ID = id;
                        enrollmentHeader.StudentID = students[x].ID;
                        enrollmentHeader.IsDeleted = false;
                        enrollmentHeader.GPAPerSemester = 0;
                        enrollmentHeader.TotalCoursePerSemester = 10;
                        enrollmentHeader.TotalCostPerSemester = rng.Next(10000000,99999999);
                        enrollmentHeader.TotalCoursePerSemester = rng.Next(10, 20);
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
                        for (int z = 0; z < testCasesDetail; z++)
                        {
                            EnrollmentDetailData enrollmentDetail= new EnrollmentDetailData();
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
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Running Test Cases");
            }
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

        public async Task<List<StudentDetailData>> updateStudent(int testCases, List<FacultyData> faculties, List<StudentDetailData> students)
        {
            //List<StudentDetailData> studentDatas = new List<StudentDetailData>();
            long StudentNumber = rng.NextInt64(1000000000, 9999999999);
            string firstName = "UPDATED_StudentFirstName_";
            string middleName = "UPDATED_StudentMiddleName_";
            string lastName = "UPDATED_StudentLastName_";
            string address = "UPDATED_JL Kemanggisan Raya";
            string country = "UPDATED_Indonesia";
            string province = "UPDATED_DKI Jakarta";
            string city = "UPDATED_Jakarta Barat";
            try
            {
                for (int x = 0; x < testCases; x++)
                {
                    students[x].FacultyID = faculties[rng.Next(0, faculties.Count() - 1)].ID;
                    string getModifier = students[x].StudentNumber.ToString().Substring(StudentNumber.ToString().Length - 4, 4);
                    students[x].StudentEmail = "UPDATED_"+firstName + "." + lastName + getModifier + "@Univ.ac.id";
                    students[x].StudentFirstName = firstName + getModifier;
                    students[x].StudentGPA = rng.NextDouble() * (4.0 - 1.0) + 1.0;
                    students[x].TotalCreditsEarned = rng.Next(0, 100);
                    if (middleName != null)
                    {
                        students[x].StudentMiddleName = middleName + getModifier;
                    }
                    students[x].StudentLastName = lastName + getModifier;
                    students[x].EnrolledYear = rng.Next(2000, 2023).ToString();
                    students[x].StudentDateOfBirth = generateDoB();
                    students[x].StudentPhoneNumber = generatePhoneNum();
                    students[x].StudentStreetNumber = rng.Next(0, 50);
                    students[x].StudentPostalCode = rng.Next(1000, 9999);
                    students[x].StudentCountry = country;
                    students[x].StudentProvince = province;
                    students[x].StudentStreetName = address;
                    students[x].StudentCity = city;
                    students[x].IsDeleted = false;
                    await _dataService.ExecuteNonQuery("sp_UpdateStudent", students[x], false, CommandType.StoredProcedure);
                }
                return students;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Saving Data");
            }
        }
        public async Task UpdateEnrollment(int testCasesHeader, int testCasesDetail, List<CoursesData> courses, List<StudentDetailData> students)
        {
            try
            {
                for (int x = 0; x < students.Count(); x++)
                {
                    var enrollmentHeader = await GetEnrollmentData(students[x].ID);

                    for (int y = 0; y < enrollmentHeader.Count(); y++)
                    {
                        var enrollmentDetail = await GetEnrollmentDetailData(enrollmentHeader[y].ID);
                        int countCourse = 0;
                        double countAverage = 0.0;
                        int countCost = 0;
                        int countCredit = 0;
                        for (int z=0 ; z < enrollmentDetail.Count(); z++)
                        {
                            enrollmentDetail[z].AssignmentScore = rng.Next(1, 100);
                            enrollmentDetail[z].MidExamScore = rng.Next(1, 100);
                            enrollmentDetail[z].FinalExamScore = rng.Next(1, 100);
                            enrollmentDetail[z].CourseAverageScore = (enrollmentDetail[z].AssignmentScore + enrollmentDetail[z].MidExamScore + enrollmentDetail[z].FinalExamScore) / 3.0;
                            countCourse++;
                            countCredit += courses[z].Credit;
                            countAverage += enrollmentDetail[z].CourseAverageScore;
                            countCost += courses[z].Cost;    
                            try
                            {
                                await _dataService.GetScalar("sp_UpdateEnrollmentDetail", enrollmentDetail[z], false, CommandType.StoredProcedure);
                            }
                            catch (Exception e)
                            {
                                throw new Exception("Error When Saving Header");
                            }
                        }
                        string type = enrollmentHeader[y].SemesterType;
                        enrollmentHeader[y].SemesterType = "UPDATED_" + type; 
                        enrollmentHeader[y].TotalCoursePerSemester = countCourse;
                        enrollmentHeader[y].TotalCostPerSemester = countCost;
                        enrollmentHeader[y].TotalCoursePerSemester = countCredit;
                        enrollmentHeader[y].GPAPerSemester = countAverage/countCourse;
                        await _dataService.ExecuteNonQuery("sp_UpdateEnrollmentHeader", enrollmentHeader[y], false, CommandType.StoredProcedure);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Running Test Cases");
            }
        }
    }
}
