using LWSCSecondProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LWSCSecondProject.Controllers
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


        public IActionResult TestSignalR()
        {
            return View();
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


        public PartialViewResult GetPartial()
        {
            return PartialView("_InfoAlert", "Data from  server!");
        }
    }
}