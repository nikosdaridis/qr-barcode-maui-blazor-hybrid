using QRBarcodeMauiBlazorHybrid.Common.Interfaces;
using BarcodeType = BarcodeStandard.Type;

namespace QRBarcodeMauiBlazorHybrid.Common.CodeFormats;

/// <summary>
/// Metadata describing a supported barcode format.
/// </summary>
public sealed record BarcodeFormatInfo(
    string Format,
    string Type,
    BarcodeType BarcodeType,
    Func<IGenerateModel> Factory);
