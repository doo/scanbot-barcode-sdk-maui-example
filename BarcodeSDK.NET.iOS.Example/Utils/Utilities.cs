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
    }
}