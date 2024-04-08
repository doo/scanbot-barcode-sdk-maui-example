
using ClassicComponent.MAUI.Legacy.ClassicComponent;
using ClassicComponent.MAUI.Legacy.Platforms.iOS.Utils;
using CoreGraphics;
using ScanbotSDK.iOS;
using ScanbotSDK.MAUI.iOS.Utils;
using ScanbotSDK.MAUI.Models;
using UIKit;

namespace ClassicComponent.MAUI.Legacy.Platforms.iOS.CustomViews
{
    public class BarcodeCameraView_iOS : UIView
	{
        private BarcodeCameraViewHandler barcodeCameraViewHandler;
        private SBSDKBarcodeScannerViewController cameraViewController;
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
            if (config.Enabled == true)
            {
                cameraViewController.IsTrackingOverlayEnabled = true;

                if (config.AutomaticSelectionEnabled != null)
                {
                    cameraViewController.TrackingOverlayController.Configuration.IsAutomaticSelectionEnabled = config.AutomaticSelectionEnabled.Value;
                }

                if (config.StrokeColor != null)
                {
                    cameraViewController.TrackingOverlayController.Configuration.PolygonStyle.PolygonColor = config.StrokeColor.ToNative();
                }

                if (config.TextColor != null)
                {
                    cameraViewController.TrackingOverlayController.Configuration.TextStyle.TextColor = config.TextColor.ToNative();
                }

                if (config.TextContainerColor != null)
                {
                    cameraViewController.TrackingOverlayController.Configuration.TextStyle.TextBackgroundColor = config.TextContainerColor.ToNative();
                }

                if (config.HighlightedStrokeColor != null)
                {
                    cameraViewController.TrackingOverlayController.Configuration.PolygonStyle.PolygonSelectedColor = config.HighlightedStrokeColor.ToNative();
                }

                if (config.HighlightedTextColor != null)
                {
                    cameraViewController.TrackingOverlayController.Configuration.TextStyle.SelectedTextColor = config.HighlightedTextColor.ToNative();
                }

                if (config.HighlightedTextContainerColor != null)
                {
                    cameraViewController.TrackingOverlayController.Configuration.TextStyle.TextBackgroundSelectedColor = config.HighlightedTextContainerColor.ToNative();
                }

                if (config.PolygonBackgroundColor != null)
                {
                    cameraViewController.TrackingOverlayController.Configuration.PolygonStyle.PolygonBackgroundColor = config.PolygonBackgroundColor.ToNative();
                }

                if (config.PolygonBackgroundHighlightedColor != null)
                {
                    cameraViewController.TrackingOverlayController.Configuration.PolygonStyle.PolygonBackgroundSelectedColor = config.PolygonBackgroundHighlightedColor.ToNative();
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
            if (ScanbotSDK.MAUI.ScanbotBarcodeSDK.LicenseInfo.IsValid)
            {
                return true;
            }
            else
            {
                ViewUtils.ShowAlert("License Expired!", "Ok");
                return false;
            }
        }
    }
}

