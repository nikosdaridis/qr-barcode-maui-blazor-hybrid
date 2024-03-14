using BarcodeScanning;
using Blazored.LocalStorage;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using QRBarcodeApp.Services;

namespace QRBarcodeApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            MauiAppBuilder? builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("Poppins-Regular.ttf", "PoppinsRegular");
                })
                .UseMauiCommunityToolkit()
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
}
