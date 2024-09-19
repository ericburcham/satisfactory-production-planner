using Newtonsoft.Json;

namespace ProductionPlanner.DataLoading
{
    public class JsonMiner
    {
        [JsonProperty("className")]
        public string ClassName { get; init; }

        [JsonProperty("allowedResources")]
        public List<string> AllowedResources { get; init; }

        [JsonProperty("allowLiquids")]
        public bool AllowLiquids { get; init; }

        [JsonProperty("allowSolids")]
        public bool AllowSolids { get; init; }

        [JsonProperty("itemsPerCycle")]
        public int ItemsPerCycle { get; init; }

        [JsonProperty("extractCycleTime")]
        public double ExtractCycleTime { get; init; }
    }
}