using BarcodeSDK.MAUI.Constants;

namespace BarcodeSDK.MAUI.Example.Common.Models
{
    /// <summary>
    /// Barcode scanner additional parameters.
    /// </summary>
    public class BarcodeScannerAdditionalParameters
    {
        /// <summary>
        /// When set to `true`, the scanner assumes that the barcode can be a GS1 barcode.
        /// Turn it off, if you don't want to see decoded FNC1 characters ("]C1" and ASCII char 29).
        /// The default value is `true`.
        /// NOTE: Currently works for CODE128 barcodes only!
        /// </summary>
        public bool? Gs1DecodingEnabled { get; set; }

        /// <summary>
        /// Optional minimum required text length of the detected barcode.
        /// The default is 0 (setting is turned off).
        /// NOTE: This feature works on ITF barcodes only.
        /// </summary>
        public int? MinimumTextLength { get; set; }

        /// <summary>
        /// Optional maximum required text length of the detected barcode.
        /// The default is 0 (setting is turned off).
        /// NOTE: This feature works on ITF barcodes only.
        /// </summary>
        public int? MaximumTextLength { get; set; }

        /// <summary>
        /// Optional minimum required quiet zone on the barcode.
        /// Measured in modules (the size of minimal bar on the barcode).
        /// The default is 10.
        /// NOTE: This feature works on ITF barcodes only.
        /// </summary>
        public int? Minimum1DBarcodesQuietZone { get; set; }

        /// <summary>
        /// The checksum algorithm for MSI Plessey barcodes.
        /// The default value is Mod10.
        /// </summary>
        public MSIPlesseyChecksumAlgorithm? MsiPlesseyChecksumAlgorithm { get; set; }

        /// <summary>
        /// With this option enabled, the scanner removes checks digits for UPC, EAN and MSI Plessey codes.
        /// Has no effect if both single and double digit MSI Plessey checksums are enabled.
        /// The default is `false`
        /// </summary>
        public bool? StripCheckDigits { get; set; }

        /// <summary>
        /// If `true`, enabled the mode which slightly decreases the scanning quality and the energy consumption, and increases the scanning speed.
        /// If `false` - mode is disabled.
        /// The default is `false`
        /// NOTE: Android Only
        /// </summary>
        public bool? LowPowerMode { get; set; }
        /// <summary>
        /// The expected QR code density. Set to `High` if you expect to find a high number of barcodes in one image
        /// or video frame. Set to `Low` otherwise.
        /// The default is `Low`
        /// </summary>
        public BarcodeDensity? CodeDensity { get; set; }
    }
}

