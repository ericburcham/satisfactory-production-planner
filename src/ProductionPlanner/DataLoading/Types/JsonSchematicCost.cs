﻿using Newtonsoft.Json;

namespace ProductionPlanner.DataLoading;

public class JsonSchematicCost
{
    [JsonProperty("item")]
    public string Item { get; init; }

    [JsonProperty("amount")]
    public double Amount { get; init; }
}