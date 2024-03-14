using QRBarcodeApp.Helpers;
using System.ComponentModel.DataAnnotations;

namespace QRBarcodeApp.Models.Barcode
{
    public class Ean8Model : IGenerateModel
    {
        [Required(ErrorMessage = "Ean8 is required")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "Ean8 must be 8 digits")]
        public string? Ean8 { get; set; } = null;

        public string GetValue()
        {
            return Ean8 ?? "";
        }
    }
}
