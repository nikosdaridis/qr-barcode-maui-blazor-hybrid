using QRBarcodeMauiBlazorHybrid.Common.Attributes;
using QRBarcodeMauiBlazorHybrid.Common.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace QRBarcodeMauiBlazorHybrid.Models.QR
{
    public class PhoneModel : IGenerateModel
    {
        [Required(ErrorMessage = "Phone is required")]
        [RegularExpression("^\\d+$", ErrorMessage = "Invalid Phone")]
        [InputType(InputType.Number)]
        public ulong? Phone { get; set; }

        public string GetValue() =>
            $"tel:{Phone}";
    }
}
