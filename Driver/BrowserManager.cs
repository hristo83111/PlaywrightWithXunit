using CoreFramework.Config;
using CoreFramework.Enums;
using Microsoft.Playwright;

namespace CoreFramework.Driver;

/// <summary>
/// Manages the initialization and configuration of browser instances using Playwright.
/// </summary>
/// <param name="testSettings">The test settings used to configure browser instances.</param>
public class BrowserManager(TestSettings testSettings)
{
    /// <summary>
    /// Retrieves a browser instance based on the specified browser type.
    /// </summary>
    /// <param name="supportedBrowser">The type of browser to initialize.</param>
    /// <returns>An instance of <see cref="IBrowser"/> configured with the specified settings.</returns>
    public async Task<IBrowser> GetBrowserAsync(SupportedBrowser supportedBrowser)
    {
        var options = GetParameters();

        options.Channel = GetChannelForBrowser(supportedBrowser);

        return await LaunchBrowserAsync(supportedBrowser, options);
    }

    /// <summary>
    /// Launches a browser instance based on the specified browser type and launch options.
    /// </summary>
    /// <param name="supportedBrowser">The type of browser to launch.</param>
    /// <param name="options">The launch options for the browser.</param>
    /// <returns>An instance of <see cref="IBrowser"/>.</returns>
    private async Task<IBrowser> LaunchBrowserAsync(SupportedBrowser supportedBrowser, BrowserTypeLaunchOptions options)
    {
        var playwright = await Playwright.CreateAsync();
        IBrowserType browserType = supportedBrowser switch
        {
            SupportedBrowser.Chromium or SupportedBrowser.Chrome or SupportedBrowser.Edge => playwright.Chromium,
            SupportedBrowser.Firefox => playwright.Firefox,
            SupportedBrowser.Webkit => playwright.Webkit,
            _ => throw new ArgumentOutOfRangeException(nameof(supportedBrowser), $"Unsupported browser type: {supportedBrowser}")
        };

        return await browserType.LaunchAsync(options);
    }

    /// <summary>
    /// Configures the browser launch options based on the provided test settings.
    /// </summary>
    /// <returns>An instance of <see cref="BrowserTypeLaunchOptions"/> containing the configured options.</returns>
    private BrowserTypeLaunchOptions GetParameters()
    {
        return new BrowserTypeLaunchOptions
        {
            Args = testSettings.Args,
            Timeout = testSettings.Timeout,
            Headless = testSettings.Headless,
            SlowMo = testSettings.SlowMo
        };
    }

    /// <summary>
    /// Determines the appropriate channel for the specified browser.
    /// </summary>
    /// <param name="supportedBrowser">The browser type.</param>
    /// <returns>The channel name, or null if no channel is required.</returns>
    private string? GetChannelForBrowser(SupportedBrowser supportedBrowser)
    {
        return supportedBrowser switch
        {
            SupportedBrowser.Chromium => "chromium",
            SupportedBrowser.Chrome => "chrome",
            SupportedBrowser.Edge => "msedge",
            _ => null // No channel required for Firefox and Webkit
        };
    }
}

