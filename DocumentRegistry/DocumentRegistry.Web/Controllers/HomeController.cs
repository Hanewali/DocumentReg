using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DocumentRegistry.Web.Models;
using DocumentRegistry.Web.Services.HomeService;

namespace DocumentRegistry.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILoginService _loginService;

        public HomeController(ILogger<HomeController> logger, ILoginService loginService)
        {
            _logger = logger;
            _loginService = loginService;
        }

        public IActionResult Index()
        {
            //if logged in
            //redirect to Letters List
            //if not logged in
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Models.Home.Login request)
        {
            var loginResponse = _loginService.Verify(request);

            if (loginResponse.Verified)
            {
                _loginService.Login(loginResponse);
            }

            return RedirectToAction("Index", "Letter");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}