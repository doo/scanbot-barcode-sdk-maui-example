using Android;
using Android.App;
using Android.Content.PM;
using Android.OS;
using AndroidX.Core.App;
using ScanbotSDK.MAUI.Services;

namespace ScanbotSDK.MAUI.Example
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            DependencyManager.RegisterActivity(this);

            ActivityCompat.RequestPermissions(this, new string[] {
                Manifest.Permission.Camera,
                Manifest.Permission.ReadExternalStorage,
                Manifest.Permission.WriteExternalStorage
        }, 0);
        }
    }
}
