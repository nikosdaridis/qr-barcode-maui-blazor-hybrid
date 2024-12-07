using QRBarcodeMauiBlazorHybrid.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace QRBarcodeMauiBlazorHybrid.Models.QR
{
    public class EmailModel : IGenerateModel
    {
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid Email")]
        public string? Email { get; set; }

        public string GetValue() =>
            $"mailto:{Email}";
    }
}
