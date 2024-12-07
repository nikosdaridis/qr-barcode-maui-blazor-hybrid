using QRBarcodeMauiBlazorHybrid.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace QRBarcodeMauiBlazorHybrid.Models.QR
{
    public class UrlModel : IGenerateModel
    {
        [Required(ErrorMessage = "Url is required")]
        [RegularExpression(@"^(?i)(?:(?:http|https):\/\/|www\.)?[\w-]+(?:\.[\w-]+)+", ErrorMessage = "Invalid Url")]
        public string? Url { get; set; }

        public string GetValue() =>
            Url ?? string.Empty;
    }
}
