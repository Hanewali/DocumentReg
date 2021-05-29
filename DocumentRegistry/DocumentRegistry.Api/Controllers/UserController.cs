using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;

namespace DocumentRegistry.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : BaseController
    {
        // GET
        public IActionResult Index()
        {
            return Ok();
        }
        
        private Password HashPassword(string plainPassword)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(plainPassword, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);
            string savedPasswordHash = Convert.ToBase64String(hash);
            var savedSalt =  Convert.ToBase64String(salt);

            return new Password
            {
                Hash = savedPasswordHash,
                Salt = savedSalt
            };
        } 
    }
}