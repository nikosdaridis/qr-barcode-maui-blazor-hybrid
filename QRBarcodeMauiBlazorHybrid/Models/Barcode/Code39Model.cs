using QRBarcodeMauiBlazorHybrid.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace QRBarcodeMauiBlazorHybrid.Models.Barcode
{
    public class Code39Model : IGenerateModel
    {
        [Required(ErrorMessage = "Code39 is required")]
        [RegularExpression(@"^[A-Z0-9\-.$/+% ]+$", ErrorMessage = "Code39 must contain only uppercase letters, digits, and special characters (- . $ / + % space)")]
        public string? Code39 { get; set; }

        public string GetValue() =>
            Code39 ?? string.Empty;
    }
}
