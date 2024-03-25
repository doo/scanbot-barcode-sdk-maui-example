using ClassicComponent.MAUI.Legacy.ClassicComponent;
using ScanbotSDK.MAUI;
using ScanbotSDK.MAUI.Models;

namespace ClassicComponent.MAUI.Legacy;

public static class MauiProgram
{
    public const string LicenseKey = "";
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            }).ConfigureMauiHandlers(handlers =>
            {
                handlers.AddHandler(typeof(BarcodeCameraView), typeof(BarcodeCameraViewHandler));
            });

        ScanbotSDKInitialize(builder);

        return builder.Build();
    }

    private static void ScanbotSDKInitialize(MauiAppBuilder mauiApp)
    {
        ScanbotSDK.MAUI.ScanbotBarcodeSDK.Initialize(mauiApp, new InitializationOptions
        {
            LicenseKey = LicenseKey,
            LoggingEnabled = true,
            ErrorHandler = (status, feature) =>
            {
                Console.WriteLine($"License error: {status}, {feature}");
            }
        });
    }
}