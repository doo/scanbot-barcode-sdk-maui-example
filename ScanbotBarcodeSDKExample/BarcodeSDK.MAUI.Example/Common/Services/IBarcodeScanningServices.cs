using BarcodeSDK.MAUI.Example.Common.Models;

namespace BarcodeSDK.MAUI.Example.Common.Services
{
    /// <summary>
    /// Barcode Scanning service provides us functions to scan single and multiple barcodes.
    /// </summary>
    public interface IBarcodeScanningServices
	{
        /// <summary>
        /// Scan Barcode from configuration.
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        Task<BarcodeResultBundle> OpenBarcodeScannerView(BarcodeScannerConfiguration configuration);

		/// <summary>
		/// Scan multiple barcodes in Batch Barcode.s
		/// </summary>
		/// <param name="configuration"></param>
		/// <returns></returns>
        Task<BarcodeResultBundle> OpenBatchBarcodeScannerView(BatchBarcodeScannerConfiguration configuration);

        /// <summary>
        /// Force Closes the Barcode scanning view.
        /// </summary>
        void CloseBarcodeScannerView();

        /// <summary>
        /// Force closes the Batch Barcode scanning view.
        /// </summary>
        void CloseBatchBarcodeScannerView();
    }
}

