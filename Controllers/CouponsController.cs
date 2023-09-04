using Microsoft.AspNetCore.Mvc;

namespace BigCatCookinAPI.Controllers;

public class CouponsController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
