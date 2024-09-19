using Newtonsoft.Json;

namespace ProductionPlanner.DataLoading
{
    public class BuildingProvider
    {
        /// <summary>
        /// Holds the path to the json data file.
        /// </summary>
        private readonly string _jsonDataPath;
        
        /// <summary>
        /// Holds the value factory for loading buildings, and exposes the buildings.
        /// </summary>
        private readonly Lazy<Task<Dictionary<string, JsonBuilding>>> _buildingsAsync;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildingProvider"/> class.
        /// </summary>
        /// <param name="jsonDataPath">The path to the json data file.</param>
        /// <exception cref="ArgumentNullException"><paramref name="jsonDataPath"/> is null.</exception>
        public BuildingProvider(string jsonDataPath)
        {
            _jsonDataPath = jsonDataPath ?? throw new ArgumentNullException(nameof(jsonDataPath));
            _buildingsAsync = new Lazy<Task<Dictionary<string, JsonBuilding>>>(LoadBuildingsAsync);
        }

        /// <summary>
        /// Provides the value factory for the lazy-loaded <see cref="_jsonDataPath"/> field.
        /// </summary>
        /// <returns>A task that represents the asynchronous data loading operation, which loads building data from the json data file.</returns>
        /// <exception cref="FileNotFoundException">The json data file was not found.</exception>
        /// <exception cref="InvalidDataException">The json data file was invalid.</exception>
        /// <exception cref="InvalidOperationException">One of the buildings in the json data file had a null body.</exception>
        private async Task<Dictionary<string, JsonBuilding>> LoadBuildingsAsync()
        {
            if (!File.Exists(_jsonDataPath))
            {
                throw new FileNotFoundException($"The file '{_jsonDataPath}' was not found.");
            }

            var jsonData = await File.ReadAllTextAsync(_jsonDataPath);
            var root = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, object>>>(jsonData);

            if (root == null || !root.TryGetValue("buildings", out var buildingProperties))
            {
                throw new InvalidDataException("Invalid JSON structure: 'buildings' property not found.");
            }

            var buildings = new Dictionary<string, JsonBuilding>();
            foreach (var buildingProperty in buildingProperties)
            {
                var key = buildingProperty.Key;
                var value = JsonConvert.DeserializeObject<JsonBuilding>(buildingProperty.Value.ToString() ?? throw new InvalidOperationException($"The {buildingProperty.Key} property was null."));
                buildings.Add(key, value);
            }

            return buildings;
        }

        /// <summary>
        /// Gets the buildings that were found in the json data file.
        /// </summary>
        public IReadOnlyDictionary<string, JsonBuilding> Buildings => _buildingsAsync.Value.GetAwaiter().GetResult();
    }
}