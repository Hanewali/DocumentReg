using System.Collections.Generic;

namespace DocumentRegistry.Web.Models.PostCompany
{
    public class Search
    {
        public PostCompany SearchParameters { get; set; }
        public IEnumerable<PostCompany> PostCompanies { get; set; }

        public Search()
        {
            SearchParameters = new PostCompany();
            PostCompanies = new List<PostCompany>();
        }
    }
}