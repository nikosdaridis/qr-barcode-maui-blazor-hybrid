using QRBarcodeApp.Helpers;
using System.ComponentModel.DataAnnotations;

namespace QRBarcodeApp.Models.Barcode
{
    public class Code39Model : IGenerateModel
    {
        [Required(ErrorMessage = "Code39 is required")]
        [RegularExpression(@"^[A-Z0-9\-.$/+% ]+$", ErrorMessage = "Code39 must contain only uppercase letters, digits, and special characters (- . $ / + % space)")]
        public string? Code39 { get; set; }

        public string? GetValue()
        {
            return Code39;
        }
    }
}
