using System.Collections.Generic;
using DocumentRegistry.Web.Models.DocumentDirection;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DocumentRegistry.Web.Services.DocumentDirectionService
{
    public interface IDocumentDirectionService
    {
        IEnumerable<DocumentDirection> GetList(int userId);
    }
}