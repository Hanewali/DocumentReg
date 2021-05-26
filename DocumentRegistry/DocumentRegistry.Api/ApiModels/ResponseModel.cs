using System.Text.Json;

namespace DocumentRegistry.Web.Models
{
    public class ResponseModel
    {
        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}