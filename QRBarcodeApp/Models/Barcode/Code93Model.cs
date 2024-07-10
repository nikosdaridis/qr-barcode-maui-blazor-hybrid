using QRBarcodeApp.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace QRBarcodeApp.Models.Barcode
{
    public class Code93Model : IGenerateModel
    {
        [Required(ErrorMessage = "Code93 is required")]
        [RegularExpression(@"^[A-Z0-9\-.$/+% ]+$", ErrorMessage = "Code93 must contain only uppercase letters, digits, and special characters (- . $ / + % space)")]
        public string? Code93 { get; set; }

        public string GetValue()
        {
            return Code93 ?? "";
        }
    }
}
