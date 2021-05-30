using System.ComponentModel;

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
        [DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("Administrator")]
        public bool IsAdmin { get; set; }
    }
}