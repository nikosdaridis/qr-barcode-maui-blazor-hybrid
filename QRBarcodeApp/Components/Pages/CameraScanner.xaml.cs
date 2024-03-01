using BarcodeScanning;
using Microsoft.AspNetCore.Components;
using QRBarcodeApp.Services;

namespace QRBarcodeApp.Components.Pages;

public partial class CameraScanner : ContentPage
{
    private readonly NavigationManager _navigationManager;
    private readonly LocalStorageService _localStorageService;
    private bool _scanned;

    public CameraScanner(NavigationManager navigationManager, LocalStorageService localStorageService)
    {
        _navigationManager = navigationManager;
        _localStorageService = localStorageService;
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        await Methods.AskForRequiredPermissionAsync();
        base.OnAppearing();

        _scanned = false;
        Scanner.CameraEnabled = true;
    }

    protected override bool OnBackButtonPressed()
    {
        return true;
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        Scanner.CameraEnabled = false;
    }

    private async void CameraView_OnDetectionFinished(object sender, OnDetectionFinishedEventArg e)
    {

        if (Navigation.ModalStack.Any())
        {
            if (!_scanned && e?.BarcodeResults?.FirstOrDefault() is not null)
            {
                _scanned = true;
                await Navigation.PopModalAsync(false);
                string id = await _localStorageService.SaveQRAsync(e.BarcodeResults.First());
                _navigationManager.NavigateTo($"/scanresult/{id}");
            }
        }
    }

    private async void BackButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync(false);

        string activeTab = await _localStorageService.GetActiveTab();
        if (activeTab is "/scan")
            _navigationManager.NavigateTo("/history");
        else
            _navigationManager.NavigateTo(await _localStorageService.GetActiveTab());
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