using CoreFramework.Enums;
using Microsoft.Playwright;

namespace CoreFramework.Driver;

public interface IPlaywrightBrowserManager
{
    Task<IBrowser> GetBrowserAsync();
}
