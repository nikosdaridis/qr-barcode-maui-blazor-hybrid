using BarcodeScanning;
using CommunityToolkit.Maui.Alerts;
using Microsoft.AspNetCore.Components;
using NativeMedia;
using QRBarcodeApp.Models;
using QRCoder;
using System.Text.RegularExpressions;

namespace QRBarcodeApp.Services
{
    public class QRService
    {
        private readonly LocalStorageService _localStorageService;
        private readonly NavigationManager _navigationManager;

        public QRService(LocalStorageService localStorageService, NavigationManager navigationManager)
        {
            _localStorageService = localStorageService;
            _navigationManager = navigationManager;
        }

        public byte[] GenerateQRBytes(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return [];

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(value, QRCodeGenerator.ECCLevel.L);
            PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);

            return qrCode.GetGraphic(20);
        }

        public async Task<QRModel?> ToggleQRFavoriteAsync(string? id, QRModel updatedQR)
        {
            if (string.IsNullOrWhiteSpace(id))
                return null;

            return await _localStorageService.UpdateQRAsync(id, updatedQR);
        }

        public async Task SaveQRToGalleryAsync(byte[] qrBytes, string? format)
        {
            if (!qrBytes.Any())
                return;

            await MediaGallery.SaveAsync(MediaFileType.Image, qrBytes, $"{format}.png");
            await Toast.Make($"Saved {format} to Gallery").Show();
        }

        public async Task ShareQRAsync(byte[] qrBytes, string? format)
        {
            if (!qrBytes.Any())
                return;

            await File.WriteAllBytesAsync(Path.Combine(FileSystem.Current.CacheDirectory, $"{format}.png"), qrBytes);
            string file = Path.Combine(FileSystem.CacheDirectory, $"{format}.png");

            await Share.Default.RequestAsync(new ShareFileRequest
            {
                Title = $"Share {format}",
                File = new ShareFile(file)
            });
        }

        public async Task<string?> SaveQRToHistoryAsync(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;

            BarcodeResult qr = new BarcodeResult() { RawValue = value, BarcodeType = GetQRType(value), BarcodeFormat = BarcodeFormats.QRCode };

            string id = await _localStorageService.SaveQRAsync(qr, "Generated");
            await Toast.Make($"Saved {qr.BarcodeFormat} to History").Show();

            return id;
        }

        public async Task DeleteQRFromHistoryAsync(QRModel? qrToDelete)
        {
            if (qrToDelete is null)
                return;

            if (!await _localStorageService.DeleteQRAsync(qrToDelete.Id))
                return;

            await Toast.Make($"Deleted {qrToDelete.Format} from History").Show();

            _navigationManager.NavigateTo(await _localStorageService.GetActiveTabAsync());
        }

        public async Task<bool> QRExistsInHistoryAsync(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;

            List<QRModel> qrHistory = await _localStorageService.GetQRAllAsync();

            return qrHistory.Any(qr => qr.Value == value);
        }

        public string GetQRBase64(byte[] qrBytes)
        {
            if (!qrBytes.Any())
                return "";

            return $"data:image/png;base64,{Convert.ToBase64String(qrBytes)}";
        }

        public BarcodeTypes GetQRType(string? qrText)
        {
            if (string.IsNullOrWhiteSpace(qrText))
                return BarcodeTypes.Unknown;

            if (qrText.StartsWith("BEGIN:VEVENT", StringComparison.OrdinalIgnoreCase))
                return BarcodeTypes.CalendarEvent;

            if (qrText.StartsWith("BEGIN:VCARD", StringComparison.OrdinalIgnoreCase))
                return BarcodeTypes.ContactInfo;

            if (qrText.StartsWith("WIFI:", StringComparison.OrdinalIgnoreCase))
                return BarcodeTypes.WiFi;

            if (qrText.StartsWith("SMSTO:", StringComparison.OrdinalIgnoreCase))
                return BarcodeTypes.Sms;

            if (Regex.IsMatch(qrText, @"^geo:\d{1,}\.\d{1,},\d{1,}\.\d{1,}"))
                return BarcodeTypes.GeographicCoordinates;

            if (Regex.IsMatch(qrText, @"^((?:http|https):\/\/|www\.)[\w-]+(?:\.[\w-]+)+", RegexOptions.IgnoreCase))
                return BarcodeTypes.Url;

            if (Regex.IsMatch(qrText, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
                return BarcodeTypes.Email;

            if (Regex.IsMatch(qrText, @"^\+?\d{10,}$"))
                return BarcodeTypes.Phone;

            return BarcodeTypes.Text;
        }
    }
}
