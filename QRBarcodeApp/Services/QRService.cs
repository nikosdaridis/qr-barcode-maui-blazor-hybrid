using BarcodeScanning;
using CommunityToolkit.Maui.Alerts;
using NativeMedia;
using QRBarcodeApp.Models;
using QRCoder;

namespace QRBarcodeApp.Services
{
    public class QRService
    {
        private readonly LocalStorageService _localStorageService;

        public QRService(LocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public byte[] GenerateQRBytes(string? qrText)
        {
            if (string.IsNullOrWhiteSpace(qrText))
                return [];

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrText, QRCodeGenerator.ECCLevel.L);
            PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);

            return qrCode.GetGraphic(20);
        }

        public async Task<QRModel?> ToggleQRFavoriteAsync(string? id, QRModel updatedQR)
        {
            if (string.IsNullOrWhiteSpace(id))
                return null;

            return await _localStorageService.UpdateQRAsync(id, updatedQR);
        }

        public async Task SaveQRToGalleryAsync(byte[] qrBytes)
        {
            if (!qrBytes.Any())
                return;

            await MediaGallery.SaveAsync(MediaFileType.Image, qrBytes, "QR.png");
            await Toast.Make("Saved to Gallery").Show();
        }

        public async Task ShareQRAsync(byte[] qrBytes)
        {
            if (!qrBytes.Any())
                return;

            await File.WriteAllBytesAsync(Path.Combine(FileSystem.Current.CacheDirectory, "QR.png"), qrBytes);
            string file = Path.Combine(FileSystem.CacheDirectory, "QR.png");

            await Share.Default.RequestAsync(new ShareFileRequest
            {
                Title = "Share QR",
                File = new ShareFile(file)
            });
        }

        public async Task<bool> SaveQRToHistoryAsync(string? qrText)
        {
            if (string.IsNullOrWhiteSpace(qrText))
                return false;

            BarcodeResult qr = new BarcodeResult() { RawValue = qrText, BarcodeType = BarcodeTypes.Text, BarcodeFormat = BarcodeFormats.QRCode };

            await _localStorageService.SaveQRAsync(qr, "Generated");
            await Toast.Make("Saved to History").Show();

            return true;
        }

        public async Task<bool> QRExistsInHistoryAsync(string? qrText)
        {
            if (string.IsNullOrWhiteSpace(qrText))
                return false;

            List<QRModel> qrHistory = await _localStorageService.GetQRAllAsync();

            return qrHistory.Any(qr => qr.Value == qrText);
        }

        public string GetQRBase64(byte[] qrBytes)
        {
            if (!qrBytes.Any())
                return "";

            return $"data:image/png;base64,{Convert.ToBase64String(qrBytes)}";
        }
    }
}
