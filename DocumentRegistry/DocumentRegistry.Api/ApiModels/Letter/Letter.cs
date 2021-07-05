using System;
using Dapper;
using DocumentRegistry.Api.DomainModels;

namespace DocumentRegistry.Api.ApiModels.Letter
{
    public class Letter
    {
        public int? Id { get; set; }
        public int Number { get; set; }
        public int PostCompanyId { get; set; }
        public DateTime Date { get; set; }
        public DateTime ReceiveDate { get; set; }
        public string Content { get; set; }
        public int EmployeeId { get; set; }
        public int CompanyId { get; set; }
        public int DocumentTypeId { get; set; }
        public int DocumentDirectionId { get; set; }
        public string Other { get; set; }
        public bool PR { get; set; }
        public bool PO { get; set; }
        public string CompanyStreet { get; set; }
        public string CompanyCity { get; set; }
        public string CompanyPostalCode { get; set; }
        public string CompanyPostName { get; set; }
        public string CompanyPostStreet { get; set; }
        public string CompanyPostCity { get; set; }
        public string CompanyPostPostalCode { get; set; }
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
        
        public static Letter BuildFromDomainModel(DomainModels.Letter letter)
        {
            return new()
            {
                Id = letter.Id,
                Number = letter.Number,
                PostCompanyId = letter.PostCompanyId,
                Date = letter.Date,
                ReceiveDate = letter.ReceiveDate,
                Content = letter.Content,
                EmployeeId = letter.EmployeeId,
                CompanyId = letter.CompanyId,
                DocumentTypeId = letter.DocumentTypeId,
                DocumentDirectionId = letter.DocumentDirectionId,
                Other = letter.Other,
                PR = letter.PR,
                PO = letter.PO,
                CompanyStreet = letter.CompanyStreet,
                CompanyCity = letter.CompanyCity,
                CompanyPostalCode = letter.CompanyPostalCode,
                CompanyPostName = letter.CompanyPostName,
                CompanyPostCity = letter.CompanyPostCity,
                CompanyPostStreet = letter.CompanyPostStreet,
                CompanyPostPostalCode = letter.CompanyPostPostalCode,
                EmployeeFirstName = letter.EmployeeFirstName,
                EmployeeLastName = letter.EmployeeLastName
            };
        }

        public DomainModels.Letter ToDomainModel(int userId, DomainModels.Letter letter = null)
        {
            letter ??= new DomainModels.Letter();

            if (letter.Id == 0)
            {
                letter.CreateUserId = userId;
                letter.CreateDate = DateTime.Now;
                letter.IsActive = true;
            }

            letter.ModifyUserId = userId;
            letter.ModifyDate = DateTime.Now;
            letter.Number = Number;
            letter.PostCompanyId = PostCompanyId;
            letter.Date = Date;
            letter.ReceiveDate = ReceiveDate;
            letter.Content = Content;
            letter.EmployeeId = EmployeeId;
            letter.CompanyId = CompanyId;
            letter.DocumentTypeId = DocumentTypeId;
            letter.DocumentDirectionId = DocumentDirectionId;
            letter.Other = Other;
            letter.PR = PR;
            letter.PO = PO;
            letter.CompanyStreet = CompanyStreet;
            letter.CompanyCity = CompanyCity;
            letter.CompanyPostalCode = CompanyPostalCode;
            letter.CompanyPostName = CompanyPostName;
            letter.CompanyPostCity = CompanyPostCity;
            letter.CompanyPostStreet = CompanyPostStreet;
            letter.CompanyPostPostalCode = CompanyPostPostalCode;
            letter.EmployeeFirstName = EmployeeFirstName;
            letter.EmployeeLastName = EmployeeLastName;
            
            return letter;
        }


        public DynamicParameters ToDynamicParameters()
        {
            var parameters = new DynamicParameters();
            
            parameters.Add("Id", Id);
            parameters.Add("Number", Number);
            parameters.Add("PostCompanyId", PostCompanyId);
            parameters.Add("Date", Date);
            parameters.Add("ReceiveDate", ReceiveDate);
            parameters.Add("Content", Content);
            parameters.Add("EmployeeId", EmployeeId);
            parameters.Add("CompanyId",CompanyId);
            parameters.Add("DocumentTypeId", DocumentTypeId);

            return parameters;
        }
    }
}