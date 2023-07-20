using BarcodeSDK.MAUI.Models;

namespace BarcodeSDK.MAUI.Example {
    /// <summary>
    /// Application class
    /// </summary>
    public partial class App : Application
    {
        private const string LicenseKey = null;

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

