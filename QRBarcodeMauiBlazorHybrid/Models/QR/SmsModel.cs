using QRBarcodeMauiBlazorHybrid.Common.Attributes;
using QRBarcodeMauiBlazorHybrid.Common.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace QRBarcodeMauiBlazorHybrid.Models.QR
{
    public class SmsModel : IGenerateModel
    {
        [Required(ErrorMessage = "Phone is required")]
        [RegularExpression("^\\d+$", ErrorMessage = "Invalid Phone")]
        [InputType(InputType.Number)]
        public ulong? Phone { get; set; }

        [Required(ErrorMessage = "Message is required")]
        [InputType(InputType.Text)]
        public string? Message { get; set; }

        public string GetValue() =>
            $"smsto:{Phone}:{Message}";
    }
}
