using QRBarcodeMauiBlazorHybrid.Common.Attributes;
using QRBarcodeMauiBlazorHybrid.Common.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace QRBarcodeMauiBlazorHybrid.Models.Barcode
{
    public class Ean8Model : IGenerateModel
    {
        [Required(ErrorMessage = "Ean8 is required")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "Ean8 must be 8 digits")]
        [InputType(InputType.Number)]
        public string? Ean8 { get; set; } = null;

        public string GetValue() =>
            Ean8 ?? string.Empty;
    }
}
