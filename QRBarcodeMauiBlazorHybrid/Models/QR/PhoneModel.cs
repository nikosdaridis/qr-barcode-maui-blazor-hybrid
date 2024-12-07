using QRBarcodeMauiBlazorHybrid.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace QRBarcodeMauiBlazorHybrid.Models.QR
{
    public class PhoneModel : IGenerateModel
    {
        [Required(ErrorMessage = "Phone is required")]
        [RegularExpression("^\\d+$", ErrorMessage = "Invalid Phone")]
        public ulong? Phone { get; set; }

        public string GetValue() =>
            $"tel:{Phone}";
    }
}
