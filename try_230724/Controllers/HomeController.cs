using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection;
using try_230724.Models;

namespace try_230724.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Link()
        {
            return View();
        }



        [HttpPost]
        public IActionResult ShowData(YourViewModel model)
        {
            // 在這裡你可以對表單數據進行處理
            // 假設你要傳遞 Name 和 Age 屬性的值到下一個頁面

            TempData["Name"] = model.Name;
            TempData["Age"] = model.Age;

            return RedirectToAction("Display");
        }

        public IActionResult Display()
        {
            // 從 TempData 中讀取暫存的數據並將其傳遞給顯示數據的頁面
            string name = TempData["Name"] as string;
            int age = (int)TempData["Age"];

            // 建立 ViewModel 並傳遞給 View
            YourViewModel model = new YourViewModel { Name = name, Age = age };
            return View(model);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}