using MasterUniversityRelational.API.Interfaces;
using MasterUniversityRelational.API.Models;
using System.Data;
using System.Diagnostics;

namespace MasterUniversityRelational.API.Services
{
    public partial class StudentService : IStudentService
    {
        private Random rng = new Random();
        private readonly IDataService _dataService;
        public StudentService (IDataService dataService)
        {
            this._dataService = dataService;
        }
        public async Task<IEnumerable<StudentDetailData>> GetAllAsync()
        {
            try
            {
                var data = await _dataService.GetMany<StudentDetailData>("sp_GetAllStudents", CommandType.StoredProcedure);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Retrieving Data");
            }
        }

        public async Task<StudentDetailData> GetByIdAsync(Guid id)
        {
            try
            {
                var data = await _dataService.GetOne<StudentDetailData>("sp_GetStudentByID", new { id = id.ToString() }, CommandType.StoredProcedure);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Retrieving Data");
            }
        }

        public async Task<StudentDetailData> Save(StudentDetailData studentData)
        {
            studentData.ID = Guid.NewGuid();
            studentData.StudentID = Guid.NewGuid();
            studentData.IsDeleted = false;
            try
            {
                var data = await _dataService.GetScalar("sp_SaveStudent", studentData, false, CommandType.StoredProcedure);
                return studentData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Saving Data");
            }
        }

        public async Task<StudentDetailData> Update(StudentDetailData studentData)
        {
            try
            {
                var data = await _dataService.ExecuteNonQuery("sp_UpdateStudent", studentData, false, CommandType.StoredProcedure);
                return studentData;
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
                var data = await _dataService.ExecuteNonQuery("sp_DeleteStudent", new { id = id.ToString() }, false, CommandType.StoredProcedure);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Deleting Data");
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
        public async Task<TestResult> TestCase(StudentDetailData studentData, int testCases, List<FacultyData> faculties)
        {
            Stopwatch stopWatch = new Stopwatch();
            TestResult result = new TestResult();
            long StudentNumber = studentData.StudentNumber;
            string firstName = studentData.StudentFirstName;
            string middleName = studentData.StudentMiddleName;
            string lastName = studentData.StudentLastName;
            result.DataProcessed = testCases;
            try
            {
                stopWatch.Start();
                for (int x = 0; x < testCases; x++)
                {
                    studentData.ID = Guid.NewGuid();
                    studentData.StudentID = Guid.NewGuid();
                    studentData.FacultyID = faculties[rng.Next(0,faculties.Count()-1)].ID;
                    studentData.StudentNumber = StudentNumber++;
                    string getModifier = studentData.StudentNumber.ToString().Substring(StudentNumber.ToString().Length - 4,4);
                    studentData.StudentEmail = firstName + "." +lastName+ getModifier + "@Univ.ac.id";
                    studentData.StudentFirstName = firstName + getModifier;
                    studentData.StudentGPA = rng.NextDouble() * (4.0 - 1.0) + 1.0;
                    studentData.TotalCreditsEarned = rng.Next(0, 100);
                    if (studentData.StudentMiddleName != null)
                    {
                        studentData.StudentMiddleName = middleName + getModifier;
                    }
                    studentData.StudentLastName = lastName + getModifier;
                    studentData.EnrolledYear = rng.Next(2000, 2023).ToString();
                    studentData.StudentDateOfBirth = generateDoB();
                    studentData.StudentPhoneNumber = generatePhoneNum();
                    studentData.StudentStreetNumber = rng.Next(0,50);
                    studentData.StudentPostalCode = rng.Next(1000,9999);
                    studentData.IsDeleted = false;
                    var data = await _dataService.GetScalar("sp_SaveStudent", studentData, false, CommandType.StoredProcedure);
                }
                stopWatch.Stop();

                result.Hours = stopWatch.Elapsed.Hours;
                result.Minutes = stopWatch.Elapsed.Minutes;
                result.Seconds = stopWatch.Elapsed.Seconds;
                result.MiliSeconds = stopWatch.Elapsed.Milliseconds;
                double seconds = (stopWatch.ElapsedMilliseconds / 1000.00);
                double averages = result.DataProcessed /seconds;
                result.AverageTime = averages.ToString("0.##")+ " Datas Per Second";
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Saving Data");
            }
        }
    }
}
