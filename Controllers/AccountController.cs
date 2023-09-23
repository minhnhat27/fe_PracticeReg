using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using MyWebAPI.Data.ViewModels;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Logging;

namespace DangKyPhongThucHanhCNTTApi.Controllers
{
    public class AccountController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:44304/api");
        private readonly HttpClient _client;
        private readonly IConfiguration _config;

        public AccountController(IConfiguration config)
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
            _config = config;
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        private ClaimsPrincipal validateToken(string jwtToken)
        {
            IdentityModelEventSource.ShowPII = true;
            SecurityToken securityToken;

            var validationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _config["JWT:ValidIssuer"],
                ValidAudience = _config["JWT:ValidAudience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SerectKey"]!))
            };

            ClaimsPrincipal claimsPrincipal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out securityToken);
            return claimsPrincipal;
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(LoginModel loginRequest)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.MSCB = loginRequest.MSCB;
                return View("SignIn");
            }
            var result = await _client.PostAsJsonAsync(baseAddress + "/User/SignIn", loginRequest);
            var content = await result.Content.ReadAsStringAsync();
            var loginResp = JsonSerializer.Deserialize<ApiResponse>(content);

            if (!result.IsSuccessStatusCode)
            {
                ModelState.AddModelError("ErrorPassword", "Tài khoản hoặc mật khẩu không chính xác.");
                ViewBag.MSCB = loginRequest.MSCB;
                return View("SignIn");
            }

            var token = loginResp!.data!;
            var userClaims = validateToken(token);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTime.UtcNow.AddHours(8),
                IsPersistent = false,
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userClaims, authProperties);
            var cookieOptions = new CookieOptions
            {
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Path = "/",
                HttpOnly = true,
                IsEssential = true,
                Expires = DateTime.Now.AddDays(1)
            };
            Response.Cookies.Append("AuthToken", token, cookieOptions);

            return RedirectToAction("Index", "Home");

        }

        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("AuthToken");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("SignIn");
        }
    }
}
