using QRBarcodeMauiBlazorHybrid.Common.Attributes;
using QRBarcodeMauiBlazorHybrid.Common.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace QRBarcodeMauiBlazorHybrid.Models.QR;

public sealed class WiFiModel : IGenerateModel
{
    [Required(ErrorMessage = "Encryption is required")]
    public WiFiEncryptionType Encryption { get; set; } = WiFiEncryptionType.WpaWpa2;

    [Required(ErrorMessage = "SSID is required")]
    [InputType(InputType.Text)]
    public string? SSID { get; set; }

    [InputType(InputType.Text)]
    public string? Password { get; set; }

    public string GetValue() =>
        $"WIFI:T:{EncryptionToken(Encryption)};S:{SSID};P:{Password};;";

    private static string EncryptionToken(WiFiEncryptionType encryption) => encryption switch
    {
        WiFiEncryptionType.WpaWpa2 => "WPA",
        WiFiEncryptionType.Wep => "WEP",
        _ => throw new ArgumentOutOfRangeException(nameof(encryption), $"Not expected encryption value: {encryption}")
    };
}
