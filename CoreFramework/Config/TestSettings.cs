﻿using CoreFramework.Enums;

namespace CoreFramework.Config
{

    /// <summary>
    /// Represents the configuration settings for test execution.
    /// </summary>
    public class TestSettings
    {
        /// <summary>
        /// Gets or sets the browser type to be used for the test execution.
        /// </summary>
        public Browser SupportedBrowser { get; set; }

        /// <summary>
        /// Gets or sets the command-line arguments to be passed to the browser.
        /// </summary>
        public string[]? Args { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the browser should run in headless mode.
        /// </summary>
        public bool? Headless { get; set; }

        /// <summary>
        /// Gets or sets the delay (in milliseconds) to slow down browser operations for debugging purposes.
        /// </summary>
        public float? SlowMo { get; set; }

        /// <summary>
        /// Gets or sets the timeout (in seconds) for browser operations.
        /// </summary>
        public float? Timeout { get; set; }

        /// <summary>
        /// Gets or sets the Environment settings.
        /// </summary>
        public Environment? Environment { get; set; }
    }

    /// <summary>
    /// Represents the environment configuration for the application.
    /// </summary>
    public class Environment
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

