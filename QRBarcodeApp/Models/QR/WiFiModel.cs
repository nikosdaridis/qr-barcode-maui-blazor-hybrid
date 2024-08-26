using QRBarcodeApp.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace QRBarcodeApp.Models.QR
{
    public class WiFiModel : IGenerateModel
    {
        [Required(ErrorMessage = "EncryptionType is required")]
        public string EncryptionType { get; set; } = "WPA/WPA2";

        [Required(ErrorMessage = "SSID is required")]
        public string? SSID { get; set; }

        public string? Password { get; set; }

        public string GetValue() =>
            $"WIFI:T:{(EncryptionType == "WPA/WPA2" ? "WPA" : "WEP")};S:{SSID};P:{Password};;";
    }
}
