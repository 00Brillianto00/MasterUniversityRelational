using System.ComponentModel.DataAnnotations;

namespace MasterUniversityRelational.API.Models
{
    public class StudentDetailData
    {
        [Key]
        public Guid ID { get; set; }
        public Guid StudentID { get; set; }
        public Guid FacultyID { get; set; }
        public string StudentNumber { get; set; }
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
        public bool IsDeleted { get; set; }
    }

}
