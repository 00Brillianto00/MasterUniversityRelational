using System.ComponentModel.DataAnnotations;

namespace MasterUniversityRelational.API.Models
{
    public class LecturerDetailData
    {
        [Key]
        public Guid ID { get; set; }
        public Guid LecturerID { get; set; }
        public Guid DepartmentID { get; set; }
        public string LecturerCode { get; set; }
        public string LecturerEmail { get; set; }
        public DateTime JoinedDate { get; set; }
        public int Salary { get; set; }
        public string LecturerFirstName { get; set; }
        public string LecturerMiddleName { get; set; }
        public string LecturerLastName { get; set; }
        public DateTime LecturerDateOfBirth { get; set; }
        public string LecturerPhoneNumber { get; set; }
        public string LecturerStreetName { get; set; }
        public int LecturerStreetNumber { get; set; }
        public string LecturerCity { get; set; }
        public string LecturerProvince { get; set; }
        public int LecturerPostalCode { get; set; }
        public string LecturerCountry { get; set; }
        public bool IsDeleted { get; set; }
    }
}
