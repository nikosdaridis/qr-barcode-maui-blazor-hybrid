using QRBarcodeMauiBlazorHybrid.Common.Attributes;
using QRBarcodeMauiBlazorHybrid.Common.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace QRBarcodeMauiBlazorHybrid.Models.QR
{
    public class WiFiModel : IGenerateModel
    {
        [Required(ErrorMessage = "EncryptionType is required")]
        [InputType(InputType.Text)]
        [DropdownOptions("WPA/WPA2", "WEP")]
        public string EncryptionType { get; set; } = "WPA/WPA2";

        [Required(ErrorMessage = "SSID is required")]
        [InputType(InputType.Text)]
        public string? SSID { get; set; }

        [InputType(InputType.Text)]
        public string? Password { get; set; }

        public string GetValue() =>
            $"WIFI:T:{(EncryptionType == "WPA/WPA2" ? "WPA" : "WEP")};S:{SSID};P:{Password};;";
    }
}
