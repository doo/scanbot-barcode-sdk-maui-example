using Android.Content;
using Android.Graphics;
using Android.Provider;

namespace BarcodeSDK.NET.Droid;

public static class ImageUtils
{
    public static Bitmap LoadBitmapFromUri(Android.Net.Uri uri, ContentResolver contentResolver)
    {
        if (uri == null) return null;
        
        var bitmap = MediaStore.Images.Media.GetBitmap(contentResolver, uri);
        return bitmap == null ? null : RotateBitmapIfRequired(bitmap, uri, contentResolver);
    }

    private static Bitmap RotateBitmapIfRequired(Bitmap bitmap, Android.Net.Uri uri, ContentResolver contentResolver)
    {
        using var inputStream = contentResolver.OpenInputStream(uri);
        if (inputStream == null) return null;

        var exif = new AndroidX.ExifInterface.Media.ExifInterface(inputStream);
        var orientation = exif.GetAttributeInt(Android.Media.ExifInterface.TagOrientation,
            (int)Android.Media.Orientation.Normal);

        return RotateBitmap(bitmap, orientation);
    }

    private static Bitmap RotateBitmap(Bitmap bitmap, int orientation)
    {
        var matrix = new Matrix();

        switch (orientation)
        {
            case (int)Android.Media.Orientation.Rotate90:
                matrix.PostRotate(90);
                break;

            case (int)Android.Media.Orientation.Rotate180:
                matrix.PostRotate(180);
                break;

            case (int)Android.Media.Orientation.Rotate270:
                matrix.PostRotate(270);
                break;

            default:
                return bitmap;
        }

        var rotatedBitmap = Bitmap.CreateBitmap(
            bitmap, 0, 0,
            bitmap.Width, bitmap.Height,
            matrix, true
        );

        bitmap.Recycle();

        return rotatedBitmap;
    }
}
