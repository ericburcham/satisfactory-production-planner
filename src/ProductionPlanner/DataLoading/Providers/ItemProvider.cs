using Newtonsoft.Json;

namespace ProductionPlanner.DataLoading
{
    public class ItemProvider
    {
        /// <summary>
        /// Holds the path to the json data file.
        /// </summary>
        private readonly string _jsonDataPath;
        
        /// <summary>
        /// Holds the value factory for loading items, and exposes the items.
        /// </summary>
        private readonly Lazy<Task<Dictionary<string, JsonItem>>> _itemsAsync;

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemProvider"/> class.
        /// </summary>
        /// <param name="jsonDataPath">The path to the json data file.</param>
        /// <exception cref="ArgumentNullException"><paramref name="jsonDataPath"/> is null.</exception>
        public ItemProvider(string jsonDataPath)
        {
            _jsonDataPath = jsonDataPath ?? throw new ArgumentNullException(nameof(jsonDataPath));
            _itemsAsync = new Lazy<Task<Dictionary<string, JsonItem>>>(LoadItemsAsync);
        }

        /// <summary>
        /// Provides the value factory for the lazy-loaded <see cref="_jsonDataPath"/> field.
        /// </summary>
        /// <returns>A task that represents the asynchronous data loading operation, which loads item data from the json data file.</returns>
        /// <exception cref="FileNotFoundException">The json data file was not found.</exception>
        /// <exception cref="InvalidDataException">The json data file was invalid.</exception>
        /// <exception cref="InvalidOperationException">One of the items in the json data file had a null body.</exception>
        private async Task<Dictionary<string, JsonItem>> LoadItemsAsync()
        {
            if (!File.Exists(_jsonDataPath))
            {
                throw new FileNotFoundException($"The file '{_jsonDataPath}' was not found.");
            }

            var jsonData = await File.ReadAllTextAsync(_jsonDataPath);
            var root = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, object>>>(jsonData);

            if (root == null || !root.TryGetValue("items", out var itemProperties))
            {
                throw new InvalidDataException("Invalid JSON structure: 'items' property not found.");
            }

            var items = new Dictionary<string, JsonItem>();
            foreach (var itemProperty in itemProperties)
            {
                var key = itemProperty.Key;
                var value = JsonConvert.DeserializeObject<JsonItem>(itemProperty.Value.ToString() ?? throw new InvalidOperationException($"The {itemProperty.Key} property was null."));
                items.Add(key, value);
            }

            return items;
        }

        /// <summary>
        /// Gets the items that were found in the json data file.
        /// </summary>
        public IReadOnlyDictionary<string, JsonItem> Items => _itemsAsync.Value.GetAwaiter().GetResult();
    }
}