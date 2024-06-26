using Android.Runtime;
using IO.Scanbot.Sdk.Barcode_scanner;

namespace BarcodeSDK.NET.Droid
{
    [Application(LargeHeap = true)]
    public class MainApplication : Application
    {
        // Without a license key, the Scanbot Barcode SDK will work for 1 minute.
        // To scan longer, register for a trial license key here: https://scanbot.io/trial/
        private const string LicenseKey = "";

        public MainApplication(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        { }

        public override void OnCreate()
        {
            base.OnCreate();

            var initializer = new ScanbotBarcodeScannerSDKInitializer();
            initializer.WithLogging(true, false);
            initializer.License(this, LicenseKey);
            initializer.Initialize(this);
        }
    }
}
