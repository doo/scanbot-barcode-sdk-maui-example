using Java.Util;

namespace BarcodeSDK.NET.Droid;

public class BaseBarcodeResult<TNativeBarcodeResult>
    where TNativeBarcodeResult : global::Java.Lang.Object, global::Android.OS.IParcelable
{
    private const string ScanResultKey = "scan_result";
    private const string ScannedImageUuidKey = "scanned_image_uuid";

    public TNativeBarcodeResult ScanResult { get; private set; }

    public UUID ScannedImageUuid { get; private set; }

    public BaseBarcodeResult()
    {
    }

    public BaseBarcodeResult(TNativeBarcodeResult result)
    {
        ScanResult = result;
    }

    public BaseBarcodeResult(TNativeBarcodeResult result, UUID scannedImageUuid)
    {
        ScanResult = result;
        ScannedImageUuid = scannedImageUuid;
    }

    public virtual BaseBarcodeResult<TNativeBarcodeResult> FromBundle(Bundle bundle)
    {
        ScanResult = bundle?.GetParcelable(ScanResultKey) as TNativeBarcodeResult;
        
        var imageUuid = bundle?.GetString(ScannedImageUuidKey);
        if (imageUuid != null)
        {
            ScannedImageUuid = UUID.FromString(imageUuid);
        }
        
        return this;
    }
    
    public virtual Bundle ToBundle()
    {
        var bundle = new Bundle();
        bundle.PutParcelable(ScanResultKey, ScanResult);

        if (ScannedImageUuid != null)
        {
            bundle.PutString(ScannedImageUuidKey, ScannedImageUuid.ToString());
        }

        return bundle;
    }
}