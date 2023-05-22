namespace MasterUniversityRelational.API.Models
{
    public class TestResult
    {
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }
        public int MiliSeconds { get; set; }
        public int DataProcessed{ get; set; }
        public string AverageTime{ get; set; }

    }

    public class testData 
    {
        public int testCase{ get; set; }
    }

}
