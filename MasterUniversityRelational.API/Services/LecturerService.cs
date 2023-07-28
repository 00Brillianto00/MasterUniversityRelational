using MasterUniversityRelational.API.Interfaces;
using MasterUniversityRelational.API.Models;
using System.Data;
using System.Diagnostics;

namespace MasterUniversityRelational.API.Services
{
    public class LecturerService : ILecturerService
    {
        private Random rng = new Random();
        private readonly IDataService _dataService;
        public LecturerService(IDataService dataService)
        {
            this._dataService = dataService;
        }
        public async Task<IEnumerable<LecturerDetailData>> GetAllAsync()
        {
            try
            {
                var data = await _dataService.GetMany<LecturerDetailData>("sp_GetAllLecturers", CommandType.StoredProcedure);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Retrieving Data");
            }
        }

        public async Task<LecturerDetailData> GetByIdAsync(Guid id)
        {
            try
            {
                var data = await _dataService.Get<LecturerDetailData>("sp_GetLecturerByID", new { id = id.ToString() }, CommandType.StoredProcedure);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Retrieving Data");
            }
        }

        public async Task<LecturerDetailData> Save(LecturerDetailData LecturerData)
        {
            LecturerData.ID = Guid.NewGuid();
            LecturerData.LecturerID = Guid.NewGuid();
            LecturerData.IsDeleted = false;
            try
            {
                var data = await _dataService.SaveOne("sp_SaveLecturer", LecturerData, false, CommandType.StoredProcedure);
                return LecturerData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Saving Data");
            }
        }

        public async Task<LecturerDetailData> Update(LecturerDetailData studentData)
        {
            try
            {
                var data = await _dataService.RunQuery("sp_UpdateLecturer", studentData, false, CommandType.StoredProcedure);
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
                var data = await _dataService.RunQuery("sp_DeleteLecturer", new { id = id.ToString() }, false, CommandType.StoredProcedure);
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

        private DateTime generateDate(string type)
        {
            DateTime randomDay;
            DateTime startDate = new DateTime(1940, 1, 1);
            DateTime endDate = new DateTime(1980, 1, 1);
            
            if (type.Equals("JOINED"))
            {
               startDate = new DateTime(1990, 1, 1);
               endDate = new DateTime(2023, 1, 1);
            }
            
            int rangeDate = (endDate - startDate).Days;
            randomDay = startDate.AddDays(rng.Next(rangeDate));

            return randomDay;
        }
        public async Task<TestResult> TestCase(LecturerDetailData lecturerData, int testCases, List<DepartmentData> departmetns)
        {
            Stopwatch stopWatch = new Stopwatch();
            TestResult result = new TestResult();
            string lecturerCode = lecturerData.LecturerCode.Substring(0,3);
            string firstName = lecturerData.LecturerFirstName;
            string middleName = lecturerData.LecturerMiddleName;
            string lastName = lecturerData.LecturerLastName;
            int index = 0;
            result.DataProcessed = testCases;
            try
            {
                stopWatch.Start();
                for (int x = 0; x < testCases; x++)
                {
                    index++;
                    lecturerData.ID = Guid.NewGuid();
                    lecturerData.LecturerID = Guid.NewGuid();
                    lecturerData.DepartmentID = departmetns[rng.Next(0, departmetns.Count() - 1)].ID;
                    lecturerData.LecturerCode = lecturerCode + index.ToString();
                    lecturerData.LecturerEmail = firstName + "." + lastName + index + "@Univ.ac.id";
                    lecturerData.LecturerFirstName = firstName + index.ToString();
                    lecturerData.Salary = rng.Next(10, 100) * 1000000;
                    if (!lecturerData.LecturerMiddleName.Equals(""))
                    {
                        lecturerData.LecturerMiddleName = middleName + index.ToString();
                    }
                    lecturerData.LecturerLastName = lastName + index.ToString();
                    lecturerData.JoinedDate = generateDate("JOINED");
                    lecturerData.LecturerDateOfBirth= generateDate("DOB");
                    lecturerData.LecturerPhoneNumber = generatePhoneNum();
                    lecturerData.LecturerStreetNumber = rng.Next(0, 50);
                    lecturerData.LecturerPostalCode = rng.Next(1000, 9999);
                    lecturerData.IsDeleted = false;
                    var data = await _dataService.SaveOne("sp_SaveLecturer", lecturerData, false, CommandType.StoredProcedure);
                }
                stopWatch.Stop();

                result.Hours = stopWatch.Elapsed.Hours;
                result.Minutes = stopWatch.Elapsed.Minutes;
                result.Seconds = stopWatch.Elapsed.Seconds;
                result.MiliSeconds = stopWatch.Elapsed.Milliseconds;
                result.AverageTime = result.DataProcessed / result.MiliSeconds;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Saving Data");
            }
        }
    }
}
