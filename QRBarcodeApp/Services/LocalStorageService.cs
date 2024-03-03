using BarcodeScanning;
using Blazored.LocalStorage;
using QRBarcodeApp.Models;

namespace QRBarcodeApp.Services
{
    public class LocalStorageService
    {
        private readonly ILocalStorageService _localStorageService;

        public LocalStorageService(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public async Task<QRModel?> GetQRAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return null;

            List<QRModel> qrHistory = await GetQRAllAsync();
            return qrHistory.FirstOrDefault(qr => qr.Id == id);
        }

        public async Task<List<QRModel>> GetQRAllAsync()
        {
            return await _localStorageService.GetItemAsync<List<QRModel>>("QRHistory") ?? [];
        }

        public async Task<string> SaveQRAsync(BarcodeResult scanResult)
        {
            List<QRModel> qrHistory = await GetQRAllAsync();

            string type = scanResult.BarcodeType.ToString().Split('.').Last();
            string format = scanResult.BarcodeFormat.ToString().Split('.').Last();
            QRModel? newQR = new QRModel { Value = scanResult.RawValue, Type = type, Format = format };
            qrHistory.Add(newQR);
            await _localStorageService.SetItemAsync("QRHistory", qrHistory);

            return newQR.Id;
        }

        public async Task<string> GetActiveTabAsync()
        {
            return await _localStorageService.GetItemAsync<string>("ActiveTab") ?? "";
        }

        public async Task SaveActiveTabAsync(string activeTab)
        {
            await _localStorageService.SetItemAsync("ActiveTab", activeTab);
        }
    }
}
