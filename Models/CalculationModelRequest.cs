using System.Collections.Generic;
using Fluent.Core.Api;
using Newtonsoft.Json;

namespace Fluent.Api.Calculator.Models
{
    public class CalculationModelRequest : BaseApiRequest
    {
        [JsonProperty("values")]
        public List<int> Values { get; set; }
    }
}