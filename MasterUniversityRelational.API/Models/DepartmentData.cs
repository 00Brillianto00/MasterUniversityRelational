using System.ComponentModel.DataAnnotations;

namespace MasterUniversityRelational.API.Models
{
    public class DepartmentData
    {
        [Key]
        public Guid ID { get; set; }
        public Guid FacultyID { get; set;}
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
