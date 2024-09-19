using Newtonsoft.Json;

namespace ProductionPlanner.DataLoading
{
    public class JsonGenerator
    {
        [JsonProperty("className")]
        public string ClassName { get; init; }

        [JsonProperty("fuel")]
        public List<string> Fuel { get; init; }

        [JsonProperty("powerProduction")]
        public double PowerProduction { get; init; }

        [JsonProperty("powerProductionExponent")]
        public double PowerProductionExponent { get; init; }

        [JsonProperty("waterToPowerRatio")]
        public double WaterToPowerRatio { get; init; }
    }
}