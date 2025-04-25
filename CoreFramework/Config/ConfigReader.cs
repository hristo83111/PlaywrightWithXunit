using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CoreFramework.Config;

/// <summary>
/// Provides functionality to read and deserialize configuration settings from a JSON file.
/// </summary>
public static class ConfigReader
{
    /// <summary>
    /// Reads the configuration settings from the appropriate environment-specific JSON file and deserializes them into the specified type.
    /// </summary>
    /// <typeparam name="T">The type into which the configuration file will be deserialized.</typeparam>
    /// <returns>The deserialized object of type <typeparamref name="T"/> containing the configuration settings.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the configuration file cannot be deserialized.</exception>
    /// <exception cref="FileNotFoundException">Thrown when the configuration file for the specified environment is not found.</exception>
    public static T ReadConfig<T>()
    {
        // Retrieve the environment from the runsettings file
        var environment = Environment.GetEnvironmentVariable("Environment") 
            ?? throw new InvalidOperationException("Environment variable 'Environment' is not set.");

        var configFilePath = Path.Combine(
            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty,
            $"appsettings.{environment}.json"
        );

        if (!File.Exists(configFilePath))
        {
            throw new FileNotFoundException($"Configuration file not found: {configFilePath}");
        }

        var configFile = File.ReadAllText(configFilePath);

        var deserializedConfig = JsonSerializer.Deserialize<T>(configFile, CachedJsonSerializerOptions);

        return deserializedConfig ?? throw new InvalidOperationException("Failed to deserialize configuration file.");
    }

    /// <summary>
    /// Cached JSON serializer options to improve performance and ensure consistent deserialization behavior.
    /// </summary>
    private static readonly JsonSerializerOptions CachedJsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter() }
    };
}