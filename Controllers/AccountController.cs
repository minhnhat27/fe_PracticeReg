using Microsoft.AspNetCore.Mvc;
using MyWebAPI.Data.ViewModels;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Logging;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Google;
using NuGet.Protocol.Plugins;
using Microsoft.AspNetCore.Identity;
using Org.BouncyCastle.Asn1.Pkcs;

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
            var message = TempData["ErrorMessage"] as string;
            if (!message.IsNullOrEmpty())
            {
                ViewBag.Message = message;
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
            var loginResp = JsonConvert.DeserializeObject<ApiResponse>(content)!;

            if (!result.IsSuccessStatusCode)
            {
                ModelState.AddModelError("ErrorPassword", "Tài khoản hoặc mật khẩu không chính xác.");
                ViewBag.MSCB = loginRequest.MSCB;
                return View("SignIn");
            }

            var token = loginResp.data!;
            var userClaims = validateToken(token);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTime.UtcNow.AddDays(1),
                IsPersistent = false
            };
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userClaims, authProperties);
            var cookieOptions = new CookieOptions
            {
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Path = "/",
                //HttpOnly = true,
                IsEssential = true,
                Expires = DateTime.Now.AddDays(1)
            };
            Response.Cookies.Append("AuthToken", token, cookieOptions);

            return RedirectToAction("Index", "Home");

        }

        [AllowAnonymous]
        public async Task SignInGoogle()
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties()
            {
                RedirectUri = Url.Action("CallBackGoogle"),
                ExpiresUtc = DateTime.UtcNow.AddDays(1),
                IsPersistent = false
            });
        }

        public async Task<IActionResult> CallBackGoogle()
        {
            var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
            if (result.Succeeded)
            {
                var userEmail = result.Principal.FindFirstValue(ClaimTypes.Email);
                var userPic = result.Principal.FindFirstValue("urn:google:picture")!;

                var response = await _client.PostAsJsonAsync(baseAddress + "/User/ExternalLogin", userEmail);
                var content = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<ApiResponse>(content)!;

                if (!data.success)
                {
                    TempData["ErrorMessage"] = "Tài khoản Google không hợp lệ. Vui lòng đăng nhập tài khoản khác!";
                    await HttpContext.SignOutAsync();
                    return RedirectToAction("SignIn");
                }

                var token = data.data!;
                var userClaims = validateToken(token);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userClaims);
                var cookieOptions = new CookieOptions
                {
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Path = "/",
                    //HttpOnly = true,
                    IsEssential = true,
                    Expires = DateTime.Now.AddDays(1)
                };
                Response.Cookies.Append("AuthToken", token, cookieOptions);
                Response.Cookies.Append("UserPic", userPic);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("SignIn", "Account");
            }
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("AuthToken");
            Response.Cookies.Delete("UserPic");
            await HttpContext.SignOutAsync();
            return RedirectToAction("SignIn");
        }
    }
}
