// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
namespace BarcodeSDK.NET.iOS.Controllers.ClassicComponents
{
	[Register ("BarcodeScanAndCountViewController")]
	partial class BarcodeScanAndCountViewController
	{
		[Outlet]
		UIKit.UIBarButtonItem btnBarcodeCount { get; set; }

		[Outlet]
		UIKit.UIView containerView { get; set; }

		[Action ("BtnShowResults_Action:")]
		partial void BtnShowResults_Action (UIKit.UIBarButtonItem sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (btnBarcodeCount != null) {
				btnBarcodeCount.Dispose ();
				btnBarcodeCount = null;
			}

			if (containerView != null) {
				containerView.Dispose ();
				containerView = null;
			}
		}
	}
}
