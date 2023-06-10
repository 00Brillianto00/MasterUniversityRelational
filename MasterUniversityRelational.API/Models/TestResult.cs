namespace MasterUniversityRelational.API.Models
{
    public class TestResult
    {
        public int ID { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }
        public int MiliSeconds { get; set; }
        public int DataProcessed{ get; set; }
        public double AverageTime{ get; set; }

    }

    public class EnrollmentDataModel
    {
        public Guid ID { get; set; }
        public Guid FacultyID { get; set; }
        public long StudentNumber { get; set; }
        public string StudentEmail { get; set; }
        public string EnrolledYear { get; set; }
        public int TotalCreditsEarned { get; set; }
        public double StudentGPA { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentMiddleName { get; set; }
        public string StudentLastName { get; set; }
        public DateTime StudentDateOfBirth { get; set; }
        public string StudentPhoneNumber { get; set; }
        public string StudentStreetName { get; set; }
        public int StudentStreetNumber { get; set; }
        public string StudentCity { get; set; }
        public string StudentProvince { get; set; }
        public int StudentPostalCode { get; set; }
        public string StudentCountry { get; set; }
        public Guid EnrollmentID { get; set; }
        public string SemesterType { get; set; }
        public string Year { get; set; }
        public int TotalCreditsPerSemester { get; set; }
        public int TotalCostPerSemester { get; set; }
        public int TotalCoursePerSemester { get; set; }
        public double GPAPerSemester { get; set; }
        public Guid EnrollmentDetailID { get; set; }
        public Guid LecturerID { get; set; }
        public Guid CourseID { get; set; }
        public double AssignmentScore { get; set; }
        public double MidExamScore { get; set; }
        public double FinalExamScore { get; set; }
        public double CourseAverageScore { get; set; }
        public bool IsDeleted { get; set; }
    }

}
