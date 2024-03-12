using QRBarcodeApp.Helpers;
using System.ComponentModel.DataAnnotations;

namespace QRBarcodeApp.Models.Barcode
{
    public class CodaBarModel : IGenerateModel
    {
        [Required(ErrorMessage = "CodaBar is required")]
        [RegularExpression(@"^[A-Da-d][0-9\-\$:\/\.\+]*[A-Da-d]$", ErrorMessage = "CodaBar must start and end with A, B, C, or D and contain only valid characters")]
        public string? CodaBar { get; set; }

        public string? GetValue()
        {
            return CodaBar;
        }
    }
}
