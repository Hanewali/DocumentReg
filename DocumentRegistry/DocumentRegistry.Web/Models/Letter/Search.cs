using System.Collections.Generic;

namespace DocumentRegistry.Web.Models.Letter
{
    public class Search
    {
        public Letter SearchParameters { get; set; }
        public IEnumerable<Letter> Letters { get; set; }

        public Search()
        {
            SearchParameters = new Letter();
            Letters = new List<Letter>();
        }
    }
}