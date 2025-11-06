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

            ScanbotSdkMain.Initialize(builder, LicenseKey, new ScanbotSdkConfiguration
            {
                EnableLogging = true,
                ErrorHandler = (status, feature, message) =>
                {
                    Console.WriteLine($"License error: {status}, {feature}, {message}");
                }
            });

            return builder.Build();
        }
    }
}