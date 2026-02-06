using MobileCoreServices;
using PhotosUI;
using ScanbotSDK.iOS;

namespace BarcodeSDK.NET.iOS.Example.Utils;

public class ImagePickerService : NSObject, IPHPickerViewControllerDelegate
{
    /// <summary>
    /// Update this flag to receive ImageRef from path.
    /// </summary>
    private const bool ImageRefFromPath = false;

    public static async Task<SBSDKImageRef> PickImageAsync()
    {
        var service = new ImagePickerService();
        return await service.PickNativeImageAsync();
    }

    private static TaskCompletionSource<SBSDKImageRef> _taskCompletionSource;

    private Task<SBSDKImageRef> PickNativeImageAsync()
    {
        _taskCompletionSource = new TaskCompletionSource<SBSDKImageRef>();

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

        if (results.Length == 0 || results[0] == null)
        {
            _taskCompletionSource.SetResult(null);
            return;
        }

        var provider = results[0].ItemProvider;
        if (ImageRefFromPath)
        {
            ExtractImageRefFromPath(provider);
        }
        else
        {
            ExtractImageRefFromUIImage(provider);
        }
    }

    private void ExtractImageRefFromPath(NSItemProvider provider)
    {
        // Public image type
        var typeIdentifier = UTType.Image;

        if (!provider.HasItemConformingTo(typeIdentifier))
        {
            _taskCompletionSource.SetResult(null);
            return;
        }

        // Get SBSDKImageRef from path
        provider.LoadInPlaceFileRepresentation(typeIdentifier, (url, inPlace, error) =>
        {
            if (error != null || url?.Path == null)
            {
                _taskCompletionSource.SetResult(null);
                return;
            }

            var imageRef = SBSDKImageRef.FromPathWithPath(url.Path, new SBSDKPathImageLoadOptions());
            _taskCompletionSource.SetResult(imageRef);
        });
    }

    private void ExtractImageRefFromUIImage(NSItemProvider provider)
    {
        if (provider.CanLoadObject(typeof(UIImage)))
        {
            provider.LoadObject<UIImage>((image, error) =>
            {
                if (error != null)
                {
                    _taskCompletionSource.SetException(new NSErrorException(error));
                    return;
                }

                var imageRef = SBSDKImageRef.FromUIImageWithImage(image, new SBSDKRawImageLoadOptions());
                _taskCompletionSource.SetResult(imageRef);
            });
        }
        else
        {
            _taskCompletionSource.SetResult(null);
        }
    }
}
