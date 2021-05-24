using System;
using Dapper.Contrib.Extensions;

namespace DocumentRegistry.Api.DomainModels
{
    [Table("User")]
    public class User : ModelBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public bool IsAdmin { get; set; }
    }
}