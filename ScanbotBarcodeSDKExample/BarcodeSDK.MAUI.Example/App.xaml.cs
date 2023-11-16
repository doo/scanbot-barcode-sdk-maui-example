using ScanbotSDK.MAUI.Models;

namespace ScanbotSDK.MAUI.Example
{
    public partial class App : Application
    {
        public const string LicenseKey = null;

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Pages.HomePage());

            var options = new InitializationOptions
            {
                LicenseKey = LicenseKey,
                LoggingEnabled = true,
                ErrorHandler = (status, feature) =>
                {
                    Console.WriteLine($"License error: {status}, {feature}");
                }
            };
            ScanbotBarcodeSDK.Initialize(options);
        }
    }
}