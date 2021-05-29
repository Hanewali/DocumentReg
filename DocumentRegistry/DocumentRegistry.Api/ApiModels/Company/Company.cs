using System;
using Dapper;

namespace DocumentRegistry.Api.ApiModels.Company
{
    public class Company
    {
        public int? Id { get; set; }
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
                Id = company.Id,
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

        public DomainModels.Company ToDomainModel(int userId, DomainModels.Company company = null)
        {
            company ??= new DomainModels.Company();

            if (company.Id == null)
            {
                company.CreateUserId = userId;
                company.CreateDate = DateTime.Now;
                company.IsActive = true;
            }

            company.ModifyUserId = userId;
            company.ModifyDate = DateTime.Now;
            company.Name = Name;
            company.Street = Street;
            company.City = City;
            company.Branch = Branch;
            company.PostalCode = PostalCode;
            company.PostName = PostName;
            company.PostStreet = PostStreet;
            company.PostCity = PostCity;
            company.PostPostalCode = PostPostalCode;
            
            return company;
        }


        public DynamicParameters ToDynamicParameters()
        {
            var parameters = new DynamicParameters();
            
            parameters.Add("CompanyId", Id);
            parameters.Add("CompanyName", Name);
            parameters.Add("Street", Street);
            parameters.Add("City", City);
            parameters.Add("Branch", Branch);
            parameters.Add("PostalCode", PostalCode);
            parameters.Add("PostName", PostName);
            parameters.Add("PostStreet",PostStreet);
            parameters.Add("PostCity", PostCity);
            parameters.Add("PostPostalCode", PostPostalCode);

            return parameters;
        }
    }
}