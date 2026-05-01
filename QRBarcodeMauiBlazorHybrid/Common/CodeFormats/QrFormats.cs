using QRBarcodeMauiBlazorHybrid.Models.QR;

namespace QRBarcodeMauiBlazorHybrid.Common.CodeFormats;

/// <summary>
/// Source of truth for QR data types: maps each type identifier to its UI display name and model factory.
/// </summary>
public static class QrFormats
{
    /// <summary>
    /// Format value persisted on <see cref="Models.CodeModel.Format"/> for every QR code, regardless of <c>Type</c>.
    /// </summary>
    public const string QR_CODE_FORMAT = "QRCode";

    /// <summary>
    /// Type override applied to <see cref="Models.CodeModel.Type"/> when generating from an Ean13 with an ISBN
    /// prefix (978/979). Distinguishes ISBNs from generic Ean13 product codes.
    /// </summary>
    public const string ISBN_TYPE = "Isbn";

    public static readonly IReadOnlyList<QrFormatInfo> All =
    [
        new("Text",                  "Text",                   () => new TextModel()),
        new("Url",                   "Url",                    () => new UrlModel()),
        new("Email",                 "Email",                  () => new EmailModel()),
        new("WiFi",                  "WiFi",                   () => new WiFiModel()),
        new("GeographicCoordinates", "Geographic Coordinates", () => new GeographicCoordinatesModel()),
        new("ContactInfo",           "Contact Info",           () => new ContactInfoModel()),
        new("CalendarEvent",         "Calendar Event",         () => new CalendarEventModel()),
        new("Phone",                 "Phone",                  () => new PhoneModel()),
        new("Sms",                   "Sms",                    () => new SmsModel())
    ];

    private static readonly Dictionary<string, QrFormatInfo> _typeMap =
        All.ToDictionary(qrFormat => qrFormat.Type);

    /// <summary>
    /// Looks up a QR data type by identifier. Returns <c>null</c> when the identifier is not a known QR type.
    /// </summary>
    public static QrFormatInfo? Get(string? type) =>
        type is null ? null : _typeMap.GetValueOrDefault(type);
}
