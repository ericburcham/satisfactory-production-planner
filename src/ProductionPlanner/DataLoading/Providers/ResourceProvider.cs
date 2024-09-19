using Newtonsoft.Json;

namespace ProductionPlanner.DataLoading
{
    public class ResourceProvider
    {
        /// <summary>
        /// Holds the path to the json data file.
        /// </summary>
        private readonly string _jsonDataPath;
        
        /// <summary>
        /// Holds the value factory for loading resources, and exposes the resources.
        /// </summary>
        private readonly Lazy<Task<Dictionary<string, JsonResource>>> _resourcesAsync;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceProvider"/> class.
        /// </summary>
        /// <param name="jsonDataPath">The path to the json data file.</param>
        /// <exception cref="ArgumentNullException"><paramref name="jsonDataPath"/> is null.</exception>
        public ResourceProvider(string jsonDataPath)
        {
            _jsonDataPath = jsonDataPath ?? throw new ArgumentNullException(nameof(jsonDataPath));
            _resourcesAsync = new Lazy<Task<Dictionary<string, JsonResource>>>(LoadResourcesAsync);
        }

        /// <summary>
        /// Provides the value factory for the lazy-loaded <see cref="_jsonDataPath"/> field.
        /// </summary>
        /// <returns>A task that represents the asynchronous data loading operation, which loads resource data from the json data file.</returns>
        /// <exception cref="FileNotFoundException">The json data file was not found.</exception>
        /// <exception cref="InvalidDataException">The json data file was invalid.</exception>
        /// <exception cref="InvalidOperationException">One of the resources in the json data file had a null body.</exception>
        private async Task<Dictionary<string, JsonResource>> LoadResourcesAsync()
        {
            if (!File.Exists(_jsonDataPath))
            {
                throw new FileNotFoundException($"The file '{_jsonDataPath}' was not found.");
            }

            var jsonData = await File.ReadAllTextAsync(_jsonDataPath);
            var root = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, object>>>(jsonData);

            if (root == null || !root.TryGetValue("resources", out var resourceProperties))
            {
                throw new InvalidDataException("Invalid JSON structure: 'resources' property not found.");
            }

            var resources = new Dictionary<string, JsonResource>();
            foreach (var resourceProperty in resourceProperties)
            {
                var key = resourceProperty.Key;
                var value = JsonConvert.DeserializeObject<JsonResource>(resourceProperty.Value.ToString() ?? throw new InvalidOperationException($"The {resourceProperty.Key} property was null."));
                resources.Add(key, value);
            }

            return resources;
        }

        /// <summary>
        /// Gets the resources that were found in the json data file.
        /// </summary>
        public IReadOnlyDictionary<string, JsonResource> Resources => _resourcesAsync.Value.GetAwaiter().GetResult();
    }
}