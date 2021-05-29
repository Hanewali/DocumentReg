namespace DocumentRegistry.Api.ApiModels.User
{
    public class RequestModel
    {
        public int UserId { get; set; }
        public int? BeginFrom { get; set; }
        public int? Rows { get; set; }
        public User User { get; set; }
    }
}