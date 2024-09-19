using Newtonsoft.Json;

namespace ProductionPlanner.DataLoading
{
    public class JsonItem
    {
        [JsonProperty("slug")]
        public string Slug { get; init; }

        [JsonProperty("name")]
        public string Name { get; init; }

        [JsonProperty("description")]
        public string Description { get; init; }

        [JsonProperty("sinkPoints")]
        public int SinkPoints { get; init; }

        [JsonProperty("className")]
        public string ClassName { get; init; }

        [JsonProperty("stackSize")]
        public int StackSize { get; init; }

        [JsonProperty("energyValue")]
        public double EnergyValue { get; init; }

        [JsonProperty("radioactiveDecay")]
        public double RadioactiveDecay { get; init; }

        [JsonProperty("liquid")]
        public bool IsLiquid { get; init; }

        [JsonProperty("fluidColor")]
        public JsonColor JsonColor { get; init; }
    }
}