using Microsoft.AspNetCore.Mvc;
using SupperCRMApplication.DataAccess;
using SupperCRMApplication.Models;
using SupperCRMApplication.Services;
using SupperCRMApplication.WebApp.Models;
using System.Diagnostics;

namespace SupperCRMApplication.WebApp.Controllers
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
        //From service attribureu ile metod seviyesinde inject ettik
        public IActionResult Dashboard([FromServices] IDashboardService dashboardService)
        {
            DashboardModel model =dashboardService.GetDashboardModel();
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        //Home/GenFakeData
        public string GenFakeData([FromServices] IMockService mockService)
        {
            mockService.RunFakeGenerator();
            return "ok";
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}