namespace BarcodeSDK.MAUI.Example.Platforms.iOS
{
    public static class Extensions
    {
        public static UIKit.UIColor ToNative(this Color color) => new UIKit.UIColor((float)color.Red, (float)color.Green, (float)color.Blue, (float)color.Alpha);
    }
}

