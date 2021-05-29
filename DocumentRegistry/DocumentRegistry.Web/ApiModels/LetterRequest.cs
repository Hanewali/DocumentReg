using DocumentRegistry.Web.Models.Letter;

namespace DocumentRegistry.Web.ApiModels
{
    public class LetterRequest
    {
        public int UserId { get; set; }
        public int BeginFrom { get; set; }
        public int Rows { get; set; }
        public Letter Letter { get; set; }
    }
}