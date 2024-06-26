namespace BarcodeSDK.NET.Droid;

public class Converters
{
    public static byte[] ConvertToByteArray(IList<Java.Lang.Byte> rawBytes)
    {
        byte[] byteArray = new byte[rawBytes.Count];
        for (int i = 0; i < rawBytes.Count; i++)
        {
            byteArray[i] = (byte)rawBytes[i].ByteValue();
        }
        return byteArray;
    }
}