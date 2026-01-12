using PhotosUI;
using ScanbotSDK.iOS;

namespace BarcodeSDK.NET.iOS.Example.Utils;

public class ImagePickerService : NSObject, IPHPickerViewControllerDelegate
{
    public static async Task<SBSDKImageRef> PickImageAsync()
    {
        var service = new ImagePickerService();
        var image = await service.PickNativeImageAsync();
        return SBSDKImageRef.FromUIImageWithImage(image, new SBSDKRawImageLoadOptions());
    }
    
    private static  TaskCompletionSource<UIImage> _taskCompletionSource;
    private Task<UIImage> PickNativeImageAsync()
    {
        _taskCompletionSource = new TaskCompletionSource<UIImage>();

        // Configure picker
        var config = new PHPickerConfiguration
        {
            SelectionLimit = 1,
            Filter = PHPickerFilter.ImagesFilter,
        };

        var picker = new PHPickerViewController(config);
        picker.Delegate = this;

        // Get top-most view controller
        var vc = UIApplication.SharedApplication.KeyWindow?.RootViewController;
        while (vc?.PresentedViewController != null)
            vc = vc.PresentedViewController;

        vc?.PresentViewController(picker, true, null);

        return _taskCompletionSource.Task;
    }

    // Picker Callback
    public void DidFinishPicking(PHPickerViewController picker, PHPickerResult[] results)
    {
        picker.DismissViewController(true, null);

        if (results.Length == 0)
        {
            _taskCompletionSource.SetResult(null);
            return;
        }

        var provider = results[0].ItemProvider;

        if (provider.CanLoadObject(typeof(UIImage)))
        {
            provider.LoadObject<UIImage>((image, error) =>
            {
                if (error != null)
                {
                    _taskCompletionSource.SetException(new NSErrorException(error));
                    return;
                }
                
                _taskCompletionSource.SetResult(image);
            });
        }
        else
        {
            _taskCompletionSource.SetResult(null);
        }
    }
}
