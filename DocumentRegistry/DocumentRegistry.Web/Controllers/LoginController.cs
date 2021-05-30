using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DocumentRegistry.Web.Services.HomeService;

namespace DocumentRegistry.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly ILoginService _loginService;

        public LoginController(ILogger<LoginController> logger, ILoginService loginService)
        {
            _logger = logger;
            _loginService = loginService;
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserId");
            return RedirectToAction("Login", "Login");
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

            return RedirectToAction("Search", "Letter");
        }
    }
}