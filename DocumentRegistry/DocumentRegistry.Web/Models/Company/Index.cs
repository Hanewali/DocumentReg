using System.Collections.Generic;

namespace DocumentRegistry.Web.Models.Company
{
    public class Index
    {
        public Index(IEnumerable<Company> companies)
        {
            Companies = companies;
        }

        public IEnumerable<Company> Companies { get; set; }
    }
}