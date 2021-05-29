namespace DocumentRegistry.Api.ApiModels.Employee
{
    public class RequestModel
    {
        public int UserId { get; set; }
        public int? BeginFrom { get; set; }
        public int? Rows { get; set; }
        public Employee Employee { get; set; }
    }
}