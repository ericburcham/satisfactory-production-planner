using Newtonsoft.Json;

namespace ProductionPlanner.DataLoading
{
    public class RecipeProvider
    {
        /// <summary>
        /// Holds the path to the json data file.
        /// </summary>
        private readonly string _jsonDataPath;
        
        /// <summary>
        /// Holds the value factory for loading recipes, and exposes the recipes.
        /// </summary>
        private readonly Lazy<Task<Dictionary<string, JsonRecipe>>> _recipesAsync;

        /// <summary>
        /// Initializes a new instance of the <see cref="RecipeProvider"/> class.
        /// </summary>
        /// <param name="jsonDataPath">The path to the json data file.</param>
        /// <exception cref="ArgumentNullException"><paramref name="jsonDataPath"/> is null.</exception>
        public RecipeProvider(string jsonDataPath)
        {
            _jsonDataPath = jsonDataPath ?? throw new ArgumentNullException(nameof(jsonDataPath));
            _recipesAsync = new Lazy<Task<Dictionary<string, JsonRecipe>>>(LoadRecipesAsync);
        }

        /// <summary>
        /// Provides the value factory for the lazy-loaded <see cref="_jsonDataPath"/> field.
        /// </summary>
        /// <returns>A task that represents the asynchronous data loading operation, which loads recipe data from the json data file.</returns>
        /// <exception cref="FileNotFoundException">The json data file was not found.</exception>
        /// <exception cref="InvalidDataException">The json data file was invalid.</exception>
        /// <exception cref="InvalidOperationException">One of the recipes in the json data file had a null body.</exception>
        private async Task<Dictionary<string, JsonRecipe>> LoadRecipesAsync()
        {
            if (!File.Exists(_jsonDataPath))
            {
                throw new FileNotFoundException($"The file '{_jsonDataPath}' was not found.");
            }

            var jsonData = await File.ReadAllTextAsync(_jsonDataPath);
            var root = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, object>>>(jsonData);

            if (root == null || !root.TryGetValue("recipes", out var recipeProperties))
            {
                throw new InvalidDataException("Invalid JSON structure: 'recipes' property not found.");
            }

            var recipes = new Dictionary<string, JsonRecipe>();
            foreach (var recipeProperty in recipeProperties)
            {
                var key = recipeProperty.Key;
                var value = JsonConvert.DeserializeObject<JsonRecipe>(recipeProperty.Value.ToString() ?? throw new InvalidOperationException($"The {recipeProperty.Key} property was null."));
                recipes.Add(key, value);
            }

            return recipes;
        }

        /// <summary>
        /// Gets the recipes that were found in the json data file.
        /// </summary>
        public IReadOnlyDictionary<string, JsonRecipe> Recipes => _recipesAsync.Value.GetAwaiter().GetResult();
    }
}