using System.Collections.Generic;

namespace DocumentRegistry.Web.Models.User
{
    public class Search
    {
        public User SearchParameters { get; set; }
        public IEnumerable<User> Users { get; set; }

        public Search()
        {
            SearchParameters = new User();
            Users = new List<User>();
        }
    }
}