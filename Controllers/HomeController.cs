using Microsoft.AspNetCore.Mvc;

namespace Fluent.Api.Calculator.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return Json("Hello world!");
        }
    }
}