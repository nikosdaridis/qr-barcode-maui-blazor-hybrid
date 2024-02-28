using BarcodeScanning;
using QRBarcodeApp.Services;

namespace QRBarcodeApp.Components.Pages;

public partial class CameraScanner : ContentPage
{
    private readonly CameraScannerService _cameraScanService;
    private bool _scanned;

    public CameraScanner(CameraScannerService cameraScanService)
    {
        _cameraScanService = cameraScanService;
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        await Methods.AskForRequiredPermissionAsync();
        base.OnAppearing();

        _scanned = false;
        Scanner.CameraEnabled = true;
        Scanner.VibrationOnDetected = _cameraScanService.VibrationOnDetected;
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
        if (!_scanned && e?.BarcodeResults?.FirstOrDefault() is not null)
        {
            _scanned = true;
            await Navigation.PopModalAsync();
            await _cameraScanService.ScanFinished(e.BarcodeResults.First());
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