using System;
using BarcodeSDK.MAUI.Example.Common.Constants;

namespace BarcodeSDK.MAUI.Example.Common.Models
{
    public class InitializationOptions
    {
        /// <summary>
        /// Optional: If unspecified, trial mode will be activated
        /// </summary>
        public string LicenseKey { get; set; }

        /// <summary>
        /// Optional: Determines whether to output native debug log or not
        /// Default: false
        /// </summary>
        public bool LoggingEnabled { get; set; } = false;

        /// <summary>
        /// Optional: Determines whether to output native core C++ debug log or not
        /// NOTE: Android only
        /// Default: false
        /// </summary>
        public bool NativeLoggingEnabled { get; set; } = false;

        /// <summary>
        /// Optional: Determines whether RTU-UI should use Camera X under the hood.
        /// NOTE: Android only
        /// Default: false
        /// </summary>
        public bool UseCameraXRtuUi { get; set; } = false;

        /// <summary>
        /// Optional: Determines the storage location of scanned images/barcodes
        /// Default: Internal SDK storage is used (managed by native SDK).
        /// </summary>
        public string StorageDirectory { get; set; }

        /// <summary>
        /// Optional: License error handler, will be called when
        /// trying to access a feature that's not available in your license
        /// </summary>
        public Action<LicenseStatus, MissingFeature> ErrorHandler { get; set; }

        public bool HasLicense => LicenseKey != null;

        public bool HasCustomStorageDirectory => StorageDirectory != null;

        public bool HasErrorHandler => ErrorHandler != null;
    }
}

