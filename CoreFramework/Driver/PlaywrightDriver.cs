using Microsoft.Playwright;

namespace CoreFramework.Driver;

public class PlaywrightDriver
{
    private readonly AsyncLazy<IBrowser> _browser;

    public PlaywrightDriver(IPlaywrightBrowserManager playwrightBrowserManager)
    {
        _browser = new AsyncLazy<IBrowser>(() => playwrightBrowserManager.GetBrowserAsync());
    }

    public Task<IBrowser> Browser => _browser.Value;
}

