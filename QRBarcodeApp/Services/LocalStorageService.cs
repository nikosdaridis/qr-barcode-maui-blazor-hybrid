using BarcodeScanning;
using Blazored.LocalStorage;
using QRBarcodeApp.Models;
using System.Reflection;

namespace QRBarcodeApp.Services
{
    public class LocalStorageService
    {
        private readonly ILocalStorageService _localStorageService;

        public LocalStorageService(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public async Task<QRModel?> GetQRByIdAsync(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return null;

            List<QRModel> qrHistory = await GetQRAllAsync();
            return qrHistory.FirstOrDefault(qr => qr.Id == id);
        }

        public async Task<QRModel?> GetQRByValueAsync(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;

            List<QRModel> qrHistory = await GetQRAllAsync();
            return qrHistory.FirstOrDefault(qr => qr.Value == value);
        }

        public async Task<List<QRModel>> GetQRAllAsync()
        {
            return await _localStorageService.GetItemAsync<List<QRModel>>("QRHistory") ?? [];
        }

        public async Task<string> SaveQRAsync(BarcodeResult qr, string source)
        {
            List<QRModel> qrHistory = await GetQRAllAsync();

            string type = qr.BarcodeType.ToString().Split('.').Last();
            string format = qr.BarcodeFormat.ToString().Split('.').Last();
            QRModel? newQR = new QRModel { Value = qr.RawValue, Type = type, Format = format, Source = source, Favorite = false };
            qrHistory.Add(newQR);
            await _localStorageService.SetItemAsync("QRHistory", qrHistory);

            return newQR.Id;
        }

        public async Task<QRModel?> UpdateQRAsync(string? id, QRModel updatedQR)
        {
            if (string.IsNullOrWhiteSpace(id))
                return null;

            List<QRModel> qrHistory = await GetQRAllAsync();
            QRModel? qrToUpdate = qrHistory.FirstOrDefault(qr => qr.Id == id);

            if (qrToUpdate is null)
                return null;

            foreach (PropertyInfo property in updatedQR.GetType().GetProperties())
            {
                if (property.Name == "Id" || property.Name == "DateTime")
                    continue;

                object? newValue = property.GetValue(updatedQR);

                if (newValue is not null)
                {
                    PropertyInfo? propertyToUpdate = qrToUpdate.GetType().GetProperty(property.Name);

                    propertyToUpdate?.SetValue(qrToUpdate, newValue);
                }
            }

            await _localStorageService.SetItemAsync("QRHistory", qrHistory);
            return qrToUpdate;
        }

        public async Task<bool> DeleteQRAsync(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return false;

            List<QRModel> qrHistory = await GetQRAllAsync();

            QRModel? qrToDelete = qrHistory.FirstOrDefault(qr => qr.Id == id);

            if (qrToDelete is null)
                return false;

            qrHistory.Remove(qrToDelete);
            await _localStorageService.SetItemAsync("QRHistory", qrHistory);

            return true;
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
