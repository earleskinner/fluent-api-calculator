using Fluent.Core.Api;
using Newtonsoft.Json;

namespace Fluent.Api.Calculator.Models
{
    public class CalculationModelResponse : BaseApiResponse
    {
        [JsonProperty("result")]
        public int Result { get; set; }
    }
}