namespace CoreFramework.Enums;

/// <summary>
/// Represents the different application environments for configuration and deployment.
/// </summary>
public enum AppEnvironment
{
    /// <summary>
    /// Represents the Quality Assurance (QA) environment, typically used for testing and validation.
    /// </summary>
    QA,

    /// <summary>
    /// Represents the Staging environment, used as a pre-production environment for final testing.
    /// </summary>
    Stage,

    /// <summary>
    /// Represents the Production environment, used for live deployment and end-user access.
    /// </summary>
    Production
}

