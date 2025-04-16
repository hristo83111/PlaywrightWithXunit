using CoreFramework.Enums;
using Microsoft.Playwright;

namespace CoreFramework.Driver
{
    public interface IBrowserManager
    {
        Task<IBrowser> GetBrowserAsync(Browser supportedBrowser);
    }
}