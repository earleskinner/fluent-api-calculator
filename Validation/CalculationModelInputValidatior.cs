using System;
using System.Collections.Generic;
using Fluent.Api.Calculator.Models;
using Fluent.Core.Api;
using Fluent.Core.Services.Context;
using Fluent.Core.Validation;
using Microsoft.AspNetCore.Mvc.Localization;

namespace Fluent.Api.Calculator.Validation
{
    public class CalculationModelInputValidatior
    {
        private readonly IFluentContext _context;
        private readonly IHtmlLocalizer<Program> _localizer;

        public CalculationModelInputValidatior(IFluentContext context, IHtmlLocalizer<Program> localizer)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            if (localizer == null)
            {
                throw new ArgumentNullException(nameof(localizer));
            }

            _context = context;
            _localizer = localizer;
        }

        public bool IsValid(CalculationModelRequest request, out CalculationModelResponse response)
        {
            response = new CalculationModelResponse
            {
                CorrelationId = _context.CorrelationId,
                Errors = new List<ValidationMessage>()
            };

            if (request == null)
            {
                response.Errors = new List<ValidationMessage>
                {
                    new ValidationMessage
                    {
                        Type = ValidationType.Input,
                        Message = _localizer["calculation-model:no-request"].Value,
                        Code = "no_request"
                    }
                };
                return false;
            }

            if (request.Values == null ||
                request.Values.Count < 2)
            {
                response.Errors = new List<ValidationMessage>
                {
                    new ValidationMessage
                    {
                        Type = ValidationType.Input,
                        Message = _localizer["calculation-model:must-contain-more-than-one"].Value,
                        Code = "more_than_one",
                        Parameter = "values"
                    }
                };
                return false;
            }

            return true;
        }
    }
}