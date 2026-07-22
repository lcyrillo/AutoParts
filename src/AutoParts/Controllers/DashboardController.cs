using Microsoft.AspNetCore.Mvc;

namespace AutoParts.Controllers;

public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
