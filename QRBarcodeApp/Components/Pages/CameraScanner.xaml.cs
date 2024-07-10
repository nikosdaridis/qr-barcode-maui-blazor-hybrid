using BarcodeScanning;
using CommunityToolkit.Maui.Views;
using Microsoft.AspNetCore.Components;
using QRBarcodeApp.Services;

namespace QRBarcodeApp.Components.Pages;

public sealed partial class CameraScanner : Popup
{
    private readonly NavigationManager _navigationManager;
    private readonly LocalStorageService _localStorageService;
    private bool _scanned;

    /// <summary>
    /// Sets up camera scanner popup, asks for camera permissions and enables camera
    /// </summary>
    public CameraScanner(NavigationManager navigationManager, LocalStorageService localStorageService)
    {
        _navigationManager = navigationManager;
        _localStorageService = localStorageService;
        InitializeComponent();

        Methods.AskForRequiredPermissionAsync();
        Scanner.CameraEnabled = true;
    }

    /// <summary>
    /// Handles QR and Barcode detection, saves scanned code and navigates to details tab
    /// </summary>
    private async void CameraViewOnDetectionFinished(object sender, OnDetectionFinishedEventArg e)
    {
        if (e?.BarcodeResults?.FirstOrDefault() is null || _scanned)
            return;

        _scanned = true;
        await CloseAsync();

        string id = await _localStorageService.SaveCodeAsync(e.BarcodeResults.First(), "Scanned");
        _navigationManager.NavigateTo($"/details/{id}");
    }

    /// <summary>
    /// Invokes camera click handler
    /// </summary>
    private void HandleFrameCameraClick(object sender, EventArgs e) =>
        HandleCameraClick(CameraButton, e);

    /// <summary>
    /// Invokes torch click handler 
    /// </summary>
    private void HandleFrameTorchClick(object sender, EventArgs e) =>
        HandleTorchClick(TorchButton, e);

    /// <summary>
    /// Toggles camera facing direction
    /// </summary>
    private void HandleCameraClick(object sender, EventArgs e)
    {
        if (CameraButton is null)
            return;

        ToggleImageSource(CameraButton, "rotate.svg", "rotate_active.svg");

        if (Scanner.CameraFacing == CameraFacing.Back)
        {
            Scanner.CameraFacing = CameraFacing.Front;

            if (Scanner.TorchOn)
            {
                ToggleImageSource(TorchButton, "flash.svg", "flash_active.svg");
                Scanner.TorchOn = false;
            }
        }
        else
            Scanner.CameraFacing = CameraFacing.Back;
    }

    /// <summary>
    /// Toggles flashlight if not front camera
    /// </summary>
    private void HandleTorchClick(object sender, EventArgs e)
    {
        if (TorchButton is null || Scanner.CameraFacing == CameraFacing.Front)
            return;

        ToggleImageSource(TorchButton, "flash.svg", "flash_active.svg");

        Scanner.TorchOn = !Scanner.TorchOn;
    }

    /// <summary>
    /// Toggles button source image between default and active state
    /// </summary>
    private void ToggleImageSource(ImageButton button, string defaultImage, string activeImage)
    {
        if (string.IsNullOrWhiteSpace(button?.Source?.ToString()))
            return;

        button.Source = button.Source.ToString()!.EndsWith(defaultImage) ? activeImage : defaultImage;
    }
}
