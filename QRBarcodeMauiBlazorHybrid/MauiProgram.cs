using BarcodeScanning;
using Blazored.LocalStorage;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using QRBarcodeMauiBlazorHybrid.Services;

namespace QRBarcodeMauiBlazorHybrid;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        MauiAppBuilder builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("Poppins-Regular.ttf", "PoppinsRegular");
            })
            .UseMauiCommunityToolkit(options =>
            {
                options.SetPopupOptionsDefaults(new()
                {
                    Shape = null,
                    Shadow = null,
                    PageOverlayColor = Colors.Transparent,
                });

                options.SetPopupDefaults(new()
                {
                    HorizontalOptions = LayoutOptions.Fill,
                    VerticalOptions = LayoutOptions.Fill,
                    Margin = new(0),
                    Padding = new(0),
                });
            })
            .UseBarcodeScanning();

        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddBlazoredLocalStorage();
        builder.Services.AddScoped<LocalStorageService>();
        builder.Services.AddScoped<CodeService>();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
