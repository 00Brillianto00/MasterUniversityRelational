using System.ComponentModel.DataAnnotations;

namespace MasterUniversityRelational.API.Models
{
    public class EnrollmentData
    {
        [Key]
        public Guid ID { get; set; }
        public Guid StudentID { get; set; }
        public Guid LecturerID { get; set; }
        public string SemesterType{ get; set; }
        public string Year{ get; set; }
        public int TotalCreditsPerYear { get; set; }
        public int TotalCostPerSemester { get; set; }
        public int TotalCoursePerSemester{ get; set; }
        public double GPAPerSemester { get; set; }
        public bool IsDeleted { get; set; }

    }
}
