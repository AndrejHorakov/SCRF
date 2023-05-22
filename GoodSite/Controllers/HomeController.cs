using System.Diagnostics;
using System.Text;
using GoodSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace GoodSite.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var cookies = Guid.NewGuid().ToString();

        var cookiesOptions = new CookieOptions
        {
            Expires = DateTime.Now.AddDays(7)
        };
        
        Response.Cookies.Append("token", cookies, cookiesOptions);
        
        return View();
    }

    public IActionResult Privacy()
    {
        var cookie = Request.Cookies["token"];
        if (string.IsNullOrEmpty(cookie))
        {
            return NotFound();
        }

        var text = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes("Секретный ключ ваш!"));
        return Json(text);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}