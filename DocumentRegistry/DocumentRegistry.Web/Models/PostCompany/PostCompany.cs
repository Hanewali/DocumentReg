﻿using System;
using System.ComponentModel;
using Microsoft.AspNetCore.Routing.Constraints;

namespace DocumentRegistry.Web.Models.PostCompany
{
    public class PostCompany
    {
        public int Id { get; set; }
        [DisplayName("Nazwa")]
        public string Name { get; set; }
        [DisplayName("Kod")]
        public string Code { get; set; }
        [DisplayName("Miasto")]
        public string City { get; set; }
        [DisplayName("Ulica")]
        public string Street { get; set; }
        [DisplayName("Numer umowy")]
        public string ContractNumber { get; set; }
        [DisplayName("Data umowy")]
        public DateTime ContractDate { get; set; }
        [DisplayName("Urząd pocztowy")]
        public string PostOffice { get; set; }
    }
}