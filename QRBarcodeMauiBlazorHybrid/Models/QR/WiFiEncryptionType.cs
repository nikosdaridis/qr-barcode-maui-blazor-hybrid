using System.ComponentModel.DataAnnotations;

namespace QRBarcodeMauiBlazorHybrid.Models.QR;

/// <summary>
/// Wi-Fi network encryption modes supported by the QR Wi-Fi payload format.
/// </summary>
public enum WiFiEncryptionType
{
    [Display(Name = "WPA/WPA2")]
    WpaWpa2,

    [Display(Name = "WEP")]
    Wep
}
