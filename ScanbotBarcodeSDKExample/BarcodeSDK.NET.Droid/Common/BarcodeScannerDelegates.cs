using IO.Scanbot.Sdk.Barcode;
using IO.Scanbot.Sdk.Barcode.Entity;
using IO.Scanbot.Sdk.Barcode.UI;
using IO.Scanbot.Sdk.Camera;

namespace BarcodeSDK.NET.Droid
{
    class BarcodeEventArgs : EventArgs
    {
        public BarcodeScanningResult Result { get; private set; }

        public BarcodeEventArgs(Java.Lang.Object value)
        {
            Result = (BarcodeScanningResult)value;
        }
    }

    class BarcodeResultDelegate : BarcodeDetectorResultHandlerWrapper
    {
        public EventHandler<BarcodeEventArgs> Success;

        public override bool HandleResult(BarcodeScanningResult result, IO.Scanbot.Sdk.SdkLicenseError error)
        {
            if (!MainActivity.SDK.LicenseInfo.IsValid)
            {
                return false;
            }
            Success?.Invoke(this, new BarcodeEventArgs(result));
            return false;
        }
    }

    public class PictureTakenEventArgs : EventArgs
    {
        public byte[] Image { get; private set; }
        public int Orientation { get; private set; }

        public PictureTakenEventArgs(byte[] image, int orientation)
        {
            Image = image;
            Orientation = orientation;
        }
    }

    class PictureResultDelegate : PictureCallback
    {
        public EventHandler<PictureTakenEventArgs> PictureTaken;

        public override void OnPictureTaken(byte[] image, CaptureInfo captureInfo)
        {
            if (!MainActivity.SDK.LicenseInfo.IsValid)
            {
                return;
            }
            PictureTaken?.Invoke(this, new PictureTakenEventArgs(image, captureInfo.ImageOrientation));
        }
    }

    internal class BarcodeScannerViewCallback : Java.Lang.Object, IBarcodeScannerViewCallback
    {
        public EventHandler<PictureTakenEventArgs> PictureTaken;
        public Action CameraOpen;
        public EventHandler<BarcodeItem> SelectionOverlayBarcodeClicked;

        public void OnSelectionOverlayBarcodeClicked(BarcodeItem barcodeItem)
        {
            SelectionOverlayBarcodeClicked?.Invoke(null, barcodeItem);
        }

        public void OnCameraOpen()
        {
            CameraOpen?.Invoke();
        }

        public void OnPictureTaken(byte[] image, CaptureInfo captureInfo)
        {
            if (!MainActivity.SDK.LicenseInfo.IsValid)
            {
                return;
            }
            PictureTaken?.Invoke(this, new PictureTakenEventArgs(image, captureInfo.ImageOrientation));
        }
    }
}

