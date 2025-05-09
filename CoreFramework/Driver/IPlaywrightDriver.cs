using Microsoft.Playwright;

namespace CoreFramework.Driver
{
    /// <summary>
    /// Defines the contract for managing Playwright browser instances, contexts, and pages.
    /// </summary>
    public interface IPlaywrightDriver
    {
        /// <summary>
        /// Gets the Playwright browser instance, lazily initialized.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="IBrowser"/> instance.</returns>
        Task<IBrowser> Browser { get; }

        /// <summary>
        /// Gets the Playwright browser context, lazily initialized.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="IBrowserContext"/> instance.</returns>
        Task<IBrowserContext> BrowserContext { get; }

        /// <summary>
        /// Gets the Playwright page instance, lazily initialized.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="IPage"/> instance.</returns>
        Task<IPage> Page { get; }

        /// <summary>
        /// Disposes of the Playwright browser instance and releases resources.
        /// </summary>
        void Dispose();
    }
}