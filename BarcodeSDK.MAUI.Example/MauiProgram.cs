using CommunityToolkit.Maui;

namespace ScanbotSDK.MAUI.Example
{
    public static class MauiProgram
    {
        // Without a license key, the Scanbot Barcode SDK will work for 1 minute.
        // To scan longer, register for a trial license key here: https://scanbot.io/trial/
        public const string LicenseKey = "";

        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            SBSDKInitializer.Initialize(builder, LicenseKey, new SBSDKConfiguration
            {
                EnableLogging = true,
                ErrorHandler = (status, feature) =>
                {
                    Console.WriteLine($"License error: {status}, {feature}");
                }
            });

            return builder.Build();
        }
    }
}