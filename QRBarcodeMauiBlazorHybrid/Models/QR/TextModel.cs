using QRBarcodeMauiBlazorHybrid.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace QRBarcodeMauiBlazorHybrid.Models.QR
{
    public class TextModel : IGenerateModel
    {
        [Required(ErrorMessage = "Text is required")]
        public string? Text { get; set; }

        public string GetValue() =>
            Text ?? string.Empty;
    }
}
