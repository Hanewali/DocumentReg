using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace DocumentRegistry.Web.ApiModels
{
    public class NameSearchResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }

        private NameSearchResponse(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public static IEnumerable<NameSearchResponse> BuildFromDictionaryJson(string json)
        {
            var dictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(json);

            return dictionary.Select(x => new NameSearchResponse(x.Key, x.Value));
        }
    }
}