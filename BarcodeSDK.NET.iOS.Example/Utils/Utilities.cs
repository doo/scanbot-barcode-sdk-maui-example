using AVFoundation;

namespace BarcodeSDK.NET.iOS.Utils
{
    public static class Utilities
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

        internal static async Task<bool> IsCameraPermissionGranted(this UIViewController viewController)
        {
            var isGranted = false;
            var status = AVCaptureDevice.GetAuthorizationStatus(AVAuthorizationMediaType.Video);

            switch (status)
            {
                case AVAuthorizationStatus.Authorized:
                    isGranted = true;
                    break;
                
                case AVAuthorizationStatus.Denied:
                     //  Add a message to turn on permission from the settings screen.
                     Alert.Show(viewController, "Permission Alert!", "Please turn on the required camera permission from the Settings application.");
                    isGranted = false;
                    break;
                
                case AVAuthorizationStatus.Restricted:
                    break;
                
                case AVAuthorizationStatus.NotDetermined:
                     isGranted = await AVCaptureDevice.RequestAccessForMediaTypeAsync(AVAuthorizationMediaType.Video);
                    break;
            }

            return isGranted;
        }
        
        /// <summary>
        /// Add Constraints for left, top, right to a view.
        /// This is generally used for setting constraints to a View in a vertical order.
        /// </summary>
        /// <param name="parentView">Parent view of the newView, on which you are setting the constraints.</param>
        /// <param name="newView">The View on which you are setting the constraint.</param>
        /// <param name="prevView">The previous view on top of the current newView.</param>
        internal static void AddLTRViewConstraints(this UIView parentView, UIView newView, UIView prevView)
        {
            var leftConstraintLabel = NSLayoutConstraint.Create(newView, NSLayoutAttribute.Left, NSLayoutRelation.Equal, parentView, NSLayoutAttribute.Left, 1, 15);
            var rightConstraintLabel = NSLayoutConstraint.Create(newView, NSLayoutAttribute.Right, NSLayoutRelation.Equal, parentView, NSLayoutAttribute.Right, 1, -15);
            var topConstraintLabel = NSLayoutConstraint.Create(newView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, prevView, NSLayoutAttribute.Bottom, 1,9);
            parentView.AddConstraints([leftConstraintLabel, rightConstraintLabel, topConstraintLabel]);
        }
    }
}