using BarcodeSDK.MAUI.Example.Common.Models;

namespace BarcodeSDK.MAUI.Example.Common.Services
{
    /// <summary>
    /// Implement the IScanbot Service in all available platforms.
    /// </summary>
    public interface IScanbotServices
	{
        /// <summary>
        /// Clear stored images
        /// </summary>
        Result ClearStorageDirectory();
    }
}