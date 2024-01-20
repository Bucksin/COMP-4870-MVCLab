using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVClab.Models;

namespace MVClab.Controllers;

public class FileController : Controller
{
    private readonly IWebHostEnvironment _env;

    private readonly ILogger<FileController> _logger;

    public FileController(ILogger<FileController> logger, IWebHostEnvironment env)
    {
        _logger = logger;
        _env = env;
    }

    public IActionResult Index()
    {
        string path = _env.ContentRootPath + "/TextFiles";
        string[] files = Directory.GetFiles(path).Select(p => Path.GetFileName(p)).ToArray();
        ViewBag.Files = files;

        return View();
    }

    #pragma warning disable CS0114 // Member hides inherited member; missing override keyword
    public IActionResult Content(string id)
    #pragma warning restore CS0114 // Member hides inherited member; missing override keyword
    {
        string path = _env.ContentRootPath + "/TextFiles/" + id;
        string[] lines = System.IO.File.ReadAllLines(path);

        return View(lines);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
