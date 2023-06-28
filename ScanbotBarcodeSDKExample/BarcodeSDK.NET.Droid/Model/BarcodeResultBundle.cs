using System;
using Android.Graphics;
using IO.Scanbot.Sdk.Barcode.Entity;

namespace BarcodeSDK.NET.Droid
{
    public class BarcodeResultBundle
    {
        public static BarcodeResultBundle Instance { get; set; }

        public static BarcodeItem SelectedBarcodeItem { get; set; }


        public BarcodeScanningResult ScanningResult { get; set; }

        public string ImagePath { get; set; }

        public string PreviewPath { get; set; }

        public Bitmap ResultBitmap { get; set; }

        public BarcodeResultBundle() { }

        public BarcodeResultBundle(BarcodeScanningResult result)
        {
            ScanningResult = result;
        }

    }
}
