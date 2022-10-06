using CommonRazorEngine;
using CommonRazorEngine.Views;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRazorViewToStringRenderer _razorViewToStringRenderer;

        public HomeController(ILogger<HomeController> logger, IRazorViewToStringRenderer razorViewToStringRenderer)
        {
            _logger = logger;
            _razorViewToStringRenderer = razorViewToStringRenderer;
        }

        public async Task<IActionResult> Index()
        {

            // test razor view to string
            string content = await _razorViewToStringRenderer.RenderToStringAsync("~/Views/MyView.cshtml", new MyView());

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
}