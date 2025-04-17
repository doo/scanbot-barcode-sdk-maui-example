using Android.App;
using Android.OS;
using Microsoft.Maui.Graphics.Platform;

namespace ScanbotSDK.MAUI.Example
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = Android.Content.PM.LaunchMode.SingleTop)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            Window.SetStatusBarColor(App.ScanbotColor.AsColor());
            
            base.OnCreate(savedInstanceState);
        }
    }
}
