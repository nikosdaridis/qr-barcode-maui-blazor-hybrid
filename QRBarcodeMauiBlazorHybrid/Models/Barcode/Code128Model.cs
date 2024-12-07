using QRBarcodeMauiBlazorHybrid.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace QRBarcodeMauiBlazorHybrid.Models.Barcode
{
    public class Code128Model : IGenerateModel
    {
        [Required(ErrorMessage = "Code128 is required")]
        [RegularExpression(@"^[\x00-\x7F]+$", ErrorMessage = "Code128 must contain only ASCII characters")]
        public string? Code128 { get; set; }

        public string GetValue() =>
            Code128 ?? string.Empty;
    }
}
