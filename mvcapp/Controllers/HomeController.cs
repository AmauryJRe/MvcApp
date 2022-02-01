using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using mvcapp.Models;

namespace mvcapp.Controllers;

public class HomeController : Controller
{
    // private readonly ILogger<HomeController> _logger;

    // public HomeController(ILogger<HomeController> logger)
    // {
    //     _logger = logger;
    // }

    // public HomeController(){}
    

    public IActionResult Index()
    {
        return View("Index");
    }

    public IActionResult Privacy()
    {
        return View("Privacy");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public string GetEmployeeName(int empId)
    {
        string name;
        if (empId == 1)
        {
            name = "Jignesh";
        }
        else if (empId == 2)
        {
            name = "Rakesh";
        }
        else
        {
            name = "Not Found";
        }
        return name;
    }
}
