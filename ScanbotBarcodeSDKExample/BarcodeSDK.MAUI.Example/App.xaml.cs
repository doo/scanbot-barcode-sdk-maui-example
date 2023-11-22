using ScanbotSDK.MAUI.Models;

namespace ScanbotSDK.MAUI.Example
{
    public partial class App : Application
    {
        // Without a license key, the Scanbot Barcode SDK will work for 1 minute.
        // To scan longer, register for a trial license key here: https://scanbot.io/trial/
        internal const string LicenseKey = "";

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Pages.HomePage());

            ScanbotBarcodeSDK.Initialize(new InitializationOptions
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
}