﻿using QRBarcodeMauiBlazorHybrid.Common.Attributes;
using QRBarcodeMauiBlazorHybrid.Common.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace QRBarcodeMauiBlazorHybrid.Models.Barcode
{
    public class I2OF5Model : IGenerateModel
    {
        [Required(ErrorMessage = "I2OF5 is required")]
        [RegularExpression(@"^\d+$", ErrorMessage = "I2OF5 must contain digits only")]
        [InputType(InputType.Number)]
        public string? I2OF5 { get; set; }

        public string GetValue() =>
            I2OF5 ?? string.Empty;
    }
}
