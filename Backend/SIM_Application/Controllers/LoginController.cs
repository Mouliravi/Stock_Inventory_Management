using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BC = BCrypt.Net.BCrypt;
using SIM_Application.Models;
using Microsoft.EntityFrameworkCore;


namespace SIM_Application.Controllers
{
    public class LoginController : Controller
    {
        private readonly StockInventoryManagementSystemContext _context;
        public LoginController(StockInventoryManagementSystemContext context)
        {
            _context = context;
        }
        // GET: AuthController
        public ActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            if (claimUser.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // GET: AuthController
        [HttpPost]
        public async Task<IActionResult> Login(Admin admin)
        {
            var account = _context.Admins.SingleOrDefault(x => x.Name == admin.Name);
            if (account != null && BC.Verify(admin.Password, account.Password))
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, account.Name)
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);
                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = false
                };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), properties);
                return RedirectToAction("Index", "Home");
            }
            ViewData["Message"] = "Creditianls invalid";
            return View();
        }
    }
}