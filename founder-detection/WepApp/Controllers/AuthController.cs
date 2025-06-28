using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebAPI.DTO;

namespace WepApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _loginApiUrl;

        public AuthController(IHttpClientFactory factory, IConfiguration configuration)
        {
                _httpClient = factory.CreateClient("ApiWithAuth");
                _loginApiUrl = configuration["ApiSettings:BaseUrl"] + configuration["ApiSettings:UsersPath"];
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginRequest
            {
                Email = string.Empty,
                Password = string.Empty
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var response = await _httpClient.PostAsJsonAsync(_loginApiUrl + "/login", model);
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Invalid email or password.");
                return View(model);
            }

            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, model.Email),
                new Claim(ClaimTypes.Role, result.RoleName),
                new Claim("UserId", result.AccountId),
                new Claim("Token", result.Token)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Auth");
        }
    }
}