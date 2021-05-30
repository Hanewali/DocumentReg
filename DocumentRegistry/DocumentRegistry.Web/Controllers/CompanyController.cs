﻿using System;
using System.Collections.Generic;
using System.Text.Json;
using DocumentRegistry.Web.ApiModels;
using DocumentRegistry.Web.Models.Company;
using DocumentRegistry.Web.Services.CompanyService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DocumentRegistry.Web.Controllers
{
    [Route("[controller]")]
    public class CompanyController : BaseController
    {
        private readonly ILogger<CompanyController> _logger;
        private static ICompanyService _companyService;

        public CompanyController(ICompanyService companyService, ILogger<CompanyController> logger)
        {
            _companyService = companyService;
            _logger = logger;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Search(CompanyRequest model)
        {
            var searchResult = new List<Company>();
            
            try
            {
                searchResult.AddRange(_companyService.Search(model.Company, model.BeginFrom, model.Rows, GetUserIdFromSession()));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during company search");
                return Problem();
            }

            return Ok(JsonSerializer.Serialize(searchResult));
        }

        [HttpGet]
        public IActionResult Details(int companyId)
        {
            var result = new Company();

            try
            {
                result = _companyService.GetDetails(companyId, GetUserIdFromSession());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during company search");
                return Problem();
            }

            return View(result);
        }
        
        [HttpPost]
        public IActionResult Create(Company company)
        {
            try
            {
                _companyService.Create(company, GetUserIdFromSession());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during company search");
                return Problem();
            }

            return RedirectToAction("Search", "Company");
        }

        [HttpPost]
        public IActionResult Edit(Company company)
        {
            try
            {
                _companyService.Edit(company, GetUserIdFromSession());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during company search");
                return Problem();
            }

            return RedirectToAction("Details", "Company", company.Id);
        }

        [HttpPost]
        public IActionResult Delete(int companyId)
        {
            try
            {
                _companyService.Delete(companyId, GetUserIdFromSession());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during company search");
                return Problem();
            }

            return RedirectToAction("Search", "Company");
        }
    }
}