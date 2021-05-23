using System;
using Dapper.Contrib.Extensions;

namespace DocumentRegistry.DomainModels
{
    [Table("User")]
    public class User : IModel
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public User CreateUser { get; set; }
        public DateTime ModifyDate { get; set; }
        public User ModifyUser { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public bool IsAdmin { get; set; }
    }
}