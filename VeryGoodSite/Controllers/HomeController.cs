using System.Diagnostics;
using System.Text;
using GoodSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using VeryGoodSite.Models;

namespace VeryGoodSite.Controllers;

[Route("Home")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
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

    [HttpPost]
    [Route("Privacy")]
    public IActionResult Privacy([FromForm] string AntiforgeryFieldname)
    {
        var cookie = Request.Cookies["token"];
        if (string.IsNullOrEmpty(cookie) || string.IsNullOrEmpty(AntiforgeryFieldname))
        {
            return NotFound();
        }

        var text = "Секретный ключ ваш!";
        return Ok(text);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}