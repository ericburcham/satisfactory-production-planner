using Newtonsoft.Json;

namespace ProductionPlanner.DataLoading;

public class JsonSchematicUnlock
{
    [JsonProperty("recipes")]
    public List<string> Recipes { get; init; }

    [JsonProperty("scannerResources")]
    public List<string> ScannerResources { get; init; }

    [JsonProperty("inventorySlots")]
    public int InventorySlots { get; init; }

    [JsonProperty("giveItems")]
    public List<JsonSchematicGiveItem> GiveItems { get; init; }
}