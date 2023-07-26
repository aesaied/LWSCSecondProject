using Microsoft.AspNetCore.Mvc;

namespace LWSCSecondProject.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
