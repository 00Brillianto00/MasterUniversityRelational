using System.ComponentModel.DataAnnotations;

namespace MasterUniversityRelational.API.Models
{
    public class StudenDetailData
    {
        [Key]
        public Guid ID { get; set; }
        public Guid StudentID { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentMiddleName { get; set; }
        public string StudentLastName { get; set;}
        public DateTime StudentDOB{ get; set;}
        public string StudentPhoneNumber { get; set; }
        public string StudentStreetName { get; set; }
        public string StudentStreetNumber { get; set; }
        public string StudentCity { get; set; }
        public string StudentProvince { get; set; }
        public string StudentPostalCode { get; set; }
        public string StudentCOpuntry { get; set; }
        public bool IsDeleted { get; set; }
    }
}
