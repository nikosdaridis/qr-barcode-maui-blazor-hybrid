using QRBarcodeMauiBlazorHybrid.Common.Attributes;
using QRBarcodeMauiBlazorHybrid.Common.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace QRBarcodeMauiBlazorHybrid.Models.Barcode
{
    public class CodaBarModel : IGenerateModel
    {
        [Required(ErrorMessage = "CodaBar is required")]
        [RegularExpression(@"^[A-Da-d][0-9\-\$:\/\.\+]*[A-Da-d]$", ErrorMessage = "CodaBar must start and end with A, B, C, or D and may include digits and special characters (- $ : / . +) in between")]
        [InputType(InputType.Text)]
        public string? CodaBar { get; set; }

        public string GetValue() =>
            CodaBar ?? string.Empty;
    }
}
