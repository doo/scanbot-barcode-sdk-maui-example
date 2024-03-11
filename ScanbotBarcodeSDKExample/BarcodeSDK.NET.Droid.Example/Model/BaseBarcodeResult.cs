using Android.Graphics;

namespace BarcodeSDK.NET.Droid;

public class BaseBarcodeResult<TNativeBarcodeResult> where TNativeBarcodeResult : global::Java.Lang.Object, global::Android.OS.IParcelable
{
    public TNativeBarcodeResult ScanningResult { get; protected set; }
    
    public Bitmap ResultBitmap { get; protected set; }

    public string ImagePath { get; protected  set; }

    public string PreviewPath { get; protected set; }

    protected readonly MemoryStream _resultOutputStream;

    public BaseBarcodeResult()
    {
        
    }
    
    public BaseBarcodeResult(TNativeBarcodeResult result)
    {
        ScanningResult = result;
    }

    public BaseBarcodeResult(TNativeBarcodeResult result, string imagePath, string previewPath)
    {
        ScanningResult = result;
        ImagePath = imagePath;
        PreviewPath = previewPath;
    }
    
    public BaseBarcodeResult(TNativeBarcodeResult result, Bitmap resultBitmap)
    {
        ScanningResult = result;
        ResultBitmap = resultBitmap;
        _resultOutputStream = new MemoryStream();
    }


    public virtual BaseBarcodeResult<TNativeBarcodeResult> FromBundle(Bundle bundle)
    {
        ScanningResult = bundle?.GetParcelable(nameof(ScanningResult)) as TNativeBarcodeResult;
        
        var rawBitmapBytes = bundle?.GetByteArray(nameof(ResultBitmap)) ?? Array.Empty<byte>();

        if (rawBitmapBytes.Length > 0)
        {
            ResultBitmap = BitmapFactory.DecodeByteArray(rawBitmapBytes, 0, rawBitmapBytes.Length);
        }
        
        ImagePath = bundle?.GetString(nameof(ImagePath));
        PreviewPath = bundle?.GetString(nameof(PreviewPath));

        return this;
    }
    
    public virtual Bundle ToBundle()
    {
        var bundle = new Bundle();

        if (_resultOutputStream != null &&
            ResultBitmap != null)
        {
            // In real life, consider storing your images instead.
            const int maxImageSize = 1 * 1024 * 1024;
            var compressionSizeEstimate = 100 * ((float)maxImageSize / ResultBitmap.ByteCount);

            ResultBitmap.Compress(Bitmap.CompressFormat.Jpeg, Math.Clamp((int)compressionSizeEstimate, 25, 100), _resultOutputStream);
            bundle.PutByteArray(nameof(ResultBitmap), _resultOutputStream.ToArray());
        }
        else
        {
            bundle.PutByteArray(nameof(ResultBitmap), Array.Empty<byte>());
        }
        bundle.PutParcelable(nameof(ScanningResult), ScanningResult);
        bundle.PutString(nameof(ImagePath), ImagePath);
        bundle.PutString(nameof(PreviewPath), PreviewPath);
        return bundle;
    }
}