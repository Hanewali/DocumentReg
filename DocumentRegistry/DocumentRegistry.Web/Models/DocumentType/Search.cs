using System.Collections.Generic;

namespace DocumentRegistry.Web.Models.DocumentType
{
    public class Search
    {
        public DocumentType SearchParameters { get; set; }
        public IEnumerable<DocumentType> DocumentTypes { get; set; }

        public Search()
        {
            SearchParameters = new DocumentType();
            DocumentTypes = new List<DocumentType>();
        }
    }
}