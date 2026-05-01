namespace QRBarcodeMauiBlazorHybrid.Common;

/// <summary>
/// Canonical route paths and tab identifiers.
/// </summary>
public static class RoutePaths
{
    public const string HISTORY = "/history";
    public const string FAVORITES = "/favorites";
    public const string GENERATE = "/generate";
    public const string DETAILS = "/details";

    public const string SCAN_TAB = "scan";
    public const string GENERATE_TAB = "generate";
    public const string FAVORITES_TAB = "favorites";
    public const string HISTORY_TAB = "history";

    public const string HISTORY_LABEL = "History";
    public const string FAVORITES_LABEL = "Favorites";
    public const string GENERATE_LABEL = "Generate";

    /// <summary>
    /// Order of tabs rendered by <c>TabBar</c>, left to right.
    /// </summary>
    public static readonly IReadOnlyList<string> TabBarOrder =
        [SCAN_TAB, GENERATE_TAB, FAVORITES_TAB, HISTORY_TAB];
}
