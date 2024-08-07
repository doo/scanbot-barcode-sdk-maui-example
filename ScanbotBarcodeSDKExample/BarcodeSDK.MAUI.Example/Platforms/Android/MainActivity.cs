using Android.App;
using Android.OS;

namespace ScanbotSDK.MAUI.Example
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = Android.Content.PM.LaunchMode.SingleTop)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            DependencyManager.RegisterActivity(this);
        }
    }
}
