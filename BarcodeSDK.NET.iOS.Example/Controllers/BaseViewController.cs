using System;

using UIKit;
using Foundation;
using ScanbotSDK.iOS;

namespace BarcodeSDK.NET.iOS;

public class BaseViewController : UIViewController
{
    public BaseViewController() {  }
    
    public BaseViewController(IntPtr handle) : base(handle) {  }
    
    protected string PageTitle { get; set; }

    public override void ViewDidLoad()
    {
        base.ViewDidLoad();
        NavigationItem.TitleView = new UILabel
        {
            Text = PageTitle,
            TextColor = UIColor.White,
            Font = UIFont.FromName("HelveticaNeue-Bold", 18)
        };
        
        if (NavigationController != null)
        {
            NavigationController.NavigationBar.Translucent = false;
            NavigationController.NavigationBar.BarTintColor = MainViewController.ScanbotRed;
            NavigationController.NavigationBar.TintColor = UIColor.White;
            if (NavigationController.View != null)
            {
                NavigationController.View.BackgroundColor = MainViewController.ScanbotRed;
            }
        }
    }

    protected void SetFlashButton(Func<bool> action)
    {
        var flashButton = UIImage.GetSystemImage("bolt.fill")?.ApplyTintColor(UIColor.Yellow);
        NavigationItem.RightBarButtonItem = new UIBarButtonItem(flashButton, UIBarButtonItemStyle.Plain,
            (_, _) =>
            {
                var scanning = action?.Invoke() ?? false;
                if (NavigationItem.RightBarButtonItem != null)
                {
                    NavigationItem.RightBarButtonItem.TintColor = scanning ? UIColor.Yellow : UIColor.White;
                }
            });
    }
}