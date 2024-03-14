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
    public class CodeService(LocalStorageService localStorageService, NavigationManager navigationManager)
    {
        public static void GenerateCodeBytes(CodeModel code, ref byte[] codeBytes)
        {
            if (string.IsNullOrWhiteSpace(code.Value))
                return;

            BarcodeType codeType = BarcodeTypeMapper.MapScanFormatToBarcodeType(code.Format);

            try
            {
                if (codeType == BarcodeType.Unspecified)
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
                    barcodeImage = barcode.Encode(codeType, code.Value, Math.Max(400, code.Value.Length * 15), 400);

                    codeBytes = barcodeImage.Encode(SKEncodedImageFormat.Png, 100).ToArray();
                }
            }
            catch
            {
                return;
            }
        }

        public async Task<CodeModel?> ToggleCodeFavoriteAsync(string id, CodeModel updatedCode)
        {
            if (string.IsNullOrWhiteSpace(id))
                return null;

            return await localStorageService.UpdateCodeAsync(id, updatedCode);
        }

        public static async Task SaveCodeToGalleryAsync(byte[] codeBytes, string format)
        {
            if (codeBytes.Length == 0)
                return;

            await MediaGallery.SaveAsync(MediaFileType.Image, codeBytes, $"{format}.png");
            await Toast.Make($"Saved {format} to Gallery").Show();
        }

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

        public async Task DeleteCodeAsync(CodeModel codeToDelete)
        {
            if (!await localStorageService.DeleteCodeAsync(codeToDelete.Id))
                return;

            await Toast.Make($"Deleted {codeToDelete.Format} from History").Show();
            navigationManager.NavigateTo(await localStorageService.GetActiveTabAsync());
        }

        public static void GetCodeImageBase64(byte[] codeBytes, ref string? imageBase64)
        {
            if (codeBytes.Length == 0)
                return;

            imageBase64 = $"data:image/png;base64,{Convert.ToBase64String(codeBytes)}";
        }
    }
}
