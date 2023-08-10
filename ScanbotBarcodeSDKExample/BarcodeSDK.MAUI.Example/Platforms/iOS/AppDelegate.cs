using BarcodeSDK.MAUI.iOS.Services;
using Foundation;
using UIKit;

namespace BarcodeSDK.MAUI.Example;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
    protected override MauiApp CreateMauiApp() => CreateMAuiApplication();

    private MauiApp CreateMAuiApplication()
    {
        DependencyManager.Register();
        return MauiProgram.CreateMauiApp();
    }


    /// <summary>
    /// Show message on top of the Root window
    /// </summary>
    /// <param name="message"></param>
    /// <param name="buttonTitle"></param>
    internal void ShowAlert(string message, string buttonTitle)
    {
        var alert = UIAlertController.Create("Alert", message, UIAlertControllerStyle.Alert);
        var action = UIAlertAction.Create(buttonTitle ?? "Ok", UIAlertActionStyle.Cancel, (obj) => { });
        alert.AddAction(action);
        Window?.RootViewController?.PresentViewController(alert, true, null);
    }

    /// <summary>
    /// Extract ViewController from the Application's ViewController Hierarchy.
    /// </summary>
    /// <returns></returns>
    internal static UIViewController ExtractViewController(UIWindow window)
    {
        var viewController = window?.RootViewController;

        if (viewController == null) return null;

        // If application has a Navigation Controller
        if (viewController is UINavigationController navigationController)
        {
            // Note: The Navigation Controller has a Navigation Renderer in between the NavigationController and MainViewController, so we cannot use "navigationController?.VisibleViewController".
            return navigationController?.VisibleViewController?.ChildViewControllers?.Last();
        }
        else if (viewController is UITabBarController tabBarController)
        {
            // It is itself a Page renderer.
            return tabBarController.SelectedViewController;
        }
        else
        {   // If application has no Navigation Controller OR TabBarController
            return viewController;
        }
    }
}

