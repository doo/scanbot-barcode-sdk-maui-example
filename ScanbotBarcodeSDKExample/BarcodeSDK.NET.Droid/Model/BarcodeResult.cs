using Android.Graphics;
using IO.Scanbot.Sdk.Barcode.Entity;

namespace BarcodeSDK.NET.Droid
{
    public class BarcodeResult
    {
        public BarcodeScanningResult ScanningResult { get; private set; }

        public Bitmap ResultBitmap { get; private set; }

        public string ImagePath { get; private set; }

        public string PreviewPath { get; private set; }

        public BarcodeResult(BarcodeScanningResult result)
        {
            ScanningResult = result;
        }

        public BarcodeResult(BarcodeScanningResult result, string imagePath, string previewPath)
        {
            ScanningResult = result;
            ImagePath = imagePath;
            PreviewPath = previewPath;
        }

        public BarcodeResult(BarcodeScanningResult result, Bitmap resultBitmap)
        {
            ScanningResult = result;
            ResultBitmap = resultBitmap;
        }

        private BarcodeResult()
        {
        }

        public Bundle ToBundle()
        {
            var bundle = new Bundle();
            bundle.PutParcelable(nameof(ResultBitmap), ResultBitmap);
            bundle.PutParcelable(nameof(ScanningResult), ScanningResult);
            bundle.PutString(nameof(ImagePath), ImagePath);
            bundle.PutString(nameof(PreviewPath), PreviewPath);
            return bundle;
        }

        public static BarcodeResult CreateFromBundle(Bundle bundle)
        {
            return new BarcodeResult().FromBundle(bundle);
        }

        public BarcodeResult FromBundle(Bundle bundle)
        {
            ScanningResult = bundle?.GetParcelable(nameof(ScanningResult)) as BarcodeScanningResult;
            ResultBitmap = bundle?.GetParcelable(nameof(ResultBitmap)) as Bitmap;
            ImagePath = bundle?.GetString(nameof(ImagePath));
            PreviewPath = bundle?.GetString(nameof(PreviewPath));

            return this;
        }
    }
}
