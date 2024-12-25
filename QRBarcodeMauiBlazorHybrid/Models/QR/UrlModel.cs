using QRBarcodeMauiBlazorHybrid.Common.Attributes;
using QRBarcodeMauiBlazorHybrid.Common.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace QRBarcodeMauiBlazorHybrid.Models.QR
{
    public class UrlModel : IGenerateModel
    {
        [Required(ErrorMessage = "Url is required")]
        [RegularExpression(@"^(?i)(?:(?:http|https):\/\/|www\.)?[\w-]+(?:\.[\w-]+)+", ErrorMessage = "Invalid Url")]
        [InputType(InputType.Text)]
        public string? Url { get; set; }

        public string GetValue() =>
            Url ?? string.Empty;
    }
}
