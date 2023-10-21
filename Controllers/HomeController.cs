using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyWebAPI.Data.ViewModels.Schedule;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;
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
            var message = TempData["ErrorMessage"] as string;
            if (!message.IsNullOrEmpty())
            {
                ViewBag.Message = message;
            }
            try
            {
                var list = JsonConvert.DeserializeObject<ViewSchedule>(data);
                if (list == null)
                {
                    TempData["ErrorMessage"] = "Có lỗi xảy ra!";
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

        public async Task<IActionResult> downloadExcel()
        {
            var response = await _client.GetAsync(baseAddress + "/Schedule/getPraticeSchedule");
            var data = response.Content.ReadAsStringAsync().Result;
            try
            {
                var list = JsonConvert.DeserializeObject<ViewSchedule>(data)!;
                if (list == null)
                {
                    TempData["ErrorMessage"] = "Có lỗi xảy ra!";
                    return RedirectToAction("Index");
                }
                var stream = new MemoryStream();
                using (var excel = new ExcelPackage(stream))
                {
                    var toDay = list.ngaybatdauhk;
                    for(int i = 1; i <= list.sotuan; i++)
                    {
                        var wday = toDay;
                        var workSheet = excel.Workbook.Worksheets.Add("Tuần " + i);
                        workSheet.DefaultRowHeight = 15;
                        workSheet.DefaultColWidth = 20;
                        workSheet.Column(1).Width = 10;
                        workSheet.Column(2).Width = 10;
                        workSheet.Row(1).Height = 20;
                        workSheet.Cells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        workSheet.Cells.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        workSheet.Cells.Style.WrapText = true;

                        workSheet.Cells["A1:I1"].Merge = true;
                        workSheet.Cells["A1"].Value = "Lịch Thực Hành Trường CNTT & TT";
                        workSheet.Cells["A1"].Style.Font.Size = 14;
                        workSheet.Cells["A1"].Style.Font.Bold = true;
                        workSheet.Cells["A1"].Style.Font.Color.SetColor(ExcelIndexedColor.Indexed17);

                        workSheet.Cells["A2:A3"].Merge = true;
                        workSheet.Cells["A2"].Value = "Buổi";
                        workSheet.Cells["B2:B3"].Merge = true;
                        workSheet.Cells["B2"].Value = "Phòng";
                        
                        workSheet.Cells["C2"].Value = "Thứ 2";
                        workSheet.Cells["D2"].Value = "Thứ 3";
                        workSheet.Cells["E2"].Value = "Thứ 4";
                        workSheet.Cells["F2"].Value = "Thứ 5";
                        workSheet.Cells["G2"].Value = "Thứ 6";
                        workSheet.Cells["H2"].Value = "Thứ 7";
                        workSheet.Cells["I2"].Value = "Chủ nhật";

                        workSheet.Cells["A4:A" + (list.phong.Count + 3)].Merge = true;
                        workSheet.Cells["A4:A" + (list.phong.Count + 3)].Value = "Sáng";
                        workSheet.Cells["A" + (list.phong.Count + 4) + ":A" + (list.phong.Count * 2 + 3)].Merge = true;
                        workSheet.Cells["A" + (list.phong.Count + 4) + ":A" + (list.phong.Count * 2 + 3)].Value = "Chiều";
                        
                        for (int j = 1; j <= 7; j++)
                        {
                            workSheet.Cells[3, j + 2].Value = wday.ToString("dd/MM/yyyy");
                            wday = wday.AddDays(1);
                        }

                        int row = 4;
                        foreach (var item in list.phong)
                        {
                            workSheet.Cells["B" + row].Value = item;
                            wday = toDay;
                            for (int j = 1; j <= 7; j++)
                            {                                
                                foreach (var lich in list.lichThucHanhs)
                                {
                                    if (lich.phong == item && lich.ngaythuchanh.Equals(wday) && lich.buoi == "Morning")
                                    {
                                        workSheet.Cells[row, j + 2].Value = lich.manhomhp + Environment.NewLine + lich.tenhp + Environment.NewLine + lich.hoten;
                                        workSheet.Row(row).Height = 60;
                                    }
                                }
                                wday = wday.AddDays(1);
                            }
                            row++;
                        }
                        foreach (var item in list.phong)
                        {
                            workSheet.Cells["B" + row].Value = item;
                            wday = toDay;
                            for (int j = 1; j <= 7; j++)
                            {
                                foreach (var lich in list.lichThucHanhs)
                                {
                                    if (lich.phong == item && lich.ngaythuchanh.Equals(wday) && lich.buoi == "Afternoon")
                                    {
                                        workSheet.Cells[row, j + 2].Value = lich.manhomhp + Environment.NewLine + lich.tenhp + Environment.NewLine + lich.hoten;
                                    }
                                }
                                wday = wday.AddDays(1);
                            }
                            row++;
                        }
                        toDay = wday;
                    }
                    excel.Save();
                    stream.Position = 0;
                    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DanhSachDuLieu.xlsx");
                }
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra. Không thể tải File!";
                return RedirectToAction("ViewSchedule");
            }
        }
        public IActionResult TeachingCourse()
        {
            return View();
        }
    }
}