using Android;
using Android.Content;
using Android.Content.PM;
using Android.Views;
using Android.Widget;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using BarcodeSDK.MAUI.Droid.Utils;
using BarcodeSDK.MAUI.Models;
using IO.Scanbot.Sdk;
using IO.Scanbot.Sdk.Barcode;
using IO.Scanbot.Sdk.Barcode.Entity;
using IO.Scanbot.Sdk.Barcode.UI;
using IO.Scanbot.Sdk.Camera;
using IO.Scanbot.Sdk.UI.Camera;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using SBSDK = IO.Scanbot.Sdk.Barcode_scanner.ScanbotBarcodeScannerSDK;

namespace BarcodeSDK.MAUI.Example.ClassicComponent
{
    public partial class BarcodeCameraViewHandler : ViewHandler<BarcodeCameraView, FrameLayout>
    {
        // Classical component
        protected BarcodeScannerView cameraViewDroid;
        private readonly int REQUEST_PERMISSION_CODE = 200;

        #region Handler Overrides

        protected override FrameLayout CreatePlatformView()
        {
            var cameraLayout = (FrameLayout)LayoutInflater
                      .FromContext(Context)
                      .Inflate(Resource.Layout.barcode_camera_view, null, false);


            // Here we retrieve the Camera View...
            cameraViewDroid = cameraLayout.FindViewById<BarcodeScannerView>(Resource.Id.barcode_camera);
            InstallHierarchyFitter(cameraViewDroid);
            return cameraLayout;
        }

        protected override void ConnectHandler(FrameLayout platformView)
        {
            base.ConnectHandler(platformView);

            var detector = new SBSDK(Context.GetActivity()).CreateBarcodeDetector();
            detector.ModifyConfig(new Function1Impl<BarcodeScannerConfigBuilder>((response) =>
            {
                response.SetSaveCameraPreviewFrame(false);
            }));

            cameraViewDroid.InitCamera(new CameraUiSettings(false));
            BarcodeScannerViewWrapper.InitDetectionBehavior(cameraViewDroid, detector, new SBResultHandler(HandleFrameHandlerResult), new BarcodeScannerViewCallback(VirtualView, cameraViewDroid));
        }

        protected override void DisconnectHandler(FrameLayout platformView)
        {
            base.DisconnectHandler(platformView);
        }

        protected override void RemoveContainer()
        {
            base.RemoveContainer();
        }

        protected override void SetupContainer()
        {
            base.SetupContainer();
        }

        #endregion

        #region Properties Implementation

        public static void MapOverlayConfiguration(BarcodeCameraViewHandler current, BarcodeCameraView commonView)
        {
            current.SetSelectionOverlayConfiguration(commonView);
        }

        public static void MapIsFlashEnabled(BarcodeCameraViewHandler current, BarcodeCameraView commonView)
        {
            current.cameraViewDroid.ViewController.UseFlash(commonView.IsFlashEnabled);
        }

        #endregion

        #region Event Handlers Implementation

        public static void MapStartDetectionHandler(BarcodeCameraViewHandler current, BarcodeCameraView commonView, object arg3)
        {
            current?.CheckPermissions();
            current?.cameraViewDroid?.ViewController.StartPreview();
            current?.cameraViewDroid?.ViewController?.OnResume();
        }

        public static void MapOnPauseHandler(BarcodeCameraViewHandler current, BarcodeCameraView commonView, object arg3)
        {
            current.cameraViewDroid.ViewController.OnPause();
        }

        public static void MapOnResumeHandler(BarcodeCameraViewHandler current, BarcodeCameraView commonView, object arg3)
        {
            current.cameraViewDroid.ViewController.OnResume();
        }

        public static void MapStopDetectionHandler(BarcodeCameraViewHandler current, BarcodeCameraView commonView, object arg3)
        {
            current.cameraViewDroid.ViewController.StopPreview();
        }

        private void CheckPermissions()
        {
            if (Context == null || Context.GetActivity() == null)
            {
                return;
            }

            var activity = Context.GetActivity();

            if (ContextCompat.CheckSelfPermission(activity, Manifest.Permission.Camera) != Permission.Granted)
            {
                ActivityCompat.RequestPermissions(activity, new string[] { Manifest.Permission.Camera }, REQUEST_PERMISSION_CODE);
            }
        }
        #endregion

        #region Support Methods

        public void SetSelectionOverlayConfiguration(BarcodeCameraView commonView)
        {
            if (commonView.OverlayConfiguration?.Enabled == true)
            {
                cameraViewDroid.SelectionOverlayController.SetEnabled(commonView.OverlayConfiguration.Enabled);
                cameraViewDroid.SelectionOverlayController.SetPolygonColor(commonView.OverlayConfiguration.PolygonColor.ToArgb());
                cameraViewDroid.SelectionOverlayController.SetTextColor(commonView.OverlayConfiguration.TextColor.ToArgb());
                cameraViewDroid.SelectionOverlayController.SetTextContainerColor(commonView.OverlayConfiguration.TextContainerColor.ToArgb());

                if (commonView.OverlayConfiguration.HighlightedPolygonColor != null)
                {
                    cameraViewDroid.SelectionOverlayController.SetPolygonHighlightedColor(commonView.OverlayConfiguration.HighlightedPolygonColor.ToArgb());
                }

                if (commonView.OverlayConfiguration.HighlightedTextColor != null)
                {
                    cameraViewDroid.SelectionOverlayController.SetTextHighlightedColor(commonView.OverlayConfiguration.HighlightedTextColor.ToArgb());
                }

                if (commonView.OverlayConfiguration.HighlightedTextContainerColor != null)
                {
                    cameraViewDroid.SelectionOverlayController.SetTextContainerHighlightedColor(commonView.OverlayConfiguration.HighlightedTextContainerColor.ToArgb());
                }
            }
        }

        private bool HandleFrameHandlerResult(BarcodeScanningResult result, IO.Scanbot.Sdk.SdkLicenseError error)
        {
            if (result == null)
            {
                cameraViewDroid.Post(() => Toast.MakeText(Context.GetActivity(), "License has expired!", ToastLength.Long).Show());
                return false;
            }

            var overlayEnabled = VirtualView.OverlayConfiguration?.Enabled ?? false;
            if (overlayEnabled == false)
            {
                var outResult = new BarcodeResultBundle
                {
                    Barcodes = result.BarcodeItems.ToFormsBarcodeList(),
                    Image = result.PreviewFrame.ToImageSource()
                };

                VirtualView.OnBarcodeScanResult?.Invoke(outResult);
            }
            return true;
        }

        private static void InstallHierarchyFitter(ViewGroup viewGroup)
        {
            viewGroup.SetOnHierarchyChangeListener(new HierarchyChangeListener(viewGroup));
        }

        #endregion
    }

    internal class SBResultHandler : BarcodeDetectorResultHandlerWrapper
    {
        public delegate bool HandleFrameHandlerResult(BarcodeScanningResult result, SdkLicenseError error);
        public readonly HandleFrameHandlerResult handleResultFunc;

        public SBResultHandler(HandleFrameHandlerResult handleResultFunc)
        {
            this.handleResultFunc = handleResultFunc;
        }

        public override bool HandleResult(BarcodeScanningResult result, SdkLicenseError error)
        {
            handleResultFunc(result, error);
            System.Diagnostics.Debug.WriteLine(result?.BarcodeItems);
            return false;
        }
    }

    internal class BarcodeScannerViewCallback : Java.Lang.Object, IBarcodeScannerViewCallback
    {
        private BarcodeCameraView virtualView;
        private BarcodeScannerView cameraViewDroid;

        public BarcodeScannerViewCallback(BarcodeCameraView virtualView, BarcodeScannerView cameraViewDroid)
        {
            this.virtualView = virtualView;
            this.cameraViewDroid = cameraViewDroid;
        }

        public void OnSelectionOverlayBarcodeClicked(BarcodeItem barcodeItem)
        {
            var outResult = new BarcodeResultBundle
            {
                Barcodes = new List<Barcode>() { barcodeItem.ToFormsBarcode() },
                Image = barcodeItem.Image.ToImageSource()
            };

            virtualView.OnBarcodeScanResult.Invoke(outResult);
        }

        public void OnCameraOpen()
        {
            cameraViewDroid.ViewController.UseFlash(virtualView.IsFlashEnabled);
        }

        public void OnPictureTaken(byte[] image, CaptureInfo captureInfo)
        {
            // get the image 
        }
    }

    internal class HierarchyChangeListener : Java.Lang.Object, ViewGroup.IOnHierarchyChangeListener
    {
        private ViewGroup viewGroup;

        public HierarchyChangeListener(ViewGroup viewGroup)
        {
            this.viewGroup = viewGroup;
        }

        public void OnChildViewAdded(Android.Views.View parent, Android.Views.View child)
        {
            if (parent == null) { return; }
            parent.Measure(
                Android.Views.View.MeasureSpec.MakeMeasureSpec(viewGroup.MeasuredWidth, MeasureSpecMode.Exactly),
                Android.Views.View.MeasureSpec.MakeMeasureSpec(viewGroup.MeasuredHeight, MeasureSpecMode.Exactly)
            );
            parent.Layout(0, 0, parent.MeasuredWidth, parent.MeasuredHeight);
        }

        public void OnChildViewRemoved(Android.Views.View parent, Android.Views.View child)
        {

        }
    }

    /**
    * Snippet from: 
    * https://stackoverflow.com/questions/64013415/pass-lambda-function-to-c-sharp-generated-code-of-kotlin-in-xamarin-android-bind
    */
    class Function1Impl<T> : Java.Lang.Object, Kotlin.Jvm.Functions.IFunction1 where T : Java.Lang.Object
    {
        private readonly Action<T> OnInvoked;

        public Function1Impl(Action<T> onInvoked)
        {
            this.OnInvoked = onInvoked;
        }

        public Java.Lang.Object Invoke(Java.Lang.Object objParameter)
        {
            try
            {
                T parameter = (T)objParameter;
                OnInvoked?.Invoke(parameter);
                return null;
            }
            catch (Exception ex)
            {
                // Exception handling, if needed
            }

            return null;
        }
    }
}