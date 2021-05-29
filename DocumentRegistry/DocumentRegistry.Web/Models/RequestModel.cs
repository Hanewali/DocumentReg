using System.Text.Json;

namespace DocumentRegistry.Web.Models
{
    public class RequestModel
    {
        public int UserID { get; set; }
        public IModel Model { get; set; }
    }
}