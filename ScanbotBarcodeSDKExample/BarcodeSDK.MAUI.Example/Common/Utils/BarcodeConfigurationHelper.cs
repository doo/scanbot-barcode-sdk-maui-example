using BarcodeSDK.MAUI.Constants;

namespace BarcodeSDK.MAUI.Example.Common.Utils
{
    public class BarcodeConfigurationHelper
	{
        public List<BarcodeFormat> AllBarcodeTypes => Enum.GetValues(typeof(BarcodeFormat)).Cast<BarcodeFormat>().ToList();
    }

    /// <summary>
    /// All Misc utils
    /// </summary>
    public class Utilities
    {
        /// <summary>
        /// Check for Camera permissions.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        internal static async Task<bool> CameraPermissionAllowed(bool userDenied = false)
        {
            var isAllowed = false;
            var status = await Permissions.CheckStatusAsync<Permissions.Camera>();
            switch (status)
            {
                case PermissionStatus.Unknown:  // Defult permission status For iOS 
                    {
                        await Permissions.RequestAsync<Permissions.Camera>();
                        isAllowed = await CameraPermissionAllowed();
                    }
                    break;

                case PermissionStatus.Denied: // Default permission status for Android
                    if (DeviceInfo.Platform == DevicePlatform.Android && !userDenied)
                    {
                        status = await Permissions.RequestAsync<Permissions.Camera>();
                        isAllowed = await CameraPermissionAllowed(status == PermissionStatus.Denied);
                    }
                    else
                    {
                        // Permission is already denied and the message is handled from the internal method.
                        // So sending true to moved ahead for SDK handling.
                        isAllowed = true;
                    }
                    break;

                case PermissionStatus.Disabled:
                    break;

                case PermissionStatus.Granted:
                    isAllowed = true;
                    break;

                case PermissionStatus.Restricted:
                    break;
            }
            return isAllowed;
        }
    }
}

