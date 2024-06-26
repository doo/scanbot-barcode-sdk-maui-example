using System;
namespace BarcodeSDK.NET.iOS.Utils
{
    public class Utilities
    {
        internal static T GetViewController<T>(string storyboardID) where T : UIViewController
        {
            var viewControllerId = typeof(T)?.Name ?? string.Empty;
            UIStoryboard storyboard = UIStoryboard.FromName(storyboardID, null);
            var viewController = storyboard.InstantiateViewController(viewControllerId) as T;
            return viewController;
        }

        internal static void CreateRoundedCardView(UIView view)
        {
            view.Layer.CornerRadius = 10.0f;
            view.Layer.ShadowColor = UIColor.LightGray.CGColor;
            view.Layer.ShadowRadius = 4.0f;
            view.Layer.ShadowOpacity = 0.3f;
            view.Layer.ShadowOffset = new CoreGraphics.CGSize(0, 2);
            view.ClipsToBounds = false;
        }
    }
}