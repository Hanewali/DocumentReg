using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DocumentRegistry.Web.Models.User
{
    public class User
    {
        public int Id { get; set; }
        [DisplayName("Imię")]
        public string FirstName { get; set; }
        [DisplayName("Nazwisko")]
        public string LastName { get; set; }
        [DisplayName("Login")]
        public string Login { get; set; }
        [DisplayName("Nowe hasło")] 
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DisplayName("Administrator")]
        public bool IsAdmin { get; set; }
    }
}