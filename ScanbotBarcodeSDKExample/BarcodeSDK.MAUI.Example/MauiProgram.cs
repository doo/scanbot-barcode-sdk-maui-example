namespace ScanbotSDK.MAUI.Example
{
    public static class MauiProgram
    {
        // Without a license key, the Scanbot Barcode SDK will work for 1 minute.
        // To scan longer, register for a trial license key here: https://scanbot.io/trial/
        public const string LicenseKey =   "RSGfnbhOxEnfq8s2gwHlDNhUFMB+5b" +
  "0/4rEBnS9CkZNcF/h37N63m/up8p0z" +
  "gTw011H6of8WuFFXMBoVvlz+4P9aIB" +
  "72LJydlJWuROEMh4KR/b7EK1vzT3ji" +
  "HJF6k8RgWi7quaRkws8QsN8JibMUTy" +
  "9z1Kis+o7qU0YC5ivRJ4rQDcQmB8zi" +
  "4M1YlUb3gjk93ABlJzMTL9L0UXT67W" +
  "rg+zUBBFpxA0qJdBrLSiqM+vOUulzP" +
  "lF7tbmVLqJG2xYpDHG3Wq0Vwh7CjWL" +
  "vIQ+LmysodbjOdsA3EmwNJCB5jyFde" +
  "K7+8W/wJvf5ug4DNqBHV5xGK4q+LZL" +
  "4fWz8Oex2W6A==\nU2NhbmJvdFNESw" +
  "pkb28uc2NhbmJvdC5jYXBhY2l0b3Iu" +
  "ZXhhbXBsZXxpby5zY2FuYm90LmV4YW" +
  "1wbGUuZmx1dHRlcnxpby5zY2FuYm90" +
  "LmV4YW1wbGUuc2RrLmFuZHJvaWR8aW" +
  "8uc2NhbmJvdC5leGFtcGxlLnNkay5i" +
  "YXJjb2RlLmFuZHJvaWR8aW8uc2Nhbm" +
  "JvdC5leGFtcGxlLnNkay5iYXJjb2Rl" +
  "LmNhcGFjaXRvcnxpby5zY2FuYm90Lm" +
  "V4YW1wbGUuc2RrLmJhcmNvZGUuZmx1" +
  "dHRlcnxpby5zY2FuYm90LmV4YW1wbG" +
  "Uuc2RrLmJhcmNvZGUuaW9uaWN8aW8u" +
  "c2NhbmJvdC5leGFtcGxlLnNkay5iYX" +
  "Jjb2RlLm1hdWl8aW8uc2NhbmJvdC5l" +
  "eGFtcGxlLnNkay5iYXJjb2RlLm5ldH" +
  "xpby5zY2FuYm90LmV4YW1wbGUuc2Rr" +
  "LmJhcmNvZGUucmVhY3RuYXRpdmV8aW" +
  "8uc2NhbmJvdC5leGFtcGxlLnNkay5i" +
  "YXJjb2RlLndpbmRvd3N8aW8uc2Nhbm" +
  "JvdC5leGFtcGxlLnNkay5iYXJjb2Rl" +
  "LnhhbWFyaW58aW8uc2NhbmJvdC5leG" +
  "FtcGxlLnNkay5iYXJjb2RlLnhhbWFy" +
  "aW4uZm9ybXN8aW8uc2NhbmJvdC5leG" +
  "FtcGxlLnNkay5jYXBhY2l0b3J8aW8u" +
  "c2NhbmJvdC5leGFtcGxlLnNkay5jYX" +
  "BhY2l0b3IuYW5ndWxhcnxpby5zY2Fu" +
  "Ym90LmV4YW1wbGUuc2RrLmNhcGFjaX" +
  "Rvci5pb25pY3xpby5zY2FuYm90LmV4" +
  "YW1wbGUuc2RrLmNhcGFjaXRvci5pb2" +
  "5pYy5yZWFjdHxpby5zY2FuYm90LmV4" +
  "YW1wbGUuc2RrLmNhcGFjaXRvci5pb2" +
  "5pYy52dWVqc3xpby5zY2FuYm90LmV4" +
  "YW1wbGUuc2RrLmNvcmRvdmEuaW9uaW" +
  "N8aW8uc2NhbmJvdC5leGFtcGxlLnNk" +
  "ay5mbHV0dGVyfGlvLnNjYW5ib3QuZX" +
  "hhbXBsZS5zZGsuaW9zLmJhcmNvZGV8" +
  "aW8uc2NhbmJvdC5leGFtcGxlLnNkay" +
  "5pb3MuY2xhc3NpY3xpby5zY2FuYm90" +
  "LmV4YW1wbGUuc2RrLmlvcy5ydHV1aX" +
  "xpby5zY2FuYm90LmV4YW1wbGUuc2Rr" +
  "Lm1hdWl8aW8uc2NhbmJvdC5leGFtcG" +
  "xlLnNkay5tYXVpLnJ0dXxpby5zY2Fu" +
  "Ym90LmV4YW1wbGUuc2RrLm5ldHxpby" +
  "5zY2FuYm90LmV4YW1wbGUuc2RrLnJl" +
  "YWN0bmF0aXZlfGlvLnNjYW5ib3QuZX" +
  "hhbXBsZS5zZGsucmVhY3QubmF0aXZl" +
  "fGlvLnNjYW5ib3QuZXhhbXBsZS5zZG" +
  "sucnR1LmFuZHJvaWR8aW8uc2NhbmJv" +
  "dC5leGFtcGxlLnNkay54YW1hcmlufG" +
  "lvLnNjYW5ib3QuZXhhbXBsZS5zZGsu" +
  "eGFtYXJpbi5mb3Jtc3xpby5zY2FuYm" +
  "90LmV4YW1wbGUuc2RrLnhhbWFyaW4u" +
  "cnR1fGlvLnNjYW5ib3QuZm9ybXMubm" +
  "F0aXZlcmVuZGVyZXJzLmV4YW1wbGV8" +
  "aW8uc2NhbmJvdC5uYXRpdmViYXJjb2" +
  "Rlc2RrcmVuZGVyZXJ8aW8uc2NhbmJv" +
  "dC5TY2FuYm90U0RLU3dpZnRVSURlbW" +
  "98aW8uc2NhbmJvdC5zZGtfd3JhcHBl" +
  "ci5kZW1vLmJhcmNvZGV8aW8uc2Nhbm" +
  "JvdC5zZGstd3JhcHBlci5kZW1vLmJh" +
  "cmNvZGV8aW8uc2NhbmJvdC5zZGsuaW" +
  "50ZXJuYWxkZW1vfGxvY2FsaG9zdHxP" +
  "cGVyYXRpbmdTeXN0ZW1TdGFuZGFsb2" +
  "5lfHNjYW5ib3RzZGstcWEtMS5zMy1l" +
  "dS13ZXN0LTEuYW1hem9uYXdzLmNvbX" +
  "xzY2FuYm90c2RrLXFhLTIuczMtZXUt" +
  "d2VzdC0xLmFtYXpvbmF3cy5jb218c2" +
  "NhbmJvdHNkay1xYS0zLnMzLWV1LXdl" +
  "c3QtMS5hbWF6b25hd3MuY29tfHNjYW" +
  "5ib3RzZGstcWEtNC5zMy1ldS13ZXN0" +
  "LTEuYW1hem9uYXdzLmNvbXxzY2FuYm" +
  "90c2RrLXFhLTUuczMtZXUtd2VzdC0x" +
  "LmFtYXpvbmF3cy5jb218c2NhbmJvdH" +
  "Nkay13YXNtLWRlYnVnaG9zdC5zMy1l" +
  "dS13ZXN0LTEuYW1hem9uYXdzLmNvbX" +
  "x3ZWJzZGstZGVtby1pbnRlcm5hbC5z" +
  "Y2FuYm90LmlvfCoucWEuc2NhbmJvdC" +
  "5pbwoxNzI1MjM1MTk5CjgzODg2MDcK" +
  "MzE=\n";
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            ScanbotSDKInitialize(builder);

            return builder.Build();
        }

        private static void ScanbotSDKInitialize(MauiAppBuilder mauiApp)
        {
            ScanbotBarcodeSDK.Initialize(mauiApp, new InitializationOptions
            {
                LicenseKey = LicenseKey,
                LoggingEnabled = true,
                ErrorHandler = (status, feature) =>
                {
                    Console.WriteLine($"License error: {status}, {feature}");
                }
            });
        }
    }
}