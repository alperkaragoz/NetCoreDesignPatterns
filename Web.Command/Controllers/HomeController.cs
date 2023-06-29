using BaseProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web.Command.Commands;
using Web.Command.Models;

namespace BaseProject.Controllers
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


            //Amacımız dinamik bir yapı olduğu için aşağıdaki kodu kullanmayacağız.
            //ExcelFile<Product> excelFile = new ExcelFile<Product>(new List<Product>());

            //excelFile.Create();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}