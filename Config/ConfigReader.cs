using CoreFramework.Enums;
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
    /// Reads the configuration settings from the appropriate environment-specific JSON file and deserializes them into a <see cref="TestSettings"/> object.
    /// </summary>
    /// <param name="appEnvironment">The appEnvironment enum value (e.g., <see cref="AppEnvironment.QA"/>, <see cref="AppEnvironment.Stage"/>, <see cref="AppEnvironment.Production"/>).</param>
    /// <returns>The deserialized <see cref="TestSettings"/> object containing the configuration settings.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the configuration file cannot be deserialized.</exception>
    /// <exception cref="FileNotFoundException">Thrown when the configuration file for the specified environment is not found.</exception>
    public static TestSettings ReadConfig(AppEnvironment appEnvironment)
    {
        var configFilePath = Path.Combine(
            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty,
            $"appsettings.{appEnvironment}.json"
        );

        if (!File.Exists(configFilePath))
        {
            throw new FileNotFoundException($"Configuration file not found: {configFilePath}");
        }

        var configFile = File.ReadAllText(configFilePath);

        var deserializedConfig = JsonSerializer.Deserialize<TestSettings>(configFile, CachedJsonSerializerOptions);

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

