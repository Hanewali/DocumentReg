using System.Collections.Generic;
using DocumentRegistry.Web.Models.Letter;

namespace DocumentRegistry.Web.Services.LetterService
{
    public interface ILetterService
    {
        IEnumerable<Letter> Search(int beginFrom, int rows, int userId);
        IEnumerable<Letter> Search(Letter letter, int beginFrom, int rows, int userId);
        Letter GetDetails(int letterId, int userId); 
        void Create(Letter letter, int userId);
        void Edit(Letter letter, int userId);
        void Delete(int letterId, int userId);
    }
}