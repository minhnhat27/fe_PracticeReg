using DangKyPhongThucHanhCNTT.Data;
using DangKyPhongThucHanhCNTT.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DangKyPhongThucHanhCNTT.Controllers
{
    public class HomeController : Controller
    {
        private readonly MyDbContext db;
        public HomeController(MyDbContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult ChangeCourse(string id)
        //{
        //    var data = db.PracticeSessions.Where(e => e.CourseId == id && e.onSchedule == false).Select(e => e);
        //    return PartialView(data);
        //}
    }
}