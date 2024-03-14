using QRBarcodeApp.Helpers;
using System.ComponentModel.DataAnnotations;

namespace QRBarcodeApp.Models.QR
{
    public class UrlModel : IGenerateModel
    {
        [Required(ErrorMessage = "Url is required")]
        [RegularExpression(@"^(?i)(?:(?:http|https):\/\/|www\.)?[\w-]+(?:\.[\w-]+)+", ErrorMessage = "Invalid Url")]
        public string? Url { get; set; }

        public string GetValue()
        {
            return Url ?? "";
        }
    }
}
