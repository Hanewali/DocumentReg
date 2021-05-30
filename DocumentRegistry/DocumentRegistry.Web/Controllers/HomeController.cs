using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DocumentRegistry.Web.Models;
using DocumentRegistry.Web.Services.HomeService;
using Microsoft.AspNetCore.Http;

namespace DocumentRegistry.Web.Controllers
{
    [Route("[controller]/[action]")]
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
            return HttpContext.Session.Get("UserId") != null ? RedirectToAction("Index", "Letter") : RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserId");
            return RedirectToAction("Login", "Home");
        }
        
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LoginPost(Models.Home.Login request)
        {
            var loginResponse = _loginService.Verify(request);

            if (loginResponse.Verified)
                _loginService.Login(HttpContext.Session, loginResponse);

            return RedirectToAction("Index", "Letter");
        }
    }
}