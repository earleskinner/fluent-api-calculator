using System.Linq;
using Fluent.Api.Calculator.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fluent.Api.Calculator.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/calculator/v{version:apiVersion}/[action]")]
    public class CalculationControllerV1 : Controller
    {
        [HttpPost]
        [ActionName("add")]
        public ActionResult Add([FromBody] CalculationModel model)
        {
            return Json(model.Values.Aggregate((current, next) => current + next));
        }

        [HttpPost]
        [ActionName("minus")]
        public ActionResult Minus([FromBody] CalculationModel model)
        {
            return Json(model.Values.Aggregate((current, next) => current - next));
        }

        [HttpPost]
        [ActionName("times")]
        public ActionResult Times([FromBody] CalculationModel model)
        {
            return Json(model.Values.Aggregate((current, next) => current * next));
        }

        [HttpPost]
        [ActionName("divide")]
        public ActionResult Divide([FromBody] CalculationModel model)
        {
            return Json(model.Values.Aggregate((current, next) => current / next));
        }
    }
}