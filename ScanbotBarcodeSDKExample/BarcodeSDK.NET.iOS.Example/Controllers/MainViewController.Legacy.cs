using System.Reflection;
using BarcodeSDK.NET.iOS.Controllers;
using BarcodeSDK.NET.iOS.Controllers.ClassicComponents;
using BarcodeSDK.NET.iOS.Utils;
using Scanbot.ImagePicker.iOS;
using ScanbotSDK.iOS;
using UIKit;

namespace BarcodeSDK.NET.iOS
{
    public partial class MainViewController
    {
        private void OnRTUUIButtonClick(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this))
            {
                return;
            }
            OpenRTUUIBarcodeScanner(withImage: false);
        }

        private void OnRTUUIImageButtonClick(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this))
            {
                return;
            }
            OpenRTUUIBarcodeScanner(withImage: true);
        }

        private void OpenRTUUIBarcodeScanner(bool withImage)
        {
            var configuration = SBSDKUIBarcodeScannerConfiguration.DefaultConfiguration;
            configuration.BehaviorConfiguration.AcceptedBarcodeTypes = BarcodeTypes.Instance.AcceptedTypes;
            configuration.BehaviorConfiguration.AdditionalParameters = new SBSDKBarcodeAdditionalParameters
            {
                CodeDensity = SBSDKBarcodeDensity.High
            };
            configuration.BehaviorConfiguration.EngineMode = SBSDKBarcodeEngineMode.NextGen;
            configuration.BehaviorConfiguration.IsSuccessBeepEnabled = true;

            if (withImage)
            {
                configuration.BehaviorConfiguration.BarcodeImageGenerationType =
                    SBSDKBarcodeImageGenerationType.CapturedImage;
            }

            configuration.TrackingOverlayConfiguration.OverlayEnabled = true;
            configuration.TrackingOverlayConfiguration.AutomaticSelectionEnabled = false;
            configuration.TrackingOverlayConfiguration.OverlayTextFormat = SBSDKBarcodeOverlayFormat.Code;
            configuration.TrackingOverlayConfiguration.TextContainerColor = UIColor.Black;
            configuration.TrackingOverlayConfiguration.TextColor = UIColor.Yellow;
            configuration.TrackingOverlayConfiguration.PolygonColor = UIColor.Yellow;

            // To see the confirmation dialog in action, uncomment the below and comment out the configuration.TrackingOverlayConfiguration lines above.
            //configuration.TextConfiguration.ConfirmationDialogTitle = "Barcode Detected!";
            //configuration.TextConfiguration.ConfirmationDialogMessage = "A barcode was found.";
            //configuration.TextConfiguration.ConfirmationDialogConfirmButtonTitle = "Continue";
            //configuration.TextConfiguration.ConfirmationDialogRetryButtonTitle = "Try again";
            //configuration.BehaviorConfiguration.ResultWithConfirmationEnabled = true;
            //configuration.BehaviorConfiguration.DialogTextFormat = SBSDKBarcodeDialogFormat.TypeAndCode;

            var controller = SBSDKUIBarcodeScannerViewController.PresentOn(this, configuration, null);
            controller.DidDetectResults += (_, e) =>
            {
                controller.DismissViewController(animated: false, completionHandler: null);

                if (e.BarcodeResults == null || e.BarcodeResults?.Length == 0)
                {
                    return;
                }

                if (NavigationController.TopViewController is ScanResultListController)
                {
                    return;
                }

                var resultsController = new ScanResultListController(e.BarcodeResults.First().BarcodeImage, e.BarcodeResults);
                NavigationController.PushViewController(resultsController, animated: true);
            };
        }

        private void OnRTUBatchBarcodeClicked(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this))
            {
                return;
            }

            // You can also use the DefaultConfiguration property directly, but then you cannot set the selectionOverlayConfig as it is read only in that case.
            var configuration = SBSDKUIBarcodesBatchScannerConfiguration.DefaultConfiguration;

            configuration.BehaviorConfiguration.AcceptedBarcodeTypes = BarcodeTypes.Instance.AcceptedTypes;
            configuration.BehaviorConfiguration.EngineMode = SBSDKBarcodeEngineMode.NextGen;
            configuration.BehaviorConfiguration.IsSuccessBeepEnabled = true;
            configuration.BehaviorConfiguration.AdditionalDetectionParameters = new SBSDKBarcodeAdditionalParameters
            {
                CodeDensity = SBSDKBarcodeDensity.High
            };
            configuration.TrackingOverlayConfiguration.OverlayEnabled = true;
            configuration.TrackingOverlayConfiguration.AutomaticSelectionEnabled = true;
            configuration.TrackingOverlayConfiguration.OverlayTextFormat = SBSDKBarcodeOverlayFormat.Code;
            configuration.TrackingOverlayConfiguration.TextColor = UIColor.Yellow;
            configuration.TrackingOverlayConfiguration.TextContainerColor = UIColor.Black;
            configuration.TrackingOverlayConfiguration.HighlightedPolygonColor = UIColor.Red;
            configuration.TrackingOverlayConfiguration.HighlightedTextColor = UIColor.Red;
            configuration.TrackingOverlayConfiguration.HighlightedTextContainerColor = UIColor.Black;
            configuration.TrackingOverlayConfiguration.PolygonColor = UIColor.Yellow;

            var controller = SBSDKUIBarcodesBatchScannerViewController.PresentOn(this, configuration, null);

            controller.DidFinishWithResults += (_, args) =>
            {
                var resultViewController = new BatchBarcodeResultViewController(args.BarcodeResults ?? new SBSDKUIBarcodeMappedResult[] { });
                NavigationController.PushViewController(resultViewController, animated: true);
            };
        }
    }
}
