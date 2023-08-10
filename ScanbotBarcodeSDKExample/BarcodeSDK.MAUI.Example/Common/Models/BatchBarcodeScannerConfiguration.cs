using BarcodeSDK.MAUI.Constants;
namespace BarcodeSDK.MAUI.Example.Common.Models
{
    /// <summary>
    /// Batch Barcode scanning configuration.
    /// </summary>
    public sealed partial class BatchBarcodeScannerConfiguration : BarcodeScannerAdditionalParameters
    {
        /// <summary>
        /// List of accepted barcode formats.
        /// </summary>
        public List<BarcodeFormat> AcceptedFormats { get; set; }

        /// <summary>
        /// An optional array of Document Formats that act as a detection filter.
        /// Only the specified Document Formats will be recognized!
        /// Default is empty (all supported Document Formats will be recognized)
        /// </summary>
        public List<BarcodeDocumentFormat> AcceptedDocumentFormats { get; set; }

        /// <summary>
        /// String used for displaying amount of scanned barcodes. Use %d for number formatting symbol.
        /// </summary>
        public string BarcodesCountText { get; set; }

        /// <summary>
        /// Text color of the barcodes count label.
        /// </summary>
        public Color? BarcodesCountTextColor { get; set; }

        /// <summary>
        /// Background color of the detection overlay.
        /// </summary>
        public Color? CameraOverlayColor { get; set; }

        /// <summary>
        /// Whether the cancel button is hidden or not (iOS only)
        /// </summary>
        public bool? CancelButtonHidden { get; set; }

        /// <summary>
        /// String being displayed on the cancel button.
        /// </summary>
        public string CancelButtonTitle { get; set; }

        /// <summary>
        /// String being displayed on the clear button.
        /// </summary>
        public string ClearButtonTitle { get; set; }

        /// <summary>
        /// String being displayed on the delete button (iOS only).
        /// </summary>
        public string DeleteButtonTitle { get; set; }

        /// <summary>
        /// Foreground color of the top bar buttons on the details screen.
        /// </summary>
        public Color? DetailsActionColor { get; set; }

        /// <summary>
        /// Background color of the details screen.
        /// </summary>
        public Color? DetailsBackgroundColor { get; set; }

        /// <summary>
        /// Text color in the details barcodes list. Also affects image background, separator and progress spinner.
        /// </summary>
        public Color? DetailsPrimaryColor { get; set; }

        /// <summary>
        /// String used to show process of fetching mapped data for barcodes.
        /// </summary>
        public string FetchingStateText { get; set; }

        /// <summary>
        /// Aspect ratio of finder frame (width \ height), which is used to build actual finder frame.
        /// Default is 1 - it is a square frame, which is good for QR capturing.
        /// </summary>
        public AspectRatio FinderAspectRatio { get; set; }

        /// <summary>
        /// String being displayed as description.
        /// </summary>
        public string FinderTextHint { get; set; }

        /// <summary>
        /// Foreground color of the description label.
        /// </summary>
        public Color? FinderTextHintColor { get; set; }

        /// <summary>
        /// Whether flash is toggled on or off.
        /// </summary>
        public bool? FlashEnabled { get; set; }

        /// <summary>
        /// Desired interface orientation (Portrait, Landscape or All).
        /// Default is 'All'.
        /// </summary>
        public InterfaceOrientation? InterfaceOrientation { get; set; }

        /// <summary>
        /// String to show that no barcodes were scanned yet.
        /// </summary>
        public string NoBarcodesTitle { get; set; }

        /// <summary>
        /// String being displayed on the submit button.
        /// </summary>
        public string SubmitButtonTitle { get; set; }

        /// <summary>
        /// Whether scanner screen should make a sound on successful barcode or MRZ detection.
        /// </summary>
        public bool? SuccessBeepEnabled { get; set; }

        /// <summary>
        /// Background color of the top bar.
        /// </summary>
        public Color? TopBarBackgroundColor { get; set; }

        /// <summary>
        /// Foreground color of the top bar buttons on the scanning screen.
        /// </summary>
        public Color? TopBarButtonsColor { get; set; }

        /// <summary>
        /// Foreground color of the flash button when flash is off.
        /// </summary>
        public Color? TopBarButtonsInactiveColor { get; set; }

        /// <summary>
        /// The desired engine mode. You can set it to "Legacy" to use
        /// the outdated detection engine. Default is NextGen.
        /// </summary>
        public EngineMode EngineMode { get; set; } = EngineMode.NextGen;

        /// <summary>
        /// If set to true, it makes the camera button title visible.
        /// </summary>
        public string EnableCameraButtonTitle { get; set; }

        /// <summary>
        /// The camera explanation text.
        /// </summary>
        public string EnableCameraExplanationText { get; set; }

        /// <summary>
        /// Foreground color of the detection overlay.
        /// </summary>
        public Color? FinderLineColor { get; set; }

        /// <summary>
        /// Width of finder frame border. Default is 2.
        /// </summary>
        public double? FinderLineWidth { get; set; }

        /// <summary>
        /// Overlay configuration. If enabled, PolygonColor, TextColor
        /// and TextContainerColor are required, rest are optional
        /// </summary>
        public SelectionOverlayConfiguration? OverlayConfiguration { get; set; }
    }
}

