using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using CBApp.Models;
using CBApp.Data;
using System.Text.RegularExpressions;
using System.Net;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using System.Security.Claims;

namespace CBApp.Controllers
{
    public class MatchesController : Controller
    {

        /**
        * Provides the Database Context object passed in through constructor’s parameter,
        * this will provide the controller with the Database Context object through a
        * Dependency Injection.
     */
        private ApplicationDbContext? context;
        public MatchesController(ApplicationDbContext _context, UserManager<User> _userManager,
            SignInManager<User> _signInManager, IWebHostEnvironment _environment)
        {   
            // Set up database context and Identity User inbuilt functionality for the controller
            context = _context;
            userManager = _userManager;
            signInManager = _signInManager;
            Environment = _environment;
        }

        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private IWebHostEnvironment Environment;

        public IActionResult Index()
        {
            return View();
        }
    }
}
