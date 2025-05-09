using Microsoft.Playwright;

namespace CoreFramework.Driver;

/// <summary>
/// Defines the contract for managing browser instances using Playwright.
/// </summary>
public interface IPlaywrightBrowserManager
{
    /// <summary>
    /// Retrieves a browser instance based on the specified configuration.
    /// </summary>
    /// <returns>An instance of <see cref="IBrowser"/> configured with the specified settings.</returns>
    Task<IBrowser> GetBrowserAsync();
}
