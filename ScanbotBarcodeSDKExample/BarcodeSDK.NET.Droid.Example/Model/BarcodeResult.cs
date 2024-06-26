using Android.Graphics;
using IO.Scanbot.Sdk.Barcode.Entity;
using IO.Scanbot.Sdk.Ui_v2.Barcode.Configuration;

namespace BarcodeSDK.NET.Droid
{
    public class BarcodeResult
    {
        public BarcodeScanningResult ScanningResult { get; private set; }
        
        public BarcodeScannerResult ScanningResultV2 { get; private set; }

        public Bitmap ResultBitmap { get; private set; }

        public string ImagePath { get; private set; }

        public string PreviewPath { get; private set; }

        private readonly MemoryStream resultOutputStream;

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
        
        public BarcodeResult(BarcodeScannerResult result, string imagePath, string previewPath)
        {
            ScanningResultV2 = result;
            ImagePath = imagePath;
            PreviewPath = previewPath;
        }

        public BarcodeResult(BarcodeScanningResult result, Bitmap resultBitmap)
        {
            ScanningResult = result;
            ResultBitmap = resultBitmap;
            resultOutputStream = new MemoryStream();
        }

        private BarcodeResult()
        {
        }

        public Bundle ToBundle()
        {
            var bundle = new Bundle();

            if (resultOutputStream != null &&
                ResultBitmap != null)
            {
                // In real life, consider storing your images instead.
                const int maxImageSize = 1 * 1024 * 1024;
                var compressionSizeEstimate = 100 * ((float)maxImageSize / ResultBitmap.ByteCount);

                ResultBitmap.Compress(Bitmap.CompressFormat.Jpeg, Math.Clamp((int)compressionSizeEstimate, 25, 100), resultOutputStream);
                bundle.PutByteArray(nameof(ResultBitmap), resultOutputStream.ToArray());
            }
            else
            {
                bundle.PutByteArray(nameof(ResultBitmap), Array.Empty<byte>());
            }
            
            bundle.PutParcelable(nameof(ScanningResultV2), ScanningResultV2);
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
            ScanningResultV2 = bundle?.GetParcelable(nameof(ScanningResultV2)) as BarcodeScannerResult;

            var rawBitmapBytes = bundle?.GetByteArray(nameof(ResultBitmap)) ?? Array.Empty<byte>();

            if (rawBitmapBytes.Length > 0)
            {
                ResultBitmap = BitmapFactory.DecodeByteArray(rawBitmapBytes, 0, rawBitmapBytes.Length);
            }
            ImagePath = bundle?.GetString(nameof(ImagePath));
            PreviewPath = bundle?.GetString(nameof(PreviewPath));

            return this;
        }
    }
}
