using Android.App;
using Android.Content.PM;
using Android.OS;
using ScanbotSDK.MAUI.Services;

namespace ClassicComponent.MAUI.Outdated;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        DependencyManager.RegisterActivity(this);
    }
}