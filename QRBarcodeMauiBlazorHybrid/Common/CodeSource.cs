namespace QRBarcodeMauiBlazorHybrid.Common;

/// <summary>
/// Identifiers for how a code entered the app, persisted on <see cref="Models.CodeModel.Source"/>.
/// </summary>
public static class CodeSource
{
    public const string SCANNED = "Scanned";
    public const string GENERATED = "Generated";
}
