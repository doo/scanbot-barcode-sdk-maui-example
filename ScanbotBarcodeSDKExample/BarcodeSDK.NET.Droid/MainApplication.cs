using Android.Runtime;
using IO.Scanbot.Sdk.Barcode_scanner;

namespace BarcodeSDK.NET.Droid
{
    [Application(LargeHeap = true)]
    public class MainApplication : Application
    {
        public MainApplication(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        { }

        public override void OnCreate()
        {
            base.OnCreate();

            var initializer = new ScanbotBarcodeScannerSDKInitializer();
            initializer.WithLogging(true, false);
            initializer.License(this, "");
            initializer.Initialize(this);
        }
    }
}
