using System;
using BarcodeSDK.MAUI.Example.Common.Models;
using BarcodeSDK.MAUI.Example.Common.Services;

namespace BarcodeSDK.MAUI.Example.Common.Helper
{
    /// <summary>
    /// SBSDK Helper Clas for accessing the Dependency service from native side.
    /// </summary>
	public class SBSDK
	{
        /// <summary>
        /// Singleton implementation of the Barcode Scanner launcher
        /// </summary>
        public static IBarcodeScanningServices Scanner => DependencyService.Get<IBarcodeScanningServices>();

        /// <summary>
        /// Singleton implementation of the image picker launcher
        /// </summary>
        //public static IImagePicker ImagePicker => DependencyService.Get<IImagePicker>();

        /// <summary>
        /// Singleton implementation available operations in the Scanbot Barcode SDK
        /// </summary>
        public static IScanbotServices Operations => DependencyService.Get<IScanbotServices>();

        /// <summary>
        /// The main entry point for Scanbot Barcode SDK. The SDK must be initialized
        /// </summary>
        public static void Initialize(InitializationOptions options)
        {
            InternalOperations.Initialize(options);
        }

        /// <summary>
        /// SDK License info. Should be periodically checked whether the license is still valid or not
        /// </summary>
        public static LicenseInfo LicenseInfo
        {
            get => InternalOperations.GetLicenseInfo();
        }

        static IInternalServices InternalOperations
        {
            get => DependencyService.Get<IInternalServices>();
        }
    }
}

