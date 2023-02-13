
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using System.Linq;
using CBApp.Data;
using CBApp.Models;

namespace CBApp.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // Access to the database
        private ApplicationDbContext _context { get; set; }

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context) // Need this to set up access to the database
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            //Random random = new Random();
            //int randomQuestionId = random.Next(1, 11);

            //var question = _context.Questions.Find(randomQuestionId).QuestionString;

            //return Content(question);

            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult FAQ()
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