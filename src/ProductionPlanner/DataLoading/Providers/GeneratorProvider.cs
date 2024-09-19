using Newtonsoft.Json;

namespace ProductionPlanner.DataLoading
{
    public class GeneratorProvider
    {
        /// <summary>
        /// Holds the path to the json data file.
        /// </summary>
        private readonly string _jsonDataPath;
        
        /// <summary>
        /// Holds the value factory for loading generators, and exposes the generators.
        /// </summary>
        private readonly Lazy<Task<Dictionary<string, JsonGenerator>>> _generatorsAsync;

        /// <summary>
        /// Initializes a new instance of the <see cref="GeneratorProvider"/> class.
        /// </summary>
        /// <param name="jsonDataPath">The path to the json data file.</param>
        /// <exception cref="ArgumentNullException"><paramref name="jsonDataPath"/> is null.</exception>
        public GeneratorProvider(string jsonDataPath)
        {
            _jsonDataPath = jsonDataPath ?? throw new ArgumentNullException(nameof(jsonDataPath));
            _generatorsAsync = new Lazy<Task<Dictionary<string, JsonGenerator>>>(LoadGeneratorsAsync);
        }

        /// <summary>
        /// Provides the value factory for the lazy-loaded <see cref="_jsonDataPath"/> field.
        /// </summary>
        /// <returns>A task that represents the asynchronous data loading operation, which loads generator data from the json data file.</returns>
        /// <exception cref="FileNotFoundException">The json data file was not found.</exception>
        /// <exception cref="InvalidDataException">The json data file was invalid.</exception>
        /// <exception cref="InvalidOperationException">One of the generators in the json data file had a null body.</exception>
        private async Task<Dictionary<string, JsonGenerator>> LoadGeneratorsAsync()
        {
            if (!File.Exists(_jsonDataPath))
            {
                throw new FileNotFoundException($"The file '{_jsonDataPath}' was not found.");
            }

            var jsonData = await File.ReadAllTextAsync(_jsonDataPath);
            var root = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, object>>>(jsonData);

            if (root == null || !root.TryGetValue("generators", out var generatorProperties))
            {
                throw new InvalidDataException("Invalid JSON structure: 'generators' property not found.");
            }

            var generators = new Dictionary<string, JsonGenerator>();
            foreach (var generatorProperty in generatorProperties)
            {
                var key = generatorProperty.Key;
                var value = JsonConvert.DeserializeObject<JsonGenerator>(generatorProperty.Value.ToString() ?? throw new InvalidOperationException($"The {generatorProperty.Key} property was null."));
                generators.Add(key, value);
            }

            return generators;
        }

        /// <summary>
        /// Gets the generators that were found in the json data file.
        /// </summary>
        public IReadOnlyDictionary<string, JsonGenerator> Generators => _generatorsAsync.Value.GetAwaiter().GetResult();
    }
}