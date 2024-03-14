using QRBarcodeApp.Helpers;
using System.ComponentModel.DataAnnotations;

namespace QRBarcodeApp.Models.QR
{
    public class TextModel : IGenerateModel
    {
        [Required(ErrorMessage = "Text is required")]
        public string? Text { get; set; }

        public string GetValue()
        {
            return Text ?? "";
        }
    }
}
