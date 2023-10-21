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

namespace DangKyPhongThucHanhCNTTApi.Controllers
{
    public class AccountController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:44304/api");
        //Uri baseAddress = new Uri("http://localhost:8000/api");
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
                ViewBag.MessagePassword = message;
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
                IsPersistent = true
            };
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userClaims, authProperties);
            var cookieOptions = new CookieOptions
            {
                Secure = true,
                Path = "/",
                IsEssential = true,
                HttpOnly = true,
                Expires = DateTime.Now.AddDays(1),
                SameSite = SameSiteMode.Lax
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
                IsPersistent = true
            });
        }

        public async Task<IActionResult> CallBackGoogle()
        {
            var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
            if (result.Succeeded)
            {
                var userEmail = result.Principal.FindFirstValue(ClaimTypes.Email);
                var userPic = result.Principal.FindFirstValue("urn:google:picture")!;

                try
                {
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
                        Path = "/",
                        HttpOnly = true,
                        IsEssential = true,
                        Expires = DateTime.Now.AddDays(1),
                        SameSite = SameSiteMode.Lax
                    };
                    Response.Cookies.Append("AuthToken", token, cookieOptions);
                    Response.Cookies.Append("UserPic", userPic, new CookieOptions{ Expires= DateTime.Now.AddDays(1)});

                    return RedirectToAction("Index", "Home");
                }
                catch
                {
                    TempData["ErrorMessage"] = "Có lỗi xảy ra. Vui lòng thử lại sau!";
                    await HttpContext.SignOutAsync();
                    return RedirectToAction("SignIn");
                }
            }
            else
            {
                return RedirectToAction("SignIn");
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

        public async Task<bool> getToken(string id)
        {
            var response = await _client.PostAsJsonAsync(baseAddress + "/User/sendToken", id);
            var content = response.Content.ReadAsStringAsync().Result;
            var data = JsonConvert.DeserializeObject<ApiResponse>(content)!;
            if (data.success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordModel forgetPassword)
        {
            if(forgetPassword.token == null)
            {
                return View();
            }
            var response = await _client.PostAsJsonAsync(baseAddress + "/User/checkToken", forgetPassword);
            var content = response.Content.ReadAsStringAsync().Result;
            var data = JsonConvert.DeserializeObject<ApiResponse>(content)!;
            if (data.success)
            {
                TempData["ResetToken"] = JsonConvert.SerializeObject(forgetPassword);
                TempData["Session"] = forgetPassword.id;
                return RedirectToAction("ConfirmPassword");
            }
            else
            {
                ModelState.AddModelError("ErrorToken", "Mã xác nhận không chính xác.");
                return View();
            }
        }

        [HttpGet]
        public IActionResult ConfirmPassword()
        {
            var model = TempData["Session"] as string;
            if (model == null)
            {
                return NotFound();
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> ConfirmPassword(ConfirmPasswordModel confirmPassword)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!confirmPassword.pass.Equals(confirmPassword.newPassword))
            {
                ModelState.AddModelError("ErrorConfirmPassword", "Mật khẩu xác nhận không chính xác");
                return View();
            }
            try
            {
                var tempData = TempData["ResetToken"]!.ToString();
                var model = JsonConvert.DeserializeObject<ForgetPasswordModel>(tempData!);
                model!.newPassword = confirmPassword.newPassword;
                var response = await _client.PostAsJsonAsync(baseAddress + "/User/changePassword", model);
                var content = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<ApiResponse>(content)!;
                if (data.success)
                {
                    TempData["ErrorMessage"] = "Đổi mật khẩu thành công. Vui lòng đăng nhập lại";
                    return RedirectToAction("SignIn");
                }
                else
                {
                    TempData["ErrorMessage"] = "Mật khẩu không đúng định dạng";
                    return View();
                }
            }
            catch(Exception)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra. Vui lòng thử lại";
                return RedirectToAction("SignIn");
            }
        }
    }
}
