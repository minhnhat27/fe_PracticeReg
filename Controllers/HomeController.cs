using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyWebAPI.Data.ViewModels.Schedule;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace DangKyPhongThucHanhCNTTApi.Controllers
{
    public class HomeController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:44304/api");
        //Uri baseAddress = new Uri("http://localhost:8000/api");
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
            var user = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["AuthToken"]);
            var response = await _client.PostAsJsonAsync(baseAddress + "/Schedule/GetTeachingofLecturer", user);
            
            var data = response.Content.ReadAsStringAsync().Result;
            var list = new LichGiangDayVM();
            try
            {
                 list = JsonConvert.DeserializeObject<LichGiangDayVM>(data);
            }
            catch(Exception)
            {
                ModelState.AddModelError("Error", "Có lỗi xảy ra. Không thể lấy thông tin!");
                return View("Index");
            }

            if (list == null)
            {
                ModelState.AddModelError(string.Empty, "Có lỗi xảy ra. Không thể xác thực người dùng!");
                return View("Index");
            }

            if (list.giangDays.IsNullOrEmpty())
            {
                ModelState.AddModelError("Error", "Giảng viên chưa nhập học phần giảng dạy!");
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

        public async Task<IActionResult> ViewSchedule()
        {
            var response = await _client.GetAsync(baseAddress + "/Schedule/getPraticeSchedule");
            var data = response.Content.ReadAsStringAsync().Result;
            try
            {
                var list = JsonConvert.DeserializeObject<ViewSchedule>(data);
                if (list == null)
                {
                    ModelState.AddModelError(string.Empty, "Có lỗi xảy ra!");
                    return RedirectToAction("Index");
                }
                return View(list);
            }
            catch (Exception)
            {
                ModelState.AddModelError("Error", "Có lỗi xảy ra. Không thể lấy thông tin!");
                return View("Index");
            }
        }
        public IActionResult TeachingCourse()
        {
            return View();
        }
    }
}