using QRBarcodeApp.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace QRBarcodeApp.Models.Barcode
{
    public class ItfModel : IGenerateModel
    {
        [Required(ErrorMessage = "Itf is required")]
        [RegularExpression(@"^(\d{2})+$", ErrorMessage = "Itf must contain an even number of digits")]
        public string? Itf { get; set; }

        public string GetValue() =>
            Itf ?? string.Empty;
    }
}
