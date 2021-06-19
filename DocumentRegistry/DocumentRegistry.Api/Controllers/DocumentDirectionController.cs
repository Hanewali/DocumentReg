using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using DocumentRegistry.Api.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;

namespace DocumentRegistry.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class DocumentDirectionController : BaseController
    {
        private readonly ILogger<DocumentDirectionController> _logger;

        public DocumentDirectionController(ILogger<DocumentDirectionController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetList()
        {
            var result = new List<DomainModels.DocumentDirection>();

            try
            {
                result.AddRange(DatabaseHelper.GetAll<DomainModels.DocumentDirection>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during DocumentDirection GetList");
                return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
            }

            return Ok(JsonSerializer.Serialize(result));
        }
    }
}