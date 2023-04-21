using Microsoft.AspNetCore.Mvc;

namespace risk.web.MVC.Controllers
{
public class ProductController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
}