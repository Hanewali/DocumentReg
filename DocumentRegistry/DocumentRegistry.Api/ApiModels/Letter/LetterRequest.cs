namespace DocumentRegistry.Api.ApiModels.Letter
{
    public class LetterRequest
    {
        public int UserId { get; set; }
        public int? BeginFrom { get; set; }
        public int? Rows { get; set; }
        public Letter Letter { get; set; }
    }
}