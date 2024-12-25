using QRBarcodeMauiBlazorHybrid.Common.Attributes;
using QRBarcodeMauiBlazorHybrid.Common.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace QRBarcodeMauiBlazorHybrid.Models.Barcode
{
    public class ItfModel : IGenerateModel
    {
        [Required(ErrorMessage = "Itf is required")]
        [RegularExpression(@"^(\d{2})+$", ErrorMessage = "Itf must contain an even number of digits")]
        [InputType(InputType.Number)]
        public string? Itf { get; set; }

        public string GetValue() =>
            Itf ?? string.Empty;
    }
}
