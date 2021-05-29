using System;
using System.Diagnostics.Contracts;
using Dapper;

namespace DocumentRegistry.Api.ApiModels.PostCompany
{
    public class PostCompany
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string ContractNumber { get; set; }
        public DateTime ContractDate { get; set; }
        public string PostOffice { get; set; }
        
        public static PostCompany BuildFromDomainModel(DomainModels.PostCompany postCompany)
        {
            return new()
            {
                Id = postCompany.Id,
                Name = postCompany.Name,
                Code = postCompany.Code,
                City = postCompany.City,
                Street = postCompany.Street,
                ContractNumber = postCompany.ContractNumber,
                ContractDate = postCompany.ContractDate,
                PostOffice = postCompany.PostOffice
            };
        }

        public DomainModels.PostCompany ToDomainModel(int userId, DomainModels.PostCompany postCompany = null)
        {
            postCompany ??= new DomainModels.PostCompany();

            if (postCompany.Id == null)
            {
                postCompany.CreateUserId = userId;
                postCompany.CreateDate = DateTime.Now;
                postCompany.IsActive = true;
            }

            postCompany.ModifyUserId = userId;
            postCompany.ModifyDate = DateTime.Now;
            postCompany.Name = Name;
            postCompany.City = City;
            postCompany.Street = Street;
            postCompany.ContractNumber = ContractNumber;
            postCompany.ContractDate = ContractDate;
            postCompany.PostOffice = PostOffice;
            
            return postCompany;
        }

        public DynamicParameters ToDynamicParameters()
        {
            var parameters = new DynamicParameters();
            
            parameters.Add("Id", Id);
            parameters.Add("Name", Name);
            parameters.Add("City", City);
            parameters.Add("Street", Street);
            parameters.Add("ContractNumber", ContractNumber);
            parameters.Add("ContractDate", ContractDate);
            parameters.Add("PostOffice",PostOffice);

            return parameters;
        }
    }
}