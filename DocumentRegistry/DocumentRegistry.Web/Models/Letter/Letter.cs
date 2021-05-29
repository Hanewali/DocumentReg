using System;

namespace DocumentRegistry.Web.Models.Letter
{
    public class Letter
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int PostCompanyId { get; set; }
        public DateTime Date { get; set; }
        public DateTime ReceiveDate { get; set; }
        public string Content { get; set; }
        public Employee.Employee EmployeeId { get; set; }
        public Company.Company CompanyId { get; set; }
        public DocumentType.DocumentType DocumentType { get; set; }
        
    }
}