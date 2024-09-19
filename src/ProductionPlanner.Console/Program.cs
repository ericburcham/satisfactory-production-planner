// Define the recipes

using ProductionPlanner.DataLoading;

var jsonDataPath = Path.Combine("data-files", "data1.0.json");

var itemProvider = new ItemProvider(jsonDataPath);
Console.WriteLine($"Loaded {itemProvider.Items.Count} items");
foreach (var item in itemProvider.Items.OrderBy(item => item.Value.Name)) Console.WriteLine(item.Value.Name);
Console.WriteLine("----------------------------------\n\n\n\n\n");

var buildingProvider = new BuildingProvider(jsonDataPath);
Console.WriteLine($"Loaded {buildingProvider.Buildings.Count} buildings");
foreach (var building in buildingProvider.Buildings.OrderBy(building => building.Value.Name)) Console.WriteLine(building.Value.Name);
Console.WriteLine("----------------------------------\n\n\n\n\n");

var resourceProvider = new ResourceProvider(jsonDataPath);
Console.WriteLine($"Loaded {resourceProvider.Resources.Count} resources");
foreach (var resource in resourceProvider.Resources.OrderBy(resource => resource.Value.Item)) Console.WriteLine(resource.Value.Item);
Console.WriteLine("----------------------------------\n\n\n\n\n");

var generatorProvider = new GeneratorProvider(jsonDataPath);
Console.WriteLine($"Loaded {generatorProvider.Generators.Count} generators");
foreach (var generator in generatorProvider.Generators.OrderBy(generator => generator.Value.ClassName)) Console.WriteLine(generator.Value.ClassName);
Console.WriteLine("----------------------------------\n\n\n\n\n");

var minerProvider = new MinerProvider(jsonDataPath);
Console.WriteLine($"Loaded {minerProvider.Miners.Count} miners");
foreach (var miner in minerProvider.Miners.OrderBy(miner => miner.Value.ClassName)) Console.WriteLine(miner.Value.ClassName);
Console.WriteLine("----------------------------------\n\n\n\n\n");

var schematicProvider = new SchematicProvider(jsonDataPath);
Console.WriteLine($"Loaded {schematicProvider.Schematics.Count} schematics");
foreach (var schematic in schematicProvider.Schematics.OrderBy(schematic => schematic.Value.ClassName)) Console.WriteLine(schematic.Value.ClassName);
Console.WriteLine("----------------------------------\n\n\n\n\n");

var recipeProvider = new RecipeProvider(jsonDataPath);
Console.WriteLine($"Loaded {recipeProvider.Recipes.Count} recipes");
foreach (var recipe in recipeProvider.Recipes.OrderBy(recipe => recipe.Value.ClassName)) Console.WriteLine(recipe.Value.ClassName);
