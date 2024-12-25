using QRBarcodeMauiBlazorHybrid.Common.Attributes;
using QRBarcodeMauiBlazorHybrid.Common.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace QRBarcodeMauiBlazorHybrid.Models.QR
{
    public class GeographicCoordinatesModel : IGenerateModel
    {
        [Required(ErrorMessage = "Latitude is required")]
        [RegularExpression(@"^([1-8]?\d(\.\d+)?|90(\.0+)?)$", ErrorMessage = "Invalid Latitude")]
        [InputType(InputType.Number)]
        public double? Latitude { get; set; } = null;

        [Required(ErrorMessage = "Longitude is required")]
        [RegularExpression(@"^((\d{1,2}|1[0-7]\d|180)(\.\d+)?)$", ErrorMessage = "Invalid Longitude")]
        [InputType(InputType.Number)]
        public double? Longitude { get; set; } = null;

        public string GetValue() =>
            $"geo:{Latitude},{Longitude}";
    }
}
