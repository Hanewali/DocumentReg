using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using Dapper;
using DocumentRegistry.Api.ApiModels.User;
using DocumentRegistry.Api.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace DocumentRegistry.Api.Controllers
{
    [ApiController] 
    [Route("[controller]/[action]")]  
    public class UserController : BaseController
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetList([FromQuery] int? beginFrom, [FromQuery] int? rows)
        {
            var result = new List<User>();

            try
            {
                var queryResult = DatabaseHelper.GetAll<DomainModels.User>();
                result.AddRange(queryResult
                    .Skip(beginFrom ?? 0)
                    .Take(rows ?? 10)
                    .Select(ApiModels.User.User.BuildFromDomainModel));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error during GetList");
                return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
            }
      
            return Ok(JsonSerializer.Serialize(result));
        }

        [HttpPost]
        public IActionResult Search(UserRequest model)
        {
            var result = new List<User>();

            try
            {
                var queryResult = DatabaseHelper.ExecProcedure<DomainModels.User>("CompanySearch", model.User.ToDynamicParameters());
                result.AddRange(queryResult
                    .Skip(model.BeginFrom ?? 0)
                    .Take(model.Rows ?? 10)
                    .Select(ApiModels.User.User.BuildFromDomainModel));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error during Search");
                return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
            }

            return Ok(JsonSerializer.Serialize(result));
        }

        [HttpGet]
        public IActionResult GetDetails([FromQuery] int Id)
        {
            var result = new User();
            try
            {
                result = DatabaseHelper.Get<User>(Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error during Search");
                return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
            }

            return Ok(JsonSerializer.Serialize(result));
        }
        
        [HttpPost]
        public IActionResult Create(UserRequest model)
        {
            try
            {
                var insertModel = model.User.ToDomainModel(model.UserId);

                var password = HashPassword(model.User.Password);
                
                insertModel.PasswordHash = password.Hash;
                insertModel.PasswordSalt = password.Salt;
                
                DatabaseHelper.Insert(insertModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error during Create");
                return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
            }

            return Ok();
        }

        [HttpPost]
        public IActionResult Edit(UserRequest model)
        {
            try
            {
                var existingUser = new DomainModels.User();
                if (model.User.Id != null)
                {
                    existingUser = DatabaseHelper.Get<DomainModels.User>(model.User.Id.Value);
                }

                var updateModel = model.User.ToDomainModel(model.UserId, existingUser);
                
                if (model.User.Password != null)
                {
                    var password = HashPassword(model.User.Password);
                    updateModel.PasswordHash = password.Hash;
                    updateModel.PasswordSalt = password.Salt;
                }
                
                var result = DatabaseHelper.Update(updateModel);

                if (!result)
                    throw new Exception("Record wasn't updated");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error during Edit");
                return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
            }

            return Ok();
        }

        [HttpPost]
        public IActionResult Delete(UserRequest model)
        {
            try
            {
                var parameters = new DynamicParameters();
                
                parameters.Add("Id", model.User.Id);
                parameters.Add("UserId", model.UserId);

                DatabaseHelper.ExecuteNoResult("DeleteCompany", parameters);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error during Delete");
                return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
            }

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