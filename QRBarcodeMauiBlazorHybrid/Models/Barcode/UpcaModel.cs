﻿using QRBarcodeMauiBlazorHybrid.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace QRBarcodeMauiBlazorHybrid.Models.Barcode
{
    public class UpcaModel : IGenerateModel
    {
        [Required(ErrorMessage = "Upca is required")]
        [RegularExpression(@"^\d{12}$", ErrorMessage = "Upca must be 12 digits")]
        public string? Upca { get; set; } = null;

        public string GetValue() =>
            Upca ?? string.Empty;
    }
}
