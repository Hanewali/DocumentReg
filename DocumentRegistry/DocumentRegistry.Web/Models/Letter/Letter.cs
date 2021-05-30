using System;

namespace DocumentRegistry.Web.Models.Letter
{
    public class Letter
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public PostCompany.PostCompany PostCompany { get; set; }
        public DateTime Date { get; set; }
        public DateTime ReceiveDate { get; set; }
        public string Content { get; set; }
        public Employee.Employee Employee { get; set; }
        public Company.Company Company { get; set; }
        public DocumentType.DocumentType DocumentType { get; set; }
        public string Other { get; set; }
        public string PR { get; set; }
        public string PO { get; set; }
        public string CompanyName { get; set; }
        public string CompanyStreet { get; set; }
        public string CompanyCity { get; set; }
        public string CompanyPostalCode { get; set; }
        public string CompanyPostName { get; set; }
        public string CompanyPostStreet { get; set; }
        public string CompanyPostCity { get; set; }
        public string CompanyPostPostalCode { get; set; }
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
        public string Kind { get; set; } //inbox, outbox
        
        
    }
}