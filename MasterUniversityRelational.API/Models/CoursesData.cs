using System.ComponentModel.DataAnnotations;

namespace MasterUniversityRelational.API.Models
{
    public class CoursesData
    {
        [Key]
        public Guid ID { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public string Syllabus { get; set; }
        public int Credit { get; set; }
        public int Cost { get; set; }
        public bool IsDeleted { get; set; }

    }
}
