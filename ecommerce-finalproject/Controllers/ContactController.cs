using Microsoft.AspNetCore.Mvc;

namespace ecommerce_finalproject.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
