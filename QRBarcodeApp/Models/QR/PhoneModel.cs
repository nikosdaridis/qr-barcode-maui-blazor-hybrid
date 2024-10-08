﻿using QRBarcodeApp.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace QRBarcodeApp.Models.QR
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
