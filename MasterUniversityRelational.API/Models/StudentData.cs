using System.ComponentModel.DataAnnotations;

namespace MasterUniversityRelational.API.Models
{
    public class StudentData
    {
        [Key]
        public Guid ID { get; set; }
        public Guid FacultyID { get; set; }
        public string StudentNumber { get; set; }
        public string StudentEmail { get; set; }
        public string EnrolledYear { get; set; }
        public int TotalCreditsEarned { get; set; }
        public double StudentGPA { get; set; }
        public bool IsDeleted { get; set; }
    }
}
