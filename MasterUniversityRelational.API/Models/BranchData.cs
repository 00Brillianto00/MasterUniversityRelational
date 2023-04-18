using System.ComponentModel.DataAnnotations;

namespace MasterUniversityRelational.API.Models
{
    public class BranchData
    {
        [Key]
        public Guid ID { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public string BranchStreetName { get; set; }
        public int BranchStreetNumber { get; set; }
        public string BranchCity { get; set; }
        public string BranchProvince { get; set; }
        public string BranchCountry { get; set; }
        public string BranchPostalCode { get; set;}
        public bool IsDeleted { get; set; }

    }
}
