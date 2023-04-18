using System.ComponentModel.DataAnnotations;

namespace MasterUniversityRelational.API.Models
{
    public class EnrollmentDetailData
    {
        [Key]
        public Guid ID { get; set; }
        public Guid EnrollmentHeaderID { get; set; }
        public Guid CourseID { get; set; }
        public int AssignmentScore { get; set; }
        public int MidExamScore { get; set; }
        public int FinalExamScore { get; set; }
        public Double CourseAverageScore { get; set; }
        public bool IsDeleted { get; set; }
    }
}
