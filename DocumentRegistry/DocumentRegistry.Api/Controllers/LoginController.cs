using System;
using System.Security.Cryptography;
using System.Text.Json;
using Dapper;
using DocumentRegistry.Api.ApiModels.Login;
using DocumentRegistry.Api.DomainModels;
using DocumentRegistry.Api.Helpers;
using Microsoft.AspNetCore.Mvc;


namespace DocumentRegistry.Api.Controllers
{
    [ApiController, Route("[controller]")]
    public class LoginController : BaseController
    {
        [HttpPost]
        public ActionResult VerifyLogin(LoginRequest request)
        {
            if (!ModelState.IsValid)
                return Problem();
            
            var parameters = new DynamicParameters();
            parameters.Add("Username", request.Username);
            parameters.Add("Password", request.Password);

            var user = DatabaseHelper.QueryFirst<User>($"select * from [User] where Login = '{request.Username}'");

            if (VerifyPassword(user.PasswordHash, user.PasswordSalt, request.Password))
            {
                return Ok(JsonSerializer.Serialize(LoginResponse.BuildFromUser(user)));
            }

            return Forbid();
        }

        private bool VerifyPassword(string passwordHash, string passwordSalt, string requestPlainPassword)
        {
            var hashBytes = Convert.FromBase64String(passwordHash);
            var salt = Convert.FromBase64String(passwordSalt);
            var pbkdf2 = new Rfc2898DeriveBytes(requestPlainPassword, salt, 100000);
            var hash = pbkdf2.GetBytes(20);
            for (int i=0; i < 20; i++)
                if (hashBytes[i] != hash[i])
                    return false;

            return true;
        }
    }
}