using System;
using Dapper;

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
                DocumentTypeId = letter.DocumentTypeId
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