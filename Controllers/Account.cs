using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CBApp.Controllers
{
    public class Account : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
