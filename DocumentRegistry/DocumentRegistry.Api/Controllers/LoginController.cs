using System;
using System.Security.Cryptography;
using Dapper;
using DocumentRegistry.Api.ApiModels.Login;
using DocumentRegistry.Api.DomainModels;
using DocumentRegistry.Api.Helpers;
using DocumentRegistry.Api.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace DocumentRegistry.Api.Controllers
{
    [ApiController, Route("[controller]/[action]")]
    public class LoginController : Controller
    {
        [HttpPost]
        public IActionResult VerifyLogin([FromBody] LoginRequest request)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Username", request.Username);
            parameters.Add("Password", request.Password);

            var user = DatabaseHelper.GetValue<User>($"select * from User where Login = {request.Username}");

            if (VerifyPassword(user.PasswordHash, user.PasswordSalt, request.Password))
            {
                return Ok(LoginResponse.BuildFromUser(user).ToJson());
            }

            return Forbid();
        }

        private bool VerifyPassword(string passwordHash, string passwordSalt, string requestPlainPassword)
        {
            var passwordHashBytes = Convert.FromBase64String(passwordHash);
            var passwordSaltBytes = Convert.FromBase64String(passwordSalt);
            var pbkdf2 = new Rfc2898DeriveBytes(requestPlainPassword, passwordSaltBytes, 100000);
            var hash = pbkdf2.GetBytes(20);
            /* Compare the results */
            for (var i=0; i < 10; i++)
                if (passwordHashBytes[i] != hash[i])
                    return false;

            return true;
        }
    }
}