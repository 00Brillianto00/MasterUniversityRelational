using System.ComponentModel.DataAnnotations;

namespace MasterUniversityRelational.API.Models
{
    public class EnrollmentDetailData
    {
        [Key]
        public Guid ID { get; set; }
        public Guid EnrollmentHeaderID { get; set; }
        public Guid CourseID { get; set; }
        public Guid LecturerID { get; set; }
        public double AssignmentScore { get; set; }
        public double MidExamScore { get; set; }
        public double FinalExamScore { get; set; }
        public double CourseAverageScore { get; set; }
        public bool IsDeleted { get; set; }
    }
}
