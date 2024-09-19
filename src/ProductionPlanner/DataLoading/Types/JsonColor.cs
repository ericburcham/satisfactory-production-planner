using Newtonsoft.Json;

namespace ProductionPlanner.DataLoading;

public class JsonColor
{
    [JsonProperty("r")]
    public int R { get; init; }

    [JsonProperty("g")]
    public int G { get; init; }

    [JsonProperty("b")]
    public int B { get; init; }

    [JsonProperty("a")]
    public int A { get; init; }
}