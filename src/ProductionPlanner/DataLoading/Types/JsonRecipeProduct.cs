using Newtonsoft.Json;

namespace ProductionPlanner.DataLoading;

public class JsonRecipeProduct
{
    [JsonProperty("item")]
    public string Item { get; init; }

    [JsonProperty("amount")]
    public double Amount { get; init; }
}