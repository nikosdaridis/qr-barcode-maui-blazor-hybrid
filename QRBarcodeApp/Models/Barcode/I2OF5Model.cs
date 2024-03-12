using QRBarcodeApp.Helpers;
using System.ComponentModel.DataAnnotations;

namespace QRBarcodeApp.Models.Barcode
{
    public class I2OF5Model : IGenerateModel
    {
        [Required(ErrorMessage = "I2OF5 is required")]
        [RegularExpression(@"^(\d{2})+$", ErrorMessage = "I2OF5 must contain an even number of digits")]
        public string? I2OF5 { get; set; }

        public string? GetValue()
        {
            return I2OF5;
        }
    }
}
