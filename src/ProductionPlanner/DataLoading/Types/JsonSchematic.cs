using Newtonsoft.Json;

namespace ProductionPlanner.DataLoading
{
    public class JsonSchematic
    {
        [JsonProperty("className")]
        public string ClassName { get; init; }

        [JsonProperty("type")]
        public string Type { get; init; }

        [JsonProperty("name")]
        public string Name { get; init; }

        [JsonProperty("slug")]
        public string Slug { get; init; }

        [JsonProperty("cost")]
        public List<JsonSchematicCost> Cost { get; init; }

        [JsonProperty("unlock")]
        public JsonSchematicUnlock Unlock { get; init; }

        [JsonProperty("requiredSchematics")]
        public List<string> RequiredSchematics { get; init; }

        [JsonProperty("tier")]
        public int Tier { get; init; }

        [JsonProperty("time")]
        public double Time { get; init; }

        [JsonProperty("mam")]
        public bool Mam { get; init; }

        [JsonProperty("alternate")]
        public bool Alternate { get; init; }
    }
}