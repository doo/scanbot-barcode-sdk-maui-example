// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
namespace BarcodeSDK.NET.iOS.Controllers.ClassicComponents.TableViewCells
{
	[Register ("BarcodeScanAndCountResultCell")]
	partial class BarcodeScanAndCountResultCell
	{
		[Outlet]
		UIKit.UIView containerView { get; set; }

		[Outlet]
		UIKit.UILabel lblBarcodeCount { get; set; }

		[Outlet]
		UIKit.UILabel lblBarcodeResult { get; set; }

		[Outlet]
		UIKit.UILabel lblBarcodeType { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (lblBarcodeResult != null) {
				lblBarcodeResult.Dispose ();
				lblBarcodeResult = null;
			}

			if (lblBarcodeType != null) {
				lblBarcodeType.Dispose ();
				lblBarcodeType = null;
			}

			if (lblBarcodeCount != null) {
				lblBarcodeCount.Dispose ();
				lblBarcodeCount = null;
			}

			if (containerView != null) {
				containerView.Dispose ();
				containerView = null;
			}
		}
	}
}
