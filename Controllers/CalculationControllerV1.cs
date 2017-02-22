using System;
using System.Linq;
using Fluent.Api.Calculator.Models;
using Fluent.Api.Calculator.Validation;
using Fluent.Core.Api;
using Fluent.Core.Services.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;

namespace Fluent.Api.Calculator.Controllers
{
    [Route("api/calculator/v1/[action]")]
    public class CalculationControllerV1 : BaseApiController
    {
        private readonly IHtmlLocalizer<Program> _localizer;

        public CalculationControllerV1(IFluentContext fluent,
                                       CommonResponses common,
                                       IHtmlLocalizer<Program> localizer) : base(fluent, common)
        {
            if (localizer == null)
            {
                throw new ArgumentNullException(nameof(localizer));
            }
            _localizer = localizer;
        }

        [HttpPost]
        [ActionName("add")]
        public ActionResult Add([FromBody] CalculationModelRequest model)
        {
            var validator = new CalculationModelInputValidatior(Fluent, _localizer);
            CalculationModelResponse response;
            var result = validator.IsValid(model, out response);
            if (!result)
            {
                return BadRequest(response);
            }

            response.Result = model.Values.Aggregate((current, next) => current + next);

            return Json(response);
        }

        [HttpPost]
        [ActionName("minus")]
        public ActionResult Minus([FromBody] CalculationModelRequest model)
        {
            var validator = new CalculationModelInputValidatior(Fluent, _localizer);
            CalculationModelResponse response;
            var result = validator.IsValid(model, out response);
            if (!result)
            {
                return BadRequest(response);
            }

            response.Result = model.Values.Aggregate((current, next) => current - next);

            return Json(response);
        }

        [HttpPost]
        [ActionName("times")]
        public ActionResult Times([FromBody] CalculationModelRequest model)
        {
            var validator = new CalculationModelInputValidatior(Fluent, _localizer);
            CalculationModelResponse response;
            var result = validator.IsValid(model, out response);
            if (!result)
            {
                return BadRequest(response);
            }

            response.Result = model.Values.Aggregate((current, next) => current * next);

            return Json(response);
        }

        [HttpPost]
        [ActionName("divide")]
        public ActionResult Divide([FromBody] CalculationModelRequest model)
        {
            var validator = new CalculationModelInputValidatior(Fluent, _localizer);
            CalculationModelResponse response;
            var result = validator.IsValid(model, out response);
            if (!result)
            {
                return BadRequest(response);
            }

            try
            {
                response.Result = model.Values.Aggregate((current, next) => current / next);
            }
            catch (DivideByZeroException)
            {
                return BadRequest(CommonResponses.DivideByZero.ApplyParameter("values"));
            }

            return Json(response);
        }
    }
}