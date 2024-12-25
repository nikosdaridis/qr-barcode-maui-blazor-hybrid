using QRBarcodeMauiBlazorHybrid.Common.Attributes;
using QRBarcodeMauiBlazorHybrid.Common.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace QRBarcodeMauiBlazorHybrid.Models.Barcode
{
    public class UpcaModel : IGenerateModel
    {
        [Required(ErrorMessage = "Upca is required")]
        [RegularExpression(@"^\d{12}$", ErrorMessage = "Upca must be 12 digits")]
        [InputType(InputType.Number)]
        public string? Upca { get; set; } = null;

        public string GetValue() =>
            Upca ?? string.Empty;
    }
}
