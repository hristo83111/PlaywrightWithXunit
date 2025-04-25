namespace ConduitTest.Configuration
{
    /// <summary>
    /// Represents the configuration settings for test execution.
    /// </summary>
    public class TestSettings
    {
        /// <summary>
        /// Gets or sets the application base URL to be tested.
        /// </summary>
        public string? ApplicationBaseUrl { get; set; }

        /// <summary>
        /// Gets or sets the API base URL to be tested.
        /// </summary>
        public string? APIBaseUrl { get; set; }
    }
}
