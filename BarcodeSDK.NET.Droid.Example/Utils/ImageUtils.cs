using Android.Content;
using Android.Graphics;
using Android.Provider;

namespace BarcodeSDK.NET.Droid;

public static class ImageUtils
{
    public static Bitmap LoadBitmapFromUri(Android.Net.Uri uri, ContentResolver contentResolver)
    {
        if (uri == null) return null;
        
        try
        {
            var bitmap = MediaStore.Images.Media.GetBitmap(contentResolver, uri);
            return bitmap == null ? null : RotateBitmapIfRequired(bitmap, uri, contentResolver);
        }
        catch
        {
            return null;
        }
    }

    private static Bitmap RotateBitmapIfRequired(Bitmap bitmap, Android.Net.Uri uri, ContentResolver contentResolver)
    {
        using var inputStream = contentResolver.OpenInputStream(uri);
        if (inputStream == null) return bitmap;

        var exif = new AndroidX.ExifInterface.Media.ExifInterface(inputStream);
        var orientation = exif.GetAttributeInt(
            AndroidX.ExifInterface.Media.ExifInterface.TagOrientation,
            AndroidX.ExifInterface.Media.ExifInterface.OrientationNormal);

        var degrees = orientation switch
        {
            AndroidX.ExifInterface.Media.ExifInterface.OrientationRotate90 => 90,
            AndroidX.ExifInterface.Media.ExifInterface.OrientationRotate180 => 180,
            AndroidX.ExifInterface.Media.ExifInterface.OrientationRotate270 => 270,
            _ => 0
        };
        
        return RotateBitmap(bitmap, degrees);
    }

    private static Bitmap RotateBitmap(Bitmap bitmap, int degrees)
    {
        if (degrees == 0) return bitmap;
        
        var matrix = new Matrix();
        matrix.PostRotate(degrees);

        var rotatedBitmap = Bitmap.CreateBitmap(
            bitmap, 0, 0,
            bitmap.Width, bitmap.Height,
            matrix, true
        );

        bitmap.Recycle();

        return rotatedBitmap;
    }
}
