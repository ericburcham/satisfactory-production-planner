using Newtonsoft.Json;

namespace ProductionPlanner.DataLoading
{
    public class JsonRecipe
    {
        [JsonProperty("slug")]
        public string Slug { get; init; }

        [JsonProperty("name")]
        public string Name { get; init; }

        [JsonProperty("className")]
        public string ClassName { get; init; }

        [JsonProperty("alternate")]
        public bool Alternate { get; init; }

        [JsonProperty("time")]
        public double Time { get; init; }

        [JsonProperty("inHand")]
        public bool InHand { get; init; }

        [JsonProperty("forBuilding")]
        public bool ForBuilding { get; init; }

        [JsonProperty("inWorkshop")]
        public bool InWorkshop { get; init; }

        [JsonProperty("inMachine")]
        public bool InMachine { get; init; }

        [JsonProperty("manualTimeMultiplier")]
        public double ManualTimeMultiplier { get; init; }

        [JsonProperty("ingredients")]
        public List<JsonRecipeIngredient> Ingredients { get; init; }

        [JsonProperty("products")]
        public List<JsonRecipeProduct> Products { get; init; }

        [JsonProperty("producedIn")]
        public List<string> ProducedIn { get; init; }

        [JsonProperty("isVariablePower")]
        public bool IsVariablePower { get; init; }

        [JsonProperty("minPower")]
        public double MinPower { get; init; }

        [JsonProperty("maxPower")]
        public double MaxPower { get; init; }
    }
}