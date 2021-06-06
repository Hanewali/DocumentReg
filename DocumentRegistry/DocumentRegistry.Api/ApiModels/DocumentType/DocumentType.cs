using System;
using Dapper;

namespace DocumentRegistry.Api.ApiModels.DocumentType
{
    public class DocumentType
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        
        public static DocumentType BuildFromDomainModel(DomainModels.DocumentType documentType)
        {
            return new()
            {
                Id = documentType.Id,
                Name = documentType.Name
            };
        }

        public DomainModels.DocumentType ToDomainModel(int userId, DomainModels.DocumentType documentType = null)
        {
            documentType ??= new DomainModels.DocumentType();

            if (documentType.Id == 0)
            {
                documentType.CreateUserId = userId;
                documentType.CreateDate = DateTime.Now;
                documentType.IsActive = true;
            }

            documentType.ModifyUserId = userId;
            documentType.ModifyDate = DateTime.Now;
            
            documentType.Name = Name;
            
            return documentType;
        }


        public DynamicParameters ToDynamicParameters()
        {
            var parameters = new DynamicParameters();
            
            parameters.Add("Id", Id);
            parameters.Add("Name", Name);


            return parameters;
        }
    }
}