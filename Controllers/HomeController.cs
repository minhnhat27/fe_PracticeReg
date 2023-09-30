using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyWebAPI.Data.ViewModels.Schedule;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Runtime.Serialization;

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
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Schedule()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, baseAddress + "/Schedule/GetTeachingofLecturer");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["AuthToken"]);
            
            var response = await _client.SendAsync(request);
            var data = response.Content.ReadAsStringAsync().Result;
            var list = JsonConvert.DeserializeObject<LichGiangDayVM>(data);
            
            if(list == null)
            {
                ModelState.AddModelError(string.Empty, "Có lỗi xảy ra. Không thể xác thực người dùng!");
                return View("Index");
            }

            if (list.giangDays.IsNullOrEmpty())
            {
                ModelState.AddModelError(string.Empty, "Giảng viên chưa nhập học phần giảng dạy!");
            }
            return View(list);
        }

        [HttpPost]
        public async Task<bool> CreateSchedule(LichThucHanhVM lichThucHanh)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["AuthToken"]);
            var response = await _client.PostAsJsonAsync(baseAddress + "/Schedule/saveSchedule", lichThucHanh);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpPost]
        public async Task<bool> updateOnSchedule(LichThucHanhVM lichThucHanh)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["AuthToken"]);
            var response = await _client.PutAsJsonAsync(baseAddress + "/Schedule/updateOnSchedule", lichThucHanh);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
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