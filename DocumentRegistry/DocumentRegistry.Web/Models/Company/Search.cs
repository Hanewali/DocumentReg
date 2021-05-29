﻿using System.Collections.Generic;

namespace DocumentRegistry.Web.Models.Company
{
    public class Search
    {
        public Company SearchParameters { get; set; }
        public IEnumerable<Company> Companies { get; set; }
    }
}