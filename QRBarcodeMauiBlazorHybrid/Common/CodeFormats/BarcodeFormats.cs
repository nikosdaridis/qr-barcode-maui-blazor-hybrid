using QRBarcodeMauiBlazorHybrid.Models.Barcode;
using BarcodeType = BarcodeStandard.Type;

namespace QRBarcodeMauiBlazorHybrid.Common.CodeFormats;

/// <summary>
/// Source of truth for barcode formats: maps each format identifier to its data category, <see cref="BarcodeStandard.Type"/>, and model factory.
/// </summary>
public static class BarcodeFormats
{
    public static readonly IReadOnlyList<BarcodeFormatInfo> All =
    [
        new("Code128", "Text",    BarcodeType.Code128,         () => new Code128Model()),
        new("Code39",  "Text",    BarcodeType.Code39,          () => new Code39Model()),
        new("Code93",  "Text",    BarcodeType.Code93,          () => new Code93Model()),
        new("CodaBar", "Text",    BarcodeType.Codabar,         () => new CodaBarModel()),
        new("Ean13",   "Product", BarcodeType.Ean13,           () => new Ean13Model()),
        new("Ean8",    "Product", BarcodeType.Ean8,            () => new Ean8Model()),
        new("Itf",     "Text",    BarcodeType.Interleaved2Of5, () => new ItfModel()),
        new("Upca",    "Product", BarcodeType.UpcA,            () => new UpcaModel()),
        new("Upce",    "Product", BarcodeType.UpcE,            () => new UpceModel()),
        new("I2OF5",   "Text",    BarcodeType.Industrial2Of5,  () => new I2OF5Model())
    ];

    private static readonly Dictionary<string, BarcodeFormatInfo> _formatMap =
        All.ToDictionary(barcodeFormat => barcodeFormat.Format);

    /// <summary>
    /// Looks up a format by identifier. Returns <c>null</c> when the identifier is not a known barcode format.
    /// </summary>
    public static BarcodeFormatInfo? Get(string? format) =>
        format is null ? null : _formatMap.GetValueOrDefault(format);
}
