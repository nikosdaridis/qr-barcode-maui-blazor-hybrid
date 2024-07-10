using BarcodeStandard;
using CommunityToolkit.Maui.Alerts;
using Microsoft.AspNetCore.Components;
using NativeMedia;
using QRBarcodeApp.Helpers;
using QRBarcodeApp.Models;
using QRCoder;
using SkiaSharp;
using BarcodeType = BarcodeStandard.Type;

namespace QRBarcodeApp.Services
{
    public sealed class CodeService(LocalStorageService localStorageService, NavigationManager navigationManager)
    {
        /// <summary>
        /// Generates QR and Barcode byte array
        /// </summary>
        public static void GenerateCodeBytes(CodeModel code, ref byte[] codeBytes)
        {
            BarcodeType barcodeType = BarcodeTypeMapper.ToBarcodeType(code.Format!);

            try
            {
                if (barcodeType == BarcodeType.Unspecified)
                {
                    QRCodeGenerator qrGenerator = new QRCodeGenerator();
                    QRCodeData qrCodeData = qrGenerator.CreateQrCode(code.Value, QRCodeGenerator.ECCLevel.L);
                    PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);

                    codeBytes = qrCode.GetGraphic(20);
                }
                else
                {
                    Barcode barcode = new Barcode();
                    barcode.IncludeLabel = true;
                    SKImage barcodeImage;
                    int width = Math.Max(300, code.Value!.Length * 16);
                    int height = Math.Max(200, Math.Min(500, code.Value.Length * 8));
                    barcodeImage = barcode.Encode(barcodeType, code.Value, width, height);

                    codeBytes = barcodeImage.Encode(SKEncodedImageFormat.Png, 100).ToArray();
                }
            }
            catch
            {
                return;
            }
        }

        /// <summary>
        /// Toggles code favorite property
        /// </summary>
        public async Task<CodeModel?> ToggleCodeFavoriteAsync(string id, CodeModel updatedCode) =>
            await localStorageService.UpdateCodeAsync(id, updatedCode);

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
