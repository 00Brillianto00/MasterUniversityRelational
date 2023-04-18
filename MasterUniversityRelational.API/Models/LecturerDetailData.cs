using System.ComponentModel.DataAnnotations;

namespace MasterUniversityRelational.API.Models
{
    public class LecturerDetailData
    {
        [Key]
        public Guid ID { get; set; }
        public Guid LecturerID { get; set; }
        public string LecturerFirstName { get; set; }
        public string LecturerMiddleName { get; set; }
        public string LecturerLastName { get; set; }
        public DateTime LecturerDOB { get; set; }
        public string LecturerPhoneNumber { get; set; }
        public string LecturerStreetName { get; set; }
        public string LecturerStreetNumber { get; set; }
        public string LecturerCity { get; set; }
        public string LecturerProvince { get; set; }
        public string LecturerPostalCode { get; set; }
        public string LecturerCountry { get; set; }
        public bool IsDeleted { get; set; }
    }
}
