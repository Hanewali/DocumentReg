namespace DocumentRegistry.Web.Models.Employee
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Company.Company Company { get; set; }
    }
}