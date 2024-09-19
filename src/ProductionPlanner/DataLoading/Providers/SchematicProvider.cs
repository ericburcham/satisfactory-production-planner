using Newtonsoft.Json;

namespace ProductionPlanner.DataLoading
{
    public class SchematicProvider
    {
        /// <summary>
        /// Holds the path to the json data file.
        /// </summary>
        private readonly string _jsonDataPath;
        
        /// <summary>
        /// Holds the value factory for loading schematics, and exposes the schematics.
        /// </summary>
        private readonly Lazy<Task<Dictionary<string, JsonSchematic>>> _schematicsAsync;

        /// <summary>
        /// Initializes a new instance of the <see cref="SchematicProvider"/> class.
        /// </summary>
        /// <param name="jsonDataPath">The path to the json data file.</param>
        /// <exception cref="ArgumentNullException"><paramref name="jsonDataPath"/> is null.</exception>
        public SchematicProvider(string jsonDataPath)
        {
            _jsonDataPath = jsonDataPath ?? throw new ArgumentNullException(nameof(jsonDataPath));
            _schematicsAsync = new Lazy<Task<Dictionary<string, JsonSchematic>>>(LoadSchematicsAsync);
        }

        /// <summary>
        /// Provides the value factory for the lazy-loaded <see cref="_jsonDataPath"/> field.
        /// </summary>
        /// <returns>A task that represents the asynchronous data loading operation, which loads schematic data from the json data file.</returns>
        /// <exception cref="FileNotFoundException">The json data file was not found.</exception>
        /// <exception cref="InvalidDataException">The json data file was invalid.</exception>
        /// <exception cref="InvalidOperationException">One of the schematics in the json data file had a null body.</exception>
        private async Task<Dictionary<string, JsonSchematic>> LoadSchematicsAsync()
        {
            if (!File.Exists(_jsonDataPath))
            {
                throw new FileNotFoundException($"The file '{_jsonDataPath}' was not found.");
            }

            var jsonData = await File.ReadAllTextAsync(_jsonDataPath);
            var root = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, object>>>(jsonData);

            if (root == null || !root.TryGetValue("schematics", out var schematicProperties))
            {
                throw new InvalidDataException("Invalid JSON structure: 'schematics' property not found.");
            }

            var schematics = new Dictionary<string, JsonSchematic>();
            foreach (var schematicProperty in schematicProperties)
            {
                var key = schematicProperty.Key;
                var jsonPayload = schematicProperty.Value.ToString();
                var value = JsonConvert.DeserializeObject<JsonSchematic>(jsonPayload ?? throw new InvalidOperationException($"The {schematicProperty.Key} property was null."));
                schematics.Add(key, value);
            }

            return schematics;
        }

        /// <summary>
        /// Gets the schematics that were found in the json data file.
        /// </summary>
        public IReadOnlyDictionary<string, JsonSchematic> Schematics => _schematicsAsync.Value.GetAwaiter().GetResult();
    }
}