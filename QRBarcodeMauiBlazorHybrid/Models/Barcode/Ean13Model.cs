﻿using QRBarcodeMauiBlazorHybrid.Common.Attributes;
using QRBarcodeMauiBlazorHybrid.Common.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace QRBarcodeMauiBlazorHybrid.Models.Barcode
{
    public class Ean13Model : IGenerateModel
    {
        [Required(ErrorMessage = "Ean13 is required")]
        [RegularExpression(@"^\d{13}$", ErrorMessage = "Ean13 must be 13 digits")]
        [InputType(InputType.Number)]
        public string? Ean13 { get; set; } = null;

        public string GetValue() =>
            Ean13 ?? string.Empty;
    }
}
