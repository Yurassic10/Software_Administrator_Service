using BLL.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        private readonly IServiceSuperAdmin _service;
        private readonly ILogger<AccountController> _logger;
        public AccountController(IServiceSuperAdmin service, ILogger<AccountController> logger)
        {
            _service = service;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "User");
            }
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(string email, string password)
        {
            var user = _service.GetByEmail(email);
            if (user == null)
            {
                _logger.LogWarning("Incorrect data for user: {Email}", email);
                ViewBag.Error = "Invalid user"; 
                return View("Error");
            }
            _logger.LogInformation("Correct enterring for user: {Email}", email);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.FirstName),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Role, user.RoleId == 1 ? "SuperAdmin" : user.RoleId == 2 ? "Admin" : "Client")
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            HttpContext.SignInAsync(principal);

            return RedirectToAction("Index", "User");
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync("CookieAuth").GetAwaiter().GetResult();
            return RedirectToAction("Login", "Account");
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
