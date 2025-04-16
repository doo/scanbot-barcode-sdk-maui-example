namespace ScanbotSDK.MAUI.Example
{
    public static class MauiProgram
    {
        // Without a license key, the Scanbot Barcode SDK will work for 1 minute.
        // To scan longer, register for a trial license key here: https://scanbot.io/trial/
        public const string LicenseKey =       "fTCEeZDmpAP06/KQtze6/zSK4HOe00" +
  "KLrU+JbT9EadupcrN8TKJ5rfWx0VVY" +
  "rW3Mbe49p6Jnfo6JJzwSVJZT/MAZCt" +
  "IEcTgtR5VXW1i3K9sN7hD9EkaHanSB" +
  "Y9xzSKaZIDa71EJ1PtbfYZ3GS5X0xV" +
  "B+MBFkyBrQbHVJbBYvOB/12go1oMry" +
  "2DbcMazF05pFJIz3xYSk0VOEhYVTZG" +
  "duFna9Ntf/qiCQnREO7lXUZf41TelJ" +
  "qqJcb4ypqqtqgKlTGYkNuX0M+6WOde" +
  "QDaf3OYnF4G5TnXoB/5AbiwSALntAN" +
  "DZn92sbdeSmy15R2HCqeUzbQ7oyH+z" +
  "LQbccChqnV6A==\nU2NhbmJvdFNESw" +
  "pkb28uc2NhbmJvdC5jYXBhY2l0b3Iu" +
  "ZXhhbXBsZXxpby5zY2FuYm90LmV4YW" +
  "1wbGUuZG9jdW1lbnQudXNlY2FzZXMu" +
  "YW5kcm9pZHxpby5zY2FuYm90LmV4YW" +
  "1wbGUuZG9jdW1lbnRzZGsudXNlY2Fz" +
  "ZXMuaW9zfGlvLnNjYW5ib3QuZXhhbX" +
  "BsZS5mbHV0dGVyfGlvLnNjYW5ib3Qu" +
  "ZXhhbXBsZS5zZGsuYW5kcm9pZHxpby" +
  "5zY2FuYm90LmV4YW1wbGUuc2RrLmJh" +
  "cmNvZGUuYW5kcm9pZHxpby5zY2FuYm" +
  "90LmV4YW1wbGUuc2RrLmJhcmNvZGUu" +
  "Y2FwYWNpdG9yfGlvLnNjYW5ib3QuZX" +
  "hhbXBsZS5zZGsuYmFyY29kZS5mbHV0" +
  "dGVyfGlvLnNjYW5ib3QuZXhhbXBsZS" +
  "5zZGsuYmFyY29kZS5pb25pY3xpby5z" +
  "Y2FuYm90LmV4YW1wbGUuc2RrLmJhcm" +
  "NvZGUubWF1aXxpby5zY2FuYm90LmV4" +
  "YW1wbGUuc2RrLmJhcmNvZGUubmV0fG" +
  "lvLnNjYW5ib3QuZXhhbXBsZS5zZGsu" +
  "YmFyY29kZS5yZWFjdG5hdGl2ZXxpby" +
  "5zY2FuYm90LmV4YW1wbGUuc2RrLmJh" +
  "cmNvZGUud2luZG93c3xpby5zY2FuYm" +
  "90LmV4YW1wbGUuc2RrLmJhcmNvZGUu" +
  "eGFtYXJpbnxpby5zY2FuYm90LmV4YW" +
  "1wbGUuc2RrLmJhcmNvZGUueGFtYXJp" +
  "bi5mb3Jtc3xpby5zY2FuYm90LmV4YW" +
  "1wbGUuc2RrLmNhcGFjaXRvcnxpby5z" +
  "Y2FuYm90LmV4YW1wbGUuc2RrLmNhcG" +
  "FjaXRvci5hbmd1bGFyfGlvLnNjYW5i" +
  "b3QuZXhhbXBsZS5zZGsuY2FwYWNpdG" +
  "9yLmlvbmljfGlvLnNjYW5ib3QuZXhh" +
  "bXBsZS5zZGsuY2FwYWNpdG9yLmlvbm" +
  "ljLnJlYWN0fGlvLnNjYW5ib3QuZXhh" +
  "bXBsZS5zZGsuY2FwYWNpdG9yLmlvbm" +
  "ljLnZ1ZWpzfGlvLnNjYW5ib3QuZXhh" +
  "bXBsZS5zZGsuY29yZG92YS5pb25pY3" +
  "xpby5zY2FuYm90LmV4YW1wbGUuc2Rr" +
  "LmZsdXR0ZXJ8aW8uc2NhbmJvdC5leG" +
  "FtcGxlLnNkay5pb3MuYmFyY29kZXxp" +
  "by5zY2FuYm90LmV4YW1wbGUuc2RrLm" +
  "lvcy5jbGFzc2ljfGlvLnNjYW5ib3Qu" +
  "ZXhhbXBsZS5zZGsuaW9zLnJ0dXVpfG" +
  "lvLnNjYW5ib3QuZXhhbXBsZS5zZGsu" +
  "bWF1aXxpby5zY2FuYm90LmV4YW1wbG" +
  "Uuc2RrLm1hdWkucnR1fGlvLnNjYW5i" +
  "b3QuZXhhbXBsZS5zZGsubmV0fGlvLn" +
  "NjYW5ib3QuZXhhbXBsZS5zZGsucmVh" +
  "Y3RuYXRpdmV8aW8uc2NhbmJvdC5leG" +
  "FtcGxlLnNkay5yZWFjdC5uYXRpdmV8" +
  "aW8uc2NhbmJvdC5leGFtcGxlLnNkay" +
  "5ydHUuYW5kcm9pZHxpby5zY2FuYm90" +
  "LmV4YW1wbGUuc2RrLnhhbWFyaW58aW" +
  "8uc2NhbmJvdC5leGFtcGxlLnNkay54" +
  "YW1hcmluLmZvcm1zfGlvLnNjYW5ib3" +
  "QuZXhhbXBsZS5zZGsueGFtYXJpbi5y" +
  "dHV8aW8uc2NhbmJvdC5mb3Jtcy5uYX" +
  "RpdmVyZW5kZXJlcnMuZXhhbXBsZXxp" +
  "by5zY2FuYm90Lm5hdGl2ZWJhcmNvZG" +
  "VzZGtyZW5kZXJlcnxpby5zY2FuYm90" +
  "LlNjYW5ib3RTREtTd2lmdFVJRGVtb3" +
  "xpby5zY2FuYm90LnNka193cmFwcGVy" +
  "LmRlbW8uYmFyY29kZXxpby5zY2FuYm" +
  "90LnNkay13cmFwcGVyLmRlbW8uYmFy" +
  "Y29kZXxpby5zY2FuYm90LnNka193cm" +
  "FwcGVyLmRlbW8uZG9jdW1lbnR8aW8u" +
  "c2NhbmJvdC5zZGstd3JhcHBlci5kZW" +
  "1vLmRvY3VtZW50fGlvLnNjYW5ib3Qu" +
  "c2RrLmludGVybmFsZGVtb3xsb2NhbG" +
  "hvc3R8c2NhbmJvdHNkay1xYS0xLnMz" +
  "LWV1LXdlc3QtMS5hbWF6b25hd3MuY2" +
  "9tfHNjYW5ib3RzZGstcWEtMi5zMy1l" +
  "dS13ZXN0LTEuYW1hem9uYXdzLmNvbX" +
  "xzY2FuYm90c2RrLXFhLTMuczMtZXUt" +
  "d2VzdC0xLmFtYXpvbmF3cy5jb218c2" +
  "NhbmJvdHNkay1xYS00LnMzLWV1LXdl" +
  "c3QtMS5hbWF6b25hd3MuY29tfHNjYW" +
  "5ib3RzZGstcWEtNS5zMy1ldS13ZXN0" +
  "LTEuYW1hem9uYXdzLmNvbXxzY2FuYm" +
  "90c2RrLXdhc20tZGVidWdob3N0LnMz" +
  "LWV1LXdlc3QtMS5hbWF6b25hd3MuY2" +
  "9tfHdlYnNkay1kZW1vLWludGVybmFs" +
  "LnNjYW5ib3QuaW98Ki5xYS5zY2FuYm" +
  "90LmlvCjE3NDYxNDM5OTkKODM4ODYw" +
  "NwozMQ==\n";
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