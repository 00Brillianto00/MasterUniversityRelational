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
            //stopWatch.Start();
            switch (testCases)
            {
                case 1000:
                    //studentDatas = await InsertStudent(10, faculties);
                    stopWatch = await InsertEnrollment(10,10,lecturers,courses, faculties,10);
                    break;
                case 5000:
                    //studentDatas = await InsertStudent(50, faculties);
                    stopWatch = await InsertEnrollment(10, 10, lecturers, courses, faculties,50);
                    break;
                case 10000:
                    //studentDatas = await InsertStudent(100, faculties);
                    stopWatch = await InsertEnrollment(10, 10, lecturers, courses, faculties,100);
                    break;
                case 50000:
                    //studentDatas = await InsertStudent(500, faculties);
                    stopWatch = await InsertEnrollment(10, 10, lecturers, courses, faculties, 500);
                    break;
                case 100000:
                   // studentDatas = await InsertStudent(1000, faculties);
                    stopWatch = await InsertEnrollment(10, 10, lecturers, courses, faculties, 1000);
                    break;
            }
            //stopWatch.Stop();
            var testResult = getTestResult(stopWatch, testCases);
            var latestTestResult = await _dataService.GetMany<TestResult>("sp_GetLatestTestInsert", CommandType.StoredProcedure);
            if(latestTestResult.Count() == 0)
            {
                testResult.ID = 1;
            }
            else
            {
                testResult.ID = latestTestResult.FirstOrDefault().ID + 1;
            }
            await _dataService.GetScalar("sp_SaveTestResultInsert", testResult, false, CommandType.StoredProcedure);
            return testResult;
        }

        public async Task<TestResult> testUpdate(int testCases, List<FacultyData> faculties, List<LecturerDetailData> lecturers, List<CoursesData> courses)
        {
            Stopwatch swStudent = new Stopwatch();
            Stopwatch swEnrollment = new Stopwatch();
            TestResult result = new TestResult();
            TestResult result2 = new TestResult();
            List<StudentDetailData> studentDatas = new List<StudentDetailData>();
            //List<StudentDetailData> newStudentDatas = new List<StudentDetailData>();
            string errMsg = string.Empty;
            try
            {
                switch (testCases)
                {
                    case 1000:
                        studentDatas = await GetStudentData(10);
                        if (studentDatas.Count() < 10)
                        {
                            errMsg = "Not Enough Datas in Database, Please Repopulate Data by Inserting Datas";
                            throw new Exception();
                        }
                        //stopWatch.Start();
                        swStudent = await updateStudent(studentDatas.Count(), faculties, studentDatas);
                        swEnrollment = await UpdateEnrollment(10, 10, courses, studentDatas);
                        break;
                    case 5000:
                        studentDatas = await GetStudentData(50);
                        if (studentDatas.Count() < 50)
                        {
                            errMsg = "Not Enough Datas in Database, Please Repopulate Data by Inserting Datas";
                            throw new Exception();
                        }
                        //stopWatch.Start();
                        swStudent = await updateStudent(studentDatas.Count(), faculties, studentDatas);
                        swEnrollment = await UpdateEnrollment(10, 10, courses, studentDatas);
                        break;
                    case 10000:
                        studentDatas = await GetStudentData(100);
                        if (studentDatas.Count() < 100)
                        {
                            errMsg = "Not Enough Datas in Database, Please Repopulate Data by Inserting Datas";
                            throw new Exception();
                        }
                        //stopWatch.Start();
                        swStudent = await updateStudent(studentDatas.Count(), faculties, studentDatas);
                        swEnrollment = await UpdateEnrollment(10, 10, courses, studentDatas);
                        break;
                    case 50000:
                        studentDatas = await GetStudentData(500);
                        if (studentDatas.Count() < 500)
                        {
                            errMsg = "Not Enough Datas in Database, Please Repopulate Data by Inserting Datas";
                            throw new Exception();
                        }
                        //stopWatch.Start();
                        swStudent = await updateStudent(studentDatas.Count(), faculties, studentDatas);
                        swEnrollment = await UpdateEnrollment(10, 10, courses, studentDatas);
                        break;
                    case 100000:
                        studentDatas = await GetStudentData(1000);
                        if (studentDatas.Count() < 1000)
                        {
                            errMsg = "Not Enough Datas in Database, Please Repopulate Data by Inserting Datas";
                            throw new Exception();
                        }
                        //stopWatch.Start();
                        swStudent = await updateStudent(studentDatas.Count(), faculties, studentDatas);
                        swEnrollment = await UpdateEnrollment(10, 10, courses, studentDatas);
                        break;
                }

            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            //stopWatch.Stop();

            result = getTestResult(swStudent, testCases);
            result2 = getTestResult(swEnrollment, testCases);

            var total = await totalResult(result, swStudent.ElapsedMilliseconds, result2, swEnrollment.ElapsedMilliseconds, testCases);

            var latestTestResult = await _dataService.GetMany<TestResult>("sp_GetLatestTestUpdate", CommandType.StoredProcedure);

            if (latestTestResult.Count() == 0)
            {
                total.ID = 1;
            }
            else
            {
                total.ID = latestTestResult.FirstOrDefault().ID + 1;
            }
            await _dataService.GetScalar("sp_SaveTestResultUpdate", total, false, CommandType.StoredProcedure);
            return total;
        }

        public async Task<TestResult> testGet(int testCases)
        {
            Stopwatch stopWatch = new Stopwatch();
            TestResult result = new TestResult();

            stopWatch.Start();
            var data = await _dataService.GetMany<EnrollmentDataModel>("sp_GetTopEnrollmentDataModel", new { topData = testCases }, CommandType.StoredProcedure);
            stopWatch.Stop();

            if (data.Count() != testCases)
            {
                string errMsg = "Not Enough Datas in Database, Please Repopulate Data by Inserting Datas";
                throw new Exception(errMsg);
            }
            
            var testResult = getTestResult(stopWatch, testCases);
            var latestTestResult = await _dataService.GetMany<TestResult>("sp_GetLatestTestGet", CommandType.StoredProcedure);
            if (latestTestResult.Count() == 0)
            {
                testResult.ID = 1;
            }
            else
            {
                testResult.ID = latestTestResult.FirstOrDefault().ID + 1;
            }
            await _dataService.GetScalar("sp_SaveTestResultGet", testResult, false, CommandType.StoredProcedure);
            return testResult;
        }

        public async Task<TestResult> testDelete(int testCases)
        {
            Stopwatch stopWatch = new Stopwatch();
            TestResult result = new TestResult();
            var checkData = await _dataService.GetMany<EnrollmentDataModel>("sp_GetTopEnrollmentDataModel", new { topData = testCases }, CommandType.StoredProcedure);
            if (checkData.Count() < testCases)
            {
                string errMsg = "Not Enough Datas in Database, Please Repopulate Data by Inserting Datas";
                throw new Exception(errMsg);
            }
            
            stopWatch.Start();
            await _dataService.ExecuteNonQuery("sp_DeleteTopEnrollmentDataModelNEW", new { topData = testCases }, false, CommandType.StoredProcedure);
            stopWatch.Stop();
            
            
            var testResult = getTestResult(stopWatch, testCases);
            var latestTestResult = await _dataService.GetMany<TestResult>("sp_GetLatestTestDelete", CommandType.StoredProcedure);
            if (latestTestResult.Count() == 0)
            {
                testResult.ID = 1;
            }
            else
            {
                testResult.ID = latestTestResult.FirstOrDefault().ID + 1;
            }
            await _dataService.GetScalar("sp_SaveTestResultDelete", testResult, false, CommandType.StoredProcedure);
            return testResult;
        }

        private TestResult getTestResult(Stopwatch stopWatch, int testCases)
        {
            TestResult result = new TestResult();
            result.DataProcessed = testCases;
            result.Hours = stopWatch.Elapsed.Hours;
            result.Minutes = stopWatch.Elapsed.Minutes;
            result.Seconds = stopWatch.Elapsed.Seconds;

            //REMINDER TO FIX KE SEMULA
            result.MiliSeconds = stopWatch.Elapsed.Milliseconds;

            double milisecond = stopWatch.ElapsedMilliseconds * 1.00;
            // data / milisecond 
            result.AverageTime = milisecond / result.DataProcessed;
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
        public async Task<Stopwatch> InsertEnrollment(int testCasesHeader, int testCasesDetail, List<LecturerDetailData> lecturers, List<CoursesData> courses, List<FacultyData> faculties, int studentNom)
        {
            long StudentNumber = rng.NextInt64(1000000000, 9999999999);
            string firstName = "StudentFirstName_";
            string middleName = "StudentMiddleName_";
            string lastName = "StudentLastName_";
            string address = "JL Kemanggisan Raya";
            string country = "Indonesia";
            string province = "DKI Jakarta";
            string city = "Jakarta Barat";
            List<StudentDetailData> studentDatas = new List<StudentDetailData>();
            Stopwatch stopwatch = new Stopwatch();
            try
            {
                for (int x = 0; x < studentNom; x++)
                { 
                    StudentDetailData studentData = new StudentDetailData();
                    studentData.ID = Guid.NewGuid();
                    studentData.StudentID = Guid.NewGuid();
                    studentData.FacultyID = faculties[rng.Next(0, faculties.Count() - 1)].ID;
                    studentData.StudentNumber = StudentNumber++;
                    string getModifier = rng.NextInt64(1000000, 1000000000).ToString();
                    studentData.StudentEmail = firstName + "." + lastName + getModifier + "@Univ.ac.id";
                    studentData.StudentFirstName = firstName + getModifier;
                    studentData.StudentGPA = rng.NextDouble() * (4.0 - 1.0) + 1.0;
                    studentData.TotalCreditsEarned = rng.Next(0, 100);
                    if (middleName != null)
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
                    studentData.StudentCity = city;
                    studentData.IsDeleted = false;
                    studentDatas.Add(studentData);
                    try
                    {
                        stopwatch.Start();
                        await _dataService.GetScalar("sp_SaveStudentNEW", studentData, false, CommandType.StoredProcedure);
                        stopwatch.Stop();
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Error When Saving Student");
                    }

                    for (int y = 0; y < testCasesHeader; y++)
                    {
                        EnrollmentData enrollmentHeader = new EnrollmentData();
                        Guid id = Guid.NewGuid();
                        enrollmentHeader.ID = id;
                        enrollmentHeader.StudentID = studentData.ID;
                        enrollmentHeader.IsDeleted = false;
                        enrollmentHeader.GPAPerSemester = 0;
                        enrollmentHeader.TotalCoursePerSemester = 10;
                        enrollmentHeader.TotalCostPerSemester = rng.Next(10000000,99999999);
                        enrollmentHeader.TotalCreditsPerSemester = rng.Next(10, 20);
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
                            stopwatch.Start();
                            await _dataService.GetScalar("sp_SaveEnrollmentHeaderNEW", enrollmentHeader, false, CommandType.StoredProcedure);
                            stopwatch.Stop();
                        }
                        catch (Exception e)
                        {
                            throw new Exception("Error When Saving HeaderNEW");
                        }
                        for (int z = 0; z < testCasesDetail; z++)
                        {
                            EnrollmentDetailData enrollmentDetail= new EnrollmentDetailData();
                            enrollmentDetail.ID = Guid.NewGuid();
                            enrollmentDetail.EnrollmentHeaderID = id;
                            enrollmentDetail.CourseID = courses[z].ID;
                            enrollmentDetail.LecturerID = lecturers[rng.Next(0, lecturers.Count)].ID;
                            enrollmentDetail.AssignmentScore = rng.Next(1, 100);
                            enrollmentDetail.MidExamScore = rng.Next(1, 100);
                            enrollmentDetail.FinalExamScore = rng.Next(1, 100);
                            enrollmentDetail.CourseAverageScore = (enrollmentDetail.AssignmentScore + enrollmentDetail.MidExamScore + enrollmentDetail.FinalExamScore) / 3.0;
                            try
                            {
                                stopwatch.Start();  
                                await _dataService.GetScalar("sp_SaveEnrollmentDetailNEW", enrollmentDetail, false, CommandType.StoredProcedure);
                                stopwatch.Stop();
                            }
                            catch (Exception e)
                            {
                                throw new Exception("Error When Saving Detail");
                            }
                        }

                    }
                }
                return stopwatch;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Running Test Cases");
            }
        }


        public async Task<Stopwatch> updateStudent(int testCases, List<FacultyData> faculties, List<StudentDetailData> students)
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
            Stopwatch stopWatch = new Stopwatch();
            try
            {
                for (int x = 0; x < testCases; x++)
                {
                    students[x].FacultyID = faculties[rng.Next(0, faculties.Count() - 1)].ID;
                    string getModifier = rng.NextInt64(0, 9999999).ToString();
                    students[x].StudentEmail = "UPDATED_" + firstName + getModifier +"." + lastName + "@Univ.ac.id";
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
                    stopWatch.Start();
                    await _dataService.ExecuteNonQuery("sp_UpdateStudentNEW", students[x], false, CommandType.StoredProcedure);
                    stopWatch.Stop();
                }
                return stopWatch;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Saving Data");
            }
        }

        public async Task<Stopwatch> UpdateEnrollment(int testCasesHeader, int testCasesDetail, List<CoursesData> courses, List<StudentDetailData> students)
        {
            Stopwatch stopWatch = new Stopwatch();
            try
            {
                for (int x = 0; x < students.Count(); x++)
                { //UPDATE STUDENT

                    //loops1++;
                    //long StudentNumber = rng.NextInt64(1000000000, 9999999999);
                    //string firstName = "UPDATED_StudentFirstName_";
                    //string middleName = "UPDATED_StudentMiddleName_";
                    //string lastName = "UPDATED_StudentLastName_";
                    //string address = "UPDATED_JL Kemanggisan Raya";
                    //string country = "UPDATED_Indonesia";
                    //string province = "UPDATED_DKI Jakarta";
                    //string city = "UPDATED_Jakarta Barat";


                    //students[x].FacultyID = faculties[rng.Next(0, faculties.Count() - 1)].ID;
                    //string getModifier = rng.NextInt64(0,1000000000).ToString();    
                    //students[x].StudentEmail = "UPDATED_" + firstName+getModifier + "." + lastName  + "@Univ.ac.id";
                    //students[x].StudentFirstName = firstName + getModifier;
                    //students[x].StudentGPA = rng.NextDouble() * (4.0 - 1.0) + 1.0;
                    //students[x].TotalCreditsEarned = rng.Next(0, 100);
                    //if (middleName != null)
                    //{
                    //    students[x].StudentMiddleName = middleName + getModifier;
                    //}
                    //students[x].StudentLastName = lastName + getModifier;
                    //students[x].EnrolledYear = rng.Next(2000, 2023).ToString();
                    //students[x].StudentDateOfBirth = generateDoB();
                    //students[x].StudentPhoneNumber = generatePhoneNum();
                    //students[x].StudentStreetNumber = rng.Next(0, 50);
                    //students[x].StudentPostalCode = rng.Next(1000, 9999);
                    //students[x].StudentCountry = country;
                    //students[x].StudentProvince = province;
                    //students[x].StudentStreetName = address;
                    //students[x].StudentCity = city;
                    //students[x].IsDeleted = false;
                    //await _dataService.ExecuteNonQuery("sp_UpdateStudentNEW", students[x], false, CommandType.StoredProcedure);

                    var enrollmentHeader = await GetEnrollmentData(students[x].ID);
                   
                    for (int y = 0; y < enrollmentHeader.Count(); y++)
                    {
                        //UPDATE HEADER
                  
                        var enrollmentDetail = await GetEnrollmentDetailData(enrollmentHeader[y].ID);
                        int countCourse = 0;
                        double countAverage = 0.0;
                        int countCost = 0;
                        int countCredit = 0;
                        for (int z=0 ; z < enrollmentDetail.Count(); z++)
                        { //UPDATE DETAIL
       
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
                                stopWatch.Start();
                                await _dataService.GetScalar("sp_UpdateEnrollmentDetailNEW", enrollmentDetail[z], false, CommandType.StoredProcedure);
                                stopWatch.Stop();
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
                        stopWatch.Start();
                        await _dataService.ExecuteNonQuery("sp_UpdateEnrollmentHeaderNEW", enrollmentHeader[y], false, CommandType.StoredProcedure);
                        stopWatch.Stop();
                    }
                }
                return stopWatch;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Running Test Cases");
            }
        }

        public async Task<List<TestResult>> getTopTestDataInsert(int testCase)
        {
            try
            {
                var topTestDataInsert = await _dataService.GetMany<TestResult>("sp_GetTopTestInsert", new { testcase = testCase }, CommandType.StoredProcedure);
                var result = topTestDataInsert.ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Retrieving Data");
            }
        }

        public async Task<List<TestResult>> getTopTestDataUpdate(int testCase)
        {
            try
            {
                var topTestDataInsert = await _dataService.GetMany<TestResult>("sp_GetTopTestUpdate", new { testcase = testCase }, CommandType.StoredProcedure);
                var result = topTestDataInsert.ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Retrieving Data");
            }
        }

        public async Task<List<TestResult>> getTopTestDataGet(int testCase)
        {
            try
            {
                var topTestDataInsert = await _dataService.GetMany<TestResult>("sp_GetTopTestGet", new { testcase = testCase }, CommandType.StoredProcedure);
                var result = topTestDataInsert.ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Retrieving Data");
            }
        }
        public async Task<List<TestResult>> getTopTestDataDelete(int testCase)
        {
            try
            {
                var topTestDataInsert = await _dataService.GetMany<TestResult>("sp_GetTopTestDelete", new { testcase = testCase }, CommandType.StoredProcedure);
                var result = topTestDataInsert.ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Retrieving Data");
            }
        }

        public async Task<List<GraphData>> getTopTestGraphDataInsert()
        {
            try
            {
                var graphData = await _dataService.GetMany<GraphData>("sp_GetGraphDataPerformanceTestInsert", CommandType.StoredProcedure);
                var result = graphData.ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Retrieving Data");
            }
        }
        public async Task<List<GraphData>> getTopTestGraphDataUpdate()
        {
            try
            {
                var graphData = await _dataService.GetMany<GraphData>("sp_GetGraphDataPerformanceTestUpdate", CommandType.StoredProcedure);
                var result = graphData.ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Retrieving Data");
            }
        }

        public async Task<List<GraphData>> getTopTestGraphDataGet()
        {
            try
            {
                var graphData = await _dataService.GetMany<GraphData>("sp_GetGraphDataPerformanceTestGet", CommandType.StoredProcedure);
                var result = graphData.ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Retrieving Data");
            }
        }

        public async Task<List<GraphData>> getTopTestGraphDataDelete()
        {
            try
            {
                var graphData = await _dataService.GetMany<GraphData>("sp_GetGraphDataPerformanceTestDelete", CommandType.StoredProcedure);
                var result = graphData.ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Retrieving Data");
            }
        }

        public async Task<TestResult> totalResult(TestResult result,long elapsedMiliStudent, TestResult result2, long elapsedMiliEnrollment, int testCases)
        {
            //milis
            double count;
            double roundDown;
            TestResult total = new TestResult();
            total.MiliSeconds = result.MiliSeconds + result2.MiliSeconds;
            if (total.MiliSeconds > 1000)
            {
                count = total.MiliSeconds / 1000;
                roundDown = Math.Floor(count * 100) / 100;
                int storeSeconds = (int)roundDown;
                total.MiliSeconds = total.MiliSeconds - 1000;
                total.Seconds = total.Seconds + storeSeconds;
            }

            //secs
            total.Seconds = total.Seconds + result.Seconds + result2.Seconds;
            if (total.Seconds > 60)
            {
                count = total.Seconds / 60;
                roundDown = Math.Floor(count * 100) / 100;
                int storeMinutes = (int)roundDown;
                total.Seconds = total.Seconds - 60;
                total.Minutes = total.Minutes + storeMinutes;
            }
            total.DataProcessed = testCases;
            total.Minutes = total.Minutes + result.Minutes + result2.Minutes;
            total.Hours = result.Hours + result2.Hours;
            long totalMilis = elapsedMiliStudent + elapsedMiliEnrollment;
            double miliseconds = totalMilis * 1.0;
            total.AverageTime = miliseconds / total.DataProcessed;
            return total;
        }
    }
}
