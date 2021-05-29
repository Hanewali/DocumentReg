using System.Collections.Generic;
using DocumentRegistry.Web.Models.DocumentType;

namespace DocumentRegistry.Web.Services.DocumentTypeService
{
    public interface IDocumentTypeService
    {
        IEnumerable<DocumentType> Search(int beginFrom, int rows, int userId);
        IEnumerable<DocumentType> Search(DocumentType documentType, int beginFrom, int rows, int userId);
        DocumentType GetDetails(int documentTypeId, int userId); 
        void Create(DocumentType documentType, int userId);
        void Edit(DocumentType documentType, int userId);
        void Delete(int documentTypeId, int userId);
    }
}