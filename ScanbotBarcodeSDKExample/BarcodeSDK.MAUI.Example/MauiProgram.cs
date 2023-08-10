using System;
using BarcodeSDK.MAUI.Example.ClassicComponent;

namespace BarcodeSDK.MAUI.Example
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                }).ConfigureMauiHandlers(handlers =>
                {
                    handlers.AddHandler(typeof(BarcodeCameraView), typeof(BarcodeCameraViewHandler));
                }); ;

            return builder.Build();
        }
    }
}

