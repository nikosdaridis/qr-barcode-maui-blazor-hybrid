using BarcodeStandard;
using CommunityToolkit.Maui.Alerts;
using Microsoft.AspNetCore.Components;
using NativeMedia;
using QRBarcodeMauiBlazorHybrid.Common.CodeFormats;
using QRBarcodeMauiBlazorHybrid.Models;
using QRCoder;
using SkiaSharp;
using System.Diagnostics;
using BarcodeType = BarcodeStandard.Type;

namespace QRBarcodeMauiBlazorHybrid.Services;

public sealed class CodeService(LocalStorageService localStorageService, NavigationManager navigationManager)
{
    private const int QR_PIXELS_PER_MODULE = 20;
    private const int BARCODE_MIN_WIDTH_PX = 300;
    private const int BARCODE_WIDTH_PX_PER_CHAR = 16;
    private const int BARCODE_MIN_HEIGHT_PX = 200;
    private const int BARCODE_MAX_HEIGHT_PX = 500;
    private const int BARCODE_HEIGHT_PX_PER_CHAR = 8;
    private const int PNG_QUALITY = 100;

    /// <summary>
    /// Generates QR and Barcode byte array.
    /// </summary>
    public static byte[] GenerateCodeBytes(CodeModel code)
    {
        if (string.IsNullOrWhiteSpace(code.Value))
        {
            return [];
        }

        BarcodeType barcodeType = BarcodeFormats.Get(code.Format)?.BarcodeType ?? BarcodeType.Unspecified;

        try
        {
            return barcodeType == BarcodeType.Unspecified
                ? GenerateQrCodeBytes(code.Value)
                : GenerateBarcodeBytes(barcodeType, code.Value);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"{nameof(GenerateCodeBytes)} failed: {ex}");
            return [];
        }

        static byte[] GenerateQrCodeBytes(string value)
        {
            QRCodeGenerator qrGenerator = new();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(value, QRCodeGenerator.ECCLevel.L);
            PngByteQRCode qrCode = new(qrCodeData);
            return qrCode.GetGraphic(QR_PIXELS_PER_MODULE);
        }

        static byte[] GenerateBarcodeBytes(BarcodeType barcodeType, string value)
        {
            Barcode barcode = new() { IncludeLabel = true };
            int width = Math.Max(BARCODE_MIN_WIDTH_PX, value.Length * BARCODE_WIDTH_PX_PER_CHAR);
            int height = Math.Max(BARCODE_MIN_HEIGHT_PX, Math.Min(BARCODE_MAX_HEIGHT_PX, value.Length * BARCODE_HEIGHT_PX_PER_CHAR));
            SKImage barcodeImage = barcode.Encode(barcodeType, value, width, height);
            return barcodeImage.Encode(SKEncodedImageFormat.Png, PNG_QUALITY).ToArray();
        }
    }

    /// <summary>
    /// Saves code image to the gallery and displays toast notification.
    /// </summary>
    public static async Task SaveCodeToGalleryAsync(byte[] codeBytes, string format)
    {
        if (codeBytes.Length == 0)
        {
            return;
        }

        await MediaGallery.SaveAsync(MediaFileType.Image, codeBytes, $"{format}.png");
        await Toast.Make($"Saved {format} to Gallery").Show();
    }

    /// <summary>
    /// Shares code image by invoking the share dialog.
    /// </summary>
    public static async Task ShareCodeAsync(byte[] codeBytes, string format)
    {
        if (codeBytes.Length == 0)
        {
            return;
        }

        string filePath = Path.Combine(FileSystem.CacheDirectory, $"{format}.png");
        await File.WriteAllBytesAsync(filePath, codeBytes);

        await Share.Default.RequestAsync(new ShareFileRequest
        {
            Title = $"Share {format}",
            File = new ShareFile(filePath)
        });
    }

    /// <summary>
    /// Deletes code, displays toast notification and navigates to active tab.
    /// </summary>
    public async Task DeleteCodeAsync(string id, CodeModel code)
    {
        if (!await localStorageService.DeleteCodeAsync(id))
        {
            return;
        }

        await Toast.Make($"Deleted {code.Format} from History").Show();
        navigationManager.NavigateTo(await localStorageService.GetActiveTabAsync());
    }

    /// <summary>
    /// Converts code bytes to a Base64 png image.
    /// </summary>
    public static string? GetCodeBase64PngImage(byte[] codeBytes) =>
        codeBytes.Length == 0
            ? null
            : $"data:image/png;base64,{Convert.ToBase64String(codeBytes)}";
}
