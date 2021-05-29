using System.Collections.Generic;
using DocumentRegistry.Web.Models.User;

namespace DocumentRegistry.Web.Services.UserService
{
    public interface IUserService
    {
        IEnumerable<User> Search(int beginFrom, int rows, int userId);
        IEnumerable<User> Search(User user, int beginFrom, int rows, int userId);
        User GetDetails(int editedUserId, int userId); 
        void Create(User user, int userId);
        void Edit(User user, int userId);
        void Delete(int deletedUserId, int userId);
    }
}