using Newtonsoft.Json;

namespace ProductionPlanner.DataLoading
{
    public class JsonResource
    {
        [JsonProperty("item")]
        public string Item { get; init; }

        [JsonProperty("pingColor")]
        public JsonColor PingColor { get; init; }

        [JsonProperty("speed")]
        public double Speed { get; init; }
    }
}