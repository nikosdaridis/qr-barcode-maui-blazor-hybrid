using QRBarcodeApp.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace QRBarcodeApp.Models.Barcode
{
    public class UpceModel : IGenerateModel
    {
        [Required(ErrorMessage = "Upce is required")]
        [RegularExpression(@"^[0-1]\d{7}$", ErrorMessage = "Upce must start with 0 or 1 and be 8 digits")]
        public string? Upce { get; set; } = null;

        public string GetValue() =>
            Upce ?? string.Empty;
    }
}
