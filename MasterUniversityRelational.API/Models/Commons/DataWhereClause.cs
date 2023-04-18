namespace MasterUniversityRelational.API.Models.Commons
{
    public class DataWhereClause
    {
        public string Operator { get; set; }

        public string Field { get; set; }

        public string Comparison { get; set; }

        public string Value { get; set; }
    }
}
