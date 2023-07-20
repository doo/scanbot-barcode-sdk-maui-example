using Android.App;
using Android.Runtime;
using BarcodeSDK.MAUI.Droid.Services;

namespace BarcodeSDK.MAUI.Example;

[Application]
public class MainApplication : MauiApplication
{
	public MainApplication(IntPtr handle, JniHandleOwnership ownership)
		: base(handle, ownership)
	{
	}

    /// <summary>
    /// Get the Current activity instance.
    /// </summary>
    public static MauiAppCompatActivity CurrentActivity => (MauiAppCompatActivity)Platform.CurrentActivity;

    /// <summary>
    /// Initialisation of the MAUI Application
    /// </summary>
    /// <returns></returns>
    protected override MauiApp CreateMauiApp() => RegisterDependencies();

    /// <summary>
    /// Returns the instance of the MAUI Application.
    /// Also Initializes the Scanbot SDK.
    /// </summary>
    /// <returns></returns>
    private MauiApp RegisterDependencies()
    {
        DependencyManager.RegisterServices();
        return MauiProgram.CreateMauiApp();
    }
}

