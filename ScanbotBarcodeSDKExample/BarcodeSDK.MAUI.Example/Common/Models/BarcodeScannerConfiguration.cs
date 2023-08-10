using BarcodeSDK.MAUI.Constants;

namespace BarcodeSDK.MAUI.Example.Common.Models
{
    /// <summary>
    /// Barcode Scanner Configuration.
    /// </summary>
    public sealed partial class BarcodeScannerConfiguration : BarcodeScannerAdditionalParameters
    {
        /// <summary>
        /// An optional array of Document Formats that act as a detection filter.
        /// Only the specified Document Formats will be recognized!
        /// Default is empty (all supported Document Formats will be recognized)
        /// </summary>
        public List<BarcodeDocumentFormat> AcceptedDocumentFormats { get; set; }

        /// <summary>
        /// List of accepted barcode formats.
        /// </summary>
        public List<BarcodeFormat> AcceptedFormats { get; set; }

        /// <summary>
        /// Specifies the way of barcode images generation or disables this generation at all.
        /// Use, if you want to receive a full sized image with barcodes.
        /// Defaults to SBSDKBarcodeImageGenerationTypeNone.
        /// </summary>
        public BarcodeImageGenerationType BarcodeImageGenerationType { get; set; } = BarcodeImageGenerationType.None;

        /// <summary>
        /// Whether the cancel button is hidden or not (iOS only)
        /// </summary>
        public bool? CancelButtonHidden { get; set; }

        /// <summary>
        /// String being displayed on the cancel button.
        /// </summary>
        public string CancelButtonTitle { get; set; }

        /// Relative width of finder frame. Together with finderHeight it defines the aspect ratio,
        /// which is used to build actual finder frame. Default is 1.
        /// For example if finderWidth and finderHeight both equals 1 -
        /// it will make a square frame, which is good for QR capturing.
        public int? FinderWidth { get; set; }

        /// <summary>
        /// Relative height of finder frame. Together with finderWidth it defines the aspect ratio,
        /// which is used to build actual finder frame. Default is 1.
        /// For example if finderWidth and finderHeight both equals 1 -
        /// it will make a square frame, which is good for QR capturing.
        /// </summary>
        public int? FinderHeight { get; set; }

        /// <summary>
        /// Width of finder frame border. Default is 2.
        /// </summary>
        public double? FinderLineWidth { get; set; }

        /// <summary>
        /// String being displayed as description.
        /// </summary>
        public string FinderTextHint { get; set; }

        /// <summary>
        /// String being displayed on the flash button (iOS only).
        /// </summary>
        public string FlashButtonTitle { get; set; }

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
        /// Whether scanner screen should make a sound on successful barcode or MRZ detection.
        /// </summary>
        public bool? SuccessBeepEnabled { get; set; }

        /// <summary>
        /// If set to true, it makes the camera button title visible.
        /// </summary>
        public string EnableCameraButtonTitle { get; set; }

        /// <summary>
        /// The camera explanation text.
        /// </summary>
        public string EnableCameraExplanationText { get; set; }

        /// <summary>
        /// The desired engine mode. You can set it to "Legacy" to use
        /// the outdated detection engine. Default is NextGen.
        /// </summary>
        public EngineMode EngineMode { get; set; } = EngineMode.NextGen;

        /// <summary>
        /// Overlay configuration. If enabled, PolygonColor, TextColor
        /// and TextContainerColor are required, rest are optional
        /// </summary>
        public SelectionOverlayConfiguration? OverlayConfiguration { get; set; }

#nullable enable

        /// <summary>
        /// Background color of the top bar.
        /// </summary>
        public Color? TopBarBackgroundColor { get; set; }

        /// <summary>
        /// Foreground color of the cancel button and when flash button is on.
        /// </summary>
        public Color? TopBarButtonsColor { get; set; }

        /// <summary>
        /// Background color of the detection overlay.
        /// </summary>
        public Color? CameraOverlayColor { get; set; }

        /// <summary>
        /// Foreground color of the detection overlay.
        /// </summary>
        public Color? FinderLineColor { get; set; }

        /// <summary>
        /// Foreground color of the description label.
        /// </summary>
        public Color? FinderTextHintColor { get; set; }

        /// <summary>
        /// Foreground color of the flash button when flash is off (iOS only).
        /// </summary>
        public Color? FlashButtonInactiveColor { get; set; }

#nullable disable

    }
}

