using Android.Content;
using BarcodeSDK.NET.Droid.Activities;

namespace BarcodeSDK.NET.Droid
{
    public partial class MainActivity
    {
        private void OnBarcodeCameraXDemoClick(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this, SDK))
            {
                return;
            }
            var intent = new Intent(this, typeof(BarcodeClassicComponentActivity));
            intent.PutExtra("useCameraX", true);
            StartActivity(intent);
        }

        private void OnBarcodeCameraScanAndCountClick(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this, SDK))
            {
                return;
            }
            var intent = new Intent(this, typeof(BarcodeScanAndCountActivity));
            StartActivity(intent);
        }
    }
}