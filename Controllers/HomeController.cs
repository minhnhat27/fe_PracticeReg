using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWebAPI.Data.ViewModels;
using Newtonsoft.Json;

namespace DangKyPhongThucHanhCNTTApi.Controllers
{
    public class HomeController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:44304/api");
        private readonly HttpClient _client;
        public HomeController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        public IActionResult Index()
        {
            List<HocPhan> list = new List<HocPhan>();
            HttpResponseMessage response = _client.GetAsync(baseAddress + "/User/GetAllCourse").Result;
            var data = response.Content.ReadAsStringAsync().Result;

            list = JsonConvert.DeserializeObject<List<HocPhan>>(data)!;
            return View(list);
        }

        [Authorize]
        public IActionResult Schedule()
        {
            return View();
        }

        //[Route("signin-google")]
        //public IActionResult SignInGoogle()
        //{
        //    var properties = new AuthenticationProperties { RedirectUri = "/callback-google" };
        //    return Challenge(properties, "Google");
        //}

        //[Route("callback-google")]
        //public async Task<IActionResult> CallbackGoogle()
        //{
        //    var authenticateResult = await HttpContext.AuthenticateAsync("Google");

        //    if (!authenticateResult.Succeeded)
        //    {
        //        // Xử lý lỗi xác thực
        //        return RedirectToAction("Login");
        //    }

        //    // Lấy thông tin người dùng từ authenticateResult.Principal

        //    // Xử lý đăng nhập thành công
        //    return RedirectToAction("Index");
        //}

    }
}