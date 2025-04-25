using Microsoft.Playwright;

namespace CoreFramework.Driver;

/// <summary>
/// Manages the initialization and configuration of browser instances using Playwright.
/// </summary>
public class PlaywrightBrowserManager : IPlaywrightBrowserManager
{
    /// <summary>
    /// Retrieves a browser instance based on the specified browser type.
    /// </summary>
    /// <returns>An instance of <see cref="IBrowser"/> configured with the specified settings.</returns>
    public async Task<IBrowser> GetBrowserAsync()
    {
        var options = GetParameters();

        options.Channel = GetChannelForBrowser();

        return await LaunchBrowserAsync(options);
    }

    /// <summary>
    /// Launches a browser instance based on the specified browser type and launch options.
    /// </summary>
    /// <param name="options">The launch options for the browser.</param>
    /// <returns>An instance of <see cref="IBrowser"/>.</returns>
    private async Task<IBrowser> LaunchBrowserAsync(BrowserTypeLaunchOptions options)
    {
        var playwright = await Playwright.CreateAsync();
        var browserType = GetBrowserType(playwright);

        return await browserType.LaunchAsync(options);
    }

    /// <summary>
    /// Configures the browser launch options based on the values from the run settings file.
    /// </summary>
    /// <returns>An instance of <see cref="BrowserTypeLaunchOptions"/> containing the configured options.</returns>
    private BrowserTypeLaunchOptions GetParameters()
    {
        return new BrowserTypeLaunchOptions
        {
            Args = (Environment.GetEnvironmentVariable("Args") ?? "").Split(' ', StringSplitOptions.RemoveEmptyEntries),
            Timeout = float.TryParse(Environment.GetEnvironmentVariable("Timeout"), out var timeout) ? timeout * 1000 : 30000,
            Headless = !Environment.GetEnvironmentVariable("Headless")?.Equals("false", StringComparison.OrdinalIgnoreCase) ?? true,
            SlowMo = float.TryParse(Environment.GetEnvironmentVariable("SlowMo"), out var slowMo) ? slowMo : 0
        };
    }

    /// <summary>
    /// Determines the appropriate channel for the specified browser.
    /// </summary>
    /// <returns>The channel name, or null if no channel is required.</returns>
    private string? GetChannelForBrowser()
    {
        var browser = Environment.GetEnvironmentVariable("Browser") ?? "Chromium";
        return browser switch
        {
            "Chromium" => "chromium",
            "Chrome" => "chrome",
            "Edge" => "msedge",
            _ => null // No channel required for Firefox and Webkit
        };
    }

    /// <summary>
    /// Retrieves the appropriate browser type from Playwright based on the browser specified in the run settings.
    /// </summary>
    /// <param name="playwright">The Playwright instance.</param>
    /// <returns>The browser type.</returns>
    private IBrowserType GetBrowserType(IPlaywright playwright)
    {
        var browser = Environment.GetEnvironmentVariable("Browser") ?? "Chromium";
        return browser switch
        {
            "Chromium" or "Chrome" or "Edge" => playwright.Chromium,
            "Firefox" => playwright.Firefox,
            "Webkit" => playwright.Webkit,
            _ => throw new ArgumentOutOfRangeException(nameof(browser), $"Unsupported browser type: {browser}")
        };
    }
}