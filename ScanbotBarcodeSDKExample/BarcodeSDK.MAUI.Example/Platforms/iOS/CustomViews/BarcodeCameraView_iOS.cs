using ScanbotSDK.MAUI.Models;
using ScanbotSDK.MAUI.Example.ClassicComponent;
using CoreGraphics;
using ScanbotBarcodeSDK.iOS;
using UIKit;
using ScanbotSDK.MAUI.iOS.Utils;
using ScanbotSDK.MAUI.Example.Platforms.iOS.Utils;


namespace ScanbotSDK.MAUI.Example.Platforms.iOS.CustomViews
{
    public class BarcodeCameraView_iOS : UIView
	{
        BarcodeCameraViewHandler barcodeCameraViewHandler;
        SBSDKBarcodeScannerViewController cameraViewController;
        public BarcodeCameraView_iOS(CGRect frame) : base(frame) { }

        internal async void ConnectHandler(BarcodeCameraViewHandler barcodeCameraViewHandler)
        {
            this.barcodeCameraViewHandler = barcodeCameraViewHandler;
            var visibleViewController = await ViewUtils.TryGetTopViewControllerAsync(this);
            if (visibleViewController != null)
            {
                cameraViewController = new SBSDKBarcodeScannerViewController(visibleViewController, barcodeCameraViewHandler.PlatformView);
                cameraViewController.Delegate = new BarcodeScannerDelegate
                {
                    OnDetect = HandleBarcodeScannerResults
                };
                cameraViewController.BarcodeImageGenerationType = SBSDKBarcodeImageGenerationType.None;
                SetConfigurations();
            }
        }

        internal void MapIsFlashEnabled(bool isFlashEnabled)
        {
            if (cameraViewController == null) return;
            cameraViewController.FlashLightEnabled = isFlashEnabled;
        }

        // -----------------------------------------
        // Selection Overlay Config binding
        // -----------------------------------------
        internal void MapOverlayConfiguration(BarcodeCameraView commonView)
        {
            if (cameraViewController == null) return;
            var config = commonView.OverlayConfiguration;
            if (config?.Enabled == true)
            {
                cameraViewController.SelectionOverlayEnabled = true;
                cameraViewController.AutomaticSelectionEnabled = config.AutomaticSelectionEnabled;
                cameraViewController.SelectionPolygonColor = config.PolygonColor.ToNative();
                cameraViewController.SelectionTextColor = config.TextColor.ToNative();
                cameraViewController.SelectionTextContainerColor = config.TextContainerColor.ToNative();
                if (config.HighlightedPolygonColor != null)
                {
                    cameraViewController.SelectionHighlightedPolygonColor = config.HighlightedPolygonColor?.ToNative();
                }

                if (config.HighlightedTextColor != null)
                {
                    cameraViewController.SelectionHighlightedTextColor = config.HighlightedTextColor?.ToNative();
                }

                if (config.HighlightedTextContainerColor != null)
                {
                    cameraViewController.SelectionHighlightedTextContainerColor = config.HighlightedTextContainerColor?.ToNative();
                }
            }
        }

        internal void MapStartDetectionHandler()
        {
            if (cameraViewController == null) return;
            cameraViewController.RecognitionEnabled = true;
        }

        internal void MapStopDetectionHandler()
        {
            if (cameraViewController == null) return;
            cameraViewController.RecognitionEnabled = false;
        }

        // -----------------------------------------
        // Invokes results for the Common MAUI side
        // -----------------------------------------
        internal void HandleBarcodeScannerResults(SBSDKBarcodeScannerResult[] codes)
        {
            barcodeCameraViewHandler.VirtualView.OnBarcodeScanResult(new BarcodeResultBundle()
            {
                Barcodes = codes?.ToFormsBarcodes()
            });
        }

        /// <summary>
        /// Set the configuration again after the view is initialised.
        /// </summary>
        private void SetConfigurations()
        {
            MapIsFlashEnabled(barcodeCameraViewHandler.VirtualView.IsFlashEnabled);
            MapOverlayConfiguration(barcodeCameraViewHandler.VirtualView);
        }
    }

    // Since we cannot directly inherit from SBSDKBarcodeScannerViewControllerDelegate in our ViewRenderer,
    // we have created this wrapper class to allow binding to its events through the use of delegates
    class BarcodeScannerDelegate : SBSDKBarcodeScannerViewControllerDelegate
    {
        public delegate void OnDetectHandler(SBSDKBarcodeScannerResult[] codes);
        public OnDetectHandler OnDetect;
        public override void DidDetectBarcodes(SBSDKBarcodeScannerViewController controller, SBSDKBarcodeScannerResult[] codes)
        {
            OnDetect?.Invoke(codes);
        }

        public override bool ShouldDetectBarcodes(SBSDKBarcodeScannerViewController controller)
        {
            if (ScanbotBarcodeSDK.LicenseInfo?.IsValid == true)
            {
                return true;
            }
            else
            {
                ViewUtils.ShowAlert("License Expired!", "Ok");
                return false;
            }
        }

        public override bool ShouldHighlightResult(SBSDKBarcodeScannerViewController controller, SBSDKBarcodeScannerResult code)
        {
            return controller.AutomaticSelectionEnabled;
        }
    }
}

