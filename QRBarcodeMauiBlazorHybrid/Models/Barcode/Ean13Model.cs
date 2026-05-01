using QRBarcodeMauiBlazorHybrid.Common.Attributes;
using QRBarcodeMauiBlazorHybrid.Common.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace QRBarcodeMauiBlazorHybrid.Models.Barcode;

public sealed class Ean13Model : IGenerateModel
{
    [Required(ErrorMessage = "Ean13 is required")]
    [RegularExpression(@"^\d{13}$", ErrorMessage = "Ean13 must be 13 digits")]
    [InputType(InputType.Number)]
    public string? Ean13 { get; set; } = null;

    /// <summary>
    /// True when the value is an ISBN-13 (Ean13 with a 978 or 979 prefix).
    /// </summary>
    public bool IsIsbn =>
        Ean13 is not null && (Ean13.StartsWith("978") || Ean13.StartsWith("979"));

    public string GetValue() =>
        Ean13 ?? string.Empty;
}
