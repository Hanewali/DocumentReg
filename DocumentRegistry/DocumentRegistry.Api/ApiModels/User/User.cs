using System;
using System.Security.Cryptography;
using Dapper;
using DocumentRegistry.Api.ApiModels.Login;
using DocumentRegistry.Api.Controllers;

namespace DocumentRegistry.Api.ApiModels.User
{
    public class User
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public bool IsAdmin { get; set; }
        
        public static User BuildFromDomainModel(DomainModels.User user)
        {
            return new()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Login = user.Login,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                PasswordSalt = user.PasswordSalt,
                IsAdmin = user.IsAdmin
            };
        }

        public DomainModels.User ToDomainModel(int userId, DomainModels.User user = null)
        {
            user ??= new DomainModels.User();

            if (user.Id == 0)
            {
                user.CreateUserId = userId;
                user.CreateDate = DateTime.Now;
                user.IsActive = true;
            }

            user.ModifyUserId = userId;
            user.ModifyDate = DateTime.Now;
            user.FirstName = FirstName;
            user.LastName = LastName;
            user.Login = Login;
            user.Email = Email;
            user.PasswordHash = PasswordHash;
            user.PasswordSalt = PasswordSalt;
            user.IsAdmin = IsAdmin;
            
            return user;
        }

        public DynamicParameters ToDynamicParameters()
        {
            var parameters = new DynamicParameters();
            
            parameters.Add("Id", Id);
            parameters.Add("FirstName", FirstName);
            parameters.Add("LastName", LastName);
            parameters.Add("Login", Login);
            parameters.Add("Email", Email);
            parameters.Add("PasswordHash", PasswordHash);
            parameters.Add("PasswordSalt", PasswordSalt);
            parameters.Add("IsAdmin",IsAdmin);

            return parameters;
        }
    }
}