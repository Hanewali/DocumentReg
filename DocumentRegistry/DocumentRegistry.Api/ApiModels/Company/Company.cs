namespace DocumentRegistry.Api.ApiModels
{
    public class Company
    {
        public string Name { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Branch { get; set; }
        public string PostalCode { get; set; }
        public string PostName { get; set; }
        public string PostStreet { get; set; }
        public string PostCity { get; set; }
        public string PostPostalCode { get; set; }

        public static Company BuildFromDomainModel(DomainModels.Company company)
        {
            return new()
            {
                Name = company.Name,
                Street = company.Street,
                City = company.City,
                Branch = company.Branch,
                PostalCode = company.PostalCode,
                PostName = company.PostName,
                PostStreet = company.PostStreet,
                PostCity = company.PostCity,
                PostPostalCode = company.PostPostalCode
            };
        }
    }
}