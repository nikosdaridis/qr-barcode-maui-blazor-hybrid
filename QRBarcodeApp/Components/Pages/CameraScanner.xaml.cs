using BarcodeScanning;
using CommunityToolkit.Maui.Views;
using Microsoft.AspNetCore.Components;
using QRBarcodeApp.Models;
using QRBarcodeApp.Services;

namespace QRBarcodeApp.Components.Pages;

public partial class CameraScanner : Popup
{
    private readonly NavigationManager _navigationManager;
    private readonly LocalStorageService _localStorageService;

    private bool _scanned;
    public CameraScanner(NavigationManager navigationManager, LocalStorageService localStorageService)
    {
        _navigationManager = navigationManager;
        _localStorageService = localStorageService;
        InitializeComponent();

        Methods.AskForRequiredPermissionAsync();
        Scanner.CameraEnabled = true;
    }

    private async void CameraView_OnDetectionFinished(object sender, OnDetectionFinishedEventArg e)
    {
        if (!_scanned && e?.BarcodeResults?.FirstOrDefault() is not null)
        {
            _scanned = true;

            QRModel? qrExists = await _localStorageService.GetQRByValueAsync(e.BarcodeResults.First().RawValue);
            await CloseAsync();

            if (qrExists is null)
            {
                string id = await _localStorageService.SaveQRAsync(e.BarcodeResults.First(), "Scanned");
                _navigationManager.NavigateTo($"/scanresult/{id}");
            }
            else
                _navigationManager.NavigateTo($"/scanresult/{qrExists.Id}");
        }
    }

    private void CameraButton_Clicked(object sender, EventArgs e)
    {
        if (Scanner.CameraFacing == CameraFacing.Back)
            Scanner.CameraFacing = CameraFacing.Front;
        else
            Scanner.CameraFacing = CameraFacing.Back;
    }

    private void TorchButton_Clicked(object sender, EventArgs e)
    {
        if (Scanner.TorchOn)
            Scanner.TorchOn = false;
        else
            Scanner.TorchOn = true;
    }
}
