using System;
using Microsoft.AspNetCore.Routing.Constraints;

namespace DocumentRegistry.Web.Models.PostCompany
{
    public class PostCompany
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string ContractNumber { get; set; }
        public DateTime ContractDate { get; set; }
        public string PostOffice { get; set; }
    }
}