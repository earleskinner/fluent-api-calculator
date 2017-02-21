using System;
using System.Linq;
using Fluent.Api.Calculator.Models;
using Fluent.Core.Api;
using Fluent.Core.Services.Context;
using Microsoft.AspNetCore.Mvc;

namespace Fluent.Api.Calculator.Controllers
{
    [Route("api/calculator/v1/[action]")]
    public class CalculationControllerV1 : BaseApiController
    {
        public CalculationControllerV1(IFluentContext fluent, CommonResponses common) : base(fluent, common)
        {

        }

        [HttpPost]
        [ActionName("add")]
        public ActionResult Add([FromBody] CalculationModelRequest model)
        {
            return Json(new CalculationModelResponse
            {
                CorrelationId = Fluent.CorrelationId,
                Result = model.Values.Aggregate((current, next) => current + next)
            });
        }

        [HttpPost]
        [ActionName("minus")]
        public ActionResult Minus([FromBody] CalculationModelRequest model)
        {
            return Json(new CalculationModelResponse
            {
                CorrelationId = Fluent.CorrelationId,
                Result = model.Values.Aggregate((current, next) => current - next)
            });
        }

        [HttpPost]
        [ActionName("times")]
        public ActionResult Times([FromBody] CalculationModelRequest model)
        {
            return Json(new CalculationModelResponse
            {
                CorrelationId = Fluent.CorrelationId,
                Result = model.Values.Aggregate((current, next) => current * next)
            });
        }

        [HttpPost]
        [ActionName("divide")]
        public ActionResult Divide([FromBody] CalculationModelRequest model)
        {
            try
            {
                return Json(new CalculationModelResponse
                {
                    CorrelationId = Fluent.CorrelationId,
                    Result = model.Values.Aggregate((current, next) => current / next)
                });
            }
            catch (DivideByZeroException)
            {
                return new BadRequestObjectResult(
                    CommonResponses.DivideByZero.ApplyParameter("values"));
            }
        }
    }
}