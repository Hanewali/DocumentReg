using System.Text.Json;

namespace DocumentRegistry.Web.Models
{
    public class RequestModel
    {
        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}