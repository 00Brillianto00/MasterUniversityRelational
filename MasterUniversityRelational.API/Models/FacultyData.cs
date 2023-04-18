using System.ComponentModel.DataAnnotations;

namespace MasterUniversityRelational.API.Models
{
    public class FacultyData
    {
        [Key]
        public Guid ID { get; set; }
        public Guid BranchID { get; set; }
        public string FacultyCode { get; set; }
        public string FacultyName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
