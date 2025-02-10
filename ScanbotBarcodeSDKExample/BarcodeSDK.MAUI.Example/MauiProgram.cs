namespace ScanbotSDK.MAUI.Example
{
    public static class MauiProgram
    {
        // Without a license key, the Scanbot Barcode SDK will work for 1 minute.
        // To scan longer, register for a trial license key here: https://scanbot.io/trial/
        public const string LicenseKey =     "G9pzyJ4ka6JaNtpxOu5YAe5aIPpwAf" +
  "Bb41PbhtxpxRzvGNx//g3U/ohvE6+b" +
  "u8naAp/yrJ475nQDlOpB5c9GCND78L" +
  "ZEs9jPHlSFSuj5ureFpkVo4SdQwexV" +
  "W6CypIl163sX9FWWbjTiJwoapB9i4x" +
  "bYNX1GMH0mIumaiTch3Xn98DgzwXx/" +
  "r525DohivYNVk45JdS7NTYviMRmHvi" +
  "P+YLMVRl88HMqcD7dS93jpmYmzhDTm" +
  "YMfr8UcTmyXe9RjPPRvBrmai46249D" +
  "x0JgQFXYrXWa/KRYrEmrhL/qgvjpxW" +
  "rt3ZREBJYqVo7+Kb6qPDNNBLWQL6Ho" +
  "CNobqKxqFFlA==\nU2NhbmJvdFNESw" +
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
  "hvc3R8T3BlcmF0aW5nU3lzdGVtU3Rh" +
  "bmRhbG9uZXxzY2FuYm90c2RrLXFhLT" +
  "EuczMtZXUtd2VzdC0xLmFtYXpvbmF3" +
  "cy5jb218c2NhbmJvdHNkay1xYS0yLn" +
  "MzLWV1LXdlc3QtMS5hbWF6b25hd3Mu" +
  "Y29tfHNjYW5ib3RzZGstcWEtMy5zMy" +
  "1ldS13ZXN0LTEuYW1hem9uYXdzLmNv" +
  "bXxzY2FuYm90c2RrLXFhLTQuczMtZX" +
  "Utd2VzdC0xLmFtYXpvbmF3cy5jb218" +
  "c2NhbmJvdHNkay1xYS01LnMzLWV1LX" +
  "dlc3QtMS5hbWF6b25hd3MuY29tfHNj" +
  "YW5ib3RzZGstd2FzbS1kZWJ1Z2hvc3" +
  "QuczMtZXUtd2VzdC0xLmFtYXpvbmF3" +
  "cy5jb218d2Vic2RrLWRlbW8taW50ZX" +
  "JuYWwuc2NhbmJvdC5pb3wqLnFhLnNj" +
  "YW5ib3QuaW8KMTc0MDg3MzU5OQo4Mz" +
  "g4NjA3CjMx\n";
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