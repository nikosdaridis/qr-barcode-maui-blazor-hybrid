using QRBarcodeApp.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace QRBarcodeApp.Models.QR
{
    public class SmsModel : IGenerateModel
    {
        [Required(ErrorMessage = "Phone is required")]
        [RegularExpression("^\\d+$", ErrorMessage = "Invalid Phone")]
        public ulong? Phone { get; set; }

        [Required(ErrorMessage = "Message is required")]
        public string? Message { get; set; }

        public string GetValue()
        {
            return $"smsto:{Phone}:{Message}";
        }
    }
}
