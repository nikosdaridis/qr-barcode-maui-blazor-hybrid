using BarcodeScanning;
using Microsoft.AspNetCore.Components;

namespace QRBarcodeApp.Services
{
    public class CameraScannerService
    {
        private readonly NavigationManager _navigationManager;
        public readonly LocalStorageService _localStorageService;
        public bool VibrationOnDetected = true;

        public CameraScannerService(NavigationManager navigationManager, LocalStorageService localStorageService)
        {
            _navigationManager = navigationManager;
            _localStorageService = localStorageService;
        }

        public async Task ScanFinished(BarcodeResult scanResult)
        {
            string id = await _localStorageService.SaveQRAsync(scanResult);
            _navigationManager.NavigateTo($"/ScanResult/{id}");
        }
    }
}
