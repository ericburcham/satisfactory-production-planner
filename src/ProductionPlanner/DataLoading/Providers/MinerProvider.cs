using Newtonsoft.Json;

namespace ProductionPlanner.DataLoading
{
    public class MinerProvider
    {
        /// <summary>
        /// Holds the path to the json data file.
        /// </summary>
        private readonly string _jsonDataPath;
        
        /// <summary>
        /// Holds the value factory for loading miners, and exposes the miners.
        /// </summary>
        private readonly Lazy<Task<Dictionary<string, JsonMiner>>> _minersAsync;

        /// <summary>
        /// Initializes a new instance of the <see cref="MinerProvider"/> class.
        /// </summary>
        /// <param name="jsonDataPath">The path to the json data file.</param>
        /// <exception cref="ArgumentNullException"><paramref name="jsonDataPath"/> is null.</exception>
        public MinerProvider(string jsonDataPath)
        {
            _jsonDataPath = jsonDataPath ?? throw new ArgumentNullException(nameof(jsonDataPath));
            _minersAsync = new Lazy<Task<Dictionary<string, JsonMiner>>>(LoadMinersAsync);
        }

        /// <summary>
        /// Provides the value factory for the lazy-loaded <see cref="_jsonDataPath"/> field.
        /// </summary>
        /// <returns>A task that represents the asynchronous data loading operation, which loads miner data from the json data file.</returns>
        /// <exception cref="FileNotFoundException">The json data file was not found.</exception>
        /// <exception cref="InvalidDataException">The json data file was invalid.</exception>
        /// <exception cref="InvalidOperationException">One of the miners in the json data file had a null body.</exception>
        private async Task<Dictionary<string, JsonMiner>> LoadMinersAsync()
        {
            if (!File.Exists(_jsonDataPath))
            {
                throw new FileNotFoundException($"The file '{_jsonDataPath}' was not found.");
            }

            var jsonData = await File.ReadAllTextAsync(_jsonDataPath);
            var root = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, object>>>(jsonData);

            if (root == null || !root.TryGetValue("miners", out var minerProperties))
            {
                throw new InvalidDataException("Invalid JSON structure: 'miners' property not found.");
            }

            var miners = new Dictionary<string, JsonMiner>();
            foreach (var minerProperty in minerProperties)
            {
                var key = minerProperty.Key;
                var value = JsonConvert.DeserializeObject<JsonMiner>(minerProperty.Value.ToString() ?? throw new InvalidOperationException($"The {minerProperty.Key} property was null."));
                miners.Add(key, value);
            }

            return miners;
        }

        /// <summary>
        /// Gets the miners that were found in the json data file.
        /// </summary>
        public IReadOnlyDictionary<string, JsonMiner> Miners => _minersAsync.Value.GetAwaiter().GetResult();
    }
}