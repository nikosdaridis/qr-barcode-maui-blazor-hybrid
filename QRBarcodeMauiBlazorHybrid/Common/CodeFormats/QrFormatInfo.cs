using QRBarcodeMauiBlazorHybrid.Common.Interfaces;

namespace QRBarcodeMauiBlazorHybrid.Common.CodeFormats;

/// <summary>
/// Metadata describing a supported QR data type.
/// </summary>
public sealed record QrFormatInfo(
    string Type,
    string DisplayName,
    Func<IGenerateModel> Factory);
