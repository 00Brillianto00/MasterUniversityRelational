using System.ComponentModel.DataAnnotations;

namespace MasterUniversityRelational.API.Models
{
    public class LecturerData
    {
        [Key]
        public Guid ID { get; set; }
        public Guid DepartmentID { get; set; }
        public string LecturerCode { get; set; }
        public string LecturerEmail { get; set; }
        public DateTime JoinedDate { get; set; }
        public int Salary{ get; set; }
        public bool IsDeleted { get; set; }
    }
}
