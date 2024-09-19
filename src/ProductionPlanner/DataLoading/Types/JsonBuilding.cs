using Newtonsoft.Json;

namespace ProductionPlanner.DataLoading
{
    public class JsonBuilding
    {
        [JsonProperty("slug")]
        public string Slug { get; init; }

        [JsonProperty("name")]
        public string Name { get; init; }

        [JsonProperty("className")]
        public string ClassName { get; init; }

        [JsonProperty("description")]
        public string Description { get; init; }

        [JsonProperty("powerConsumption")]
        public double PowerConsumption { get; init; }

        [JsonProperty("category")]
        public string Category { get; init; }

        [JsonProperty("buildCost")]
        public string BuildCost { get; init; }  // Assuming this could be a string representation of cost
    }
}