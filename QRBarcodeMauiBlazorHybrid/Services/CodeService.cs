using BarcodeStandard;
using CommunityToolkit.Maui.Alerts;
using Microsoft.AspNetCore.Components;
using NativeMedia;
using QRBarcodeMauiBlazorHybrid.Models;
using QRCoder;
using SkiaSharp;
using BarcodeType = BarcodeStandard.Type;

namespace QRBarcodeMauiBlazorHybrid.Services
{
    public sealed class CodeService(LocalStorageService localStorageService, NavigationManager navigationManager)
    {
        /// <summary>
        /// Generates QR and Barcode byte array
        /// </summary>
        public static void GenerateCodeBytes(CodeModel code, ref byte[] codeBytes)
        {
            if (string.IsNullOrWhiteSpace(code?.Value))
                return;

            BarcodeType barcodeType = code.Format switch
            {
                "Code128" => BarcodeType.Code128,
                "Code39" => BarcodeType.Code39,
                "Code93" => BarcodeType.Code93,
                "CodaBar" => BarcodeType.Codabar,
                "Ean13" => BarcodeType.Ean13,
                "Ean8" => BarcodeType.Ean8,
                "Itf" => BarcodeType.Interleaved2Of5,
                "Upca" => BarcodeType.UpcA,
                "Upce" => BarcodeType.UpcE,
                "I2OF5" => BarcodeType.Industrial2Of5,
                _ => BarcodeType.Unspecified
            };

            try
            {
                codeBytes = barcodeType == BarcodeType.Unspecified
                    ? GenerateQrCodeBytes(code.Value)
                    : GenerateBarcodeBytes(barcodeType, code.Value);
            }
            catch
            {
                return;
            }

            static byte[] GenerateQrCodeBytes(string value)
            {
                QRCodeGenerator qrGenerator = new();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(value, QRCodeGenerator.ECCLevel.L);
                PngByteQRCode qrCode = new(qrCodeData);
                return qrCode.GetGraphic(20);
            }

            static byte[] GenerateBarcodeBytes(BarcodeType barcodeType, string value)
            {
                Barcode barcode = new() { IncludeLabel = true };
                int width = Math.Max(300, value.Length * 16);
                int height = Math.Max(200, Math.Min(500, value.Length * 8));
                SKImage barcodeImage = barcode.Encode(barcodeType, value, width, height);
                return barcodeImage.Encode(SKEncodedImageFormat.Png, 100).ToArray();
            }
        }

        /// <summary>
        /// Saves code image to the gallery and displays toast notification
        /// </summary>
        public static async Task SaveCodeToGalleryAsync(byte[] codeBytes, string format)
        {
            if (codeBytes.Length == 0)
                return;

            await MediaGallery.SaveAsync(MediaFileType.Image, codeBytes, $"{format}.png");
            await Toast.Make($"Saved {format} to Gallery").Show();
        }

        /// <summary>
        /// Shares code image by invoking the share dialog
        /// </summary>
        public static async Task ShareCodeAsync(byte[] codeBytes, string format)
        {
            if (codeBytes.Length == 0)
                return;

            string filePath = Path.Combine(FileSystem.CacheDirectory, $"{format}.png");
            await File.WriteAllBytesAsync(filePath, codeBytes);

            await Share.Default.RequestAsync(new ShareFileRequest
            {
                Title = $"Share {format}",
                File = new ShareFile(filePath)
            });
        }

        /// <summary>
        /// Deletes code, displays toast notification and navigates to active tab
        /// </summary>
        public async Task DeleteCodeAsync(string id, CodeModel code)
        {
            if (!await localStorageService.DeleteCodeAsync(id))
                return;

            await Toast.Make($"Deleted {code.Format} from History").Show();
            navigationManager.NavigateTo(await localStorageService.GetActiveTabAsync());
        }

        /// <summary>
        /// Converts code bytes to a Base64 png image
        /// </summary>
        public static void GetCodeBase64PngImage(byte[] codeBytes, ref string? imageBase64)
        {
            if (codeBytes.Length == 0)
                return;

            imageBase64 = $"data:image/png;base64,{Convert.ToBase64String(codeBytes)}";
        }
    }
}
