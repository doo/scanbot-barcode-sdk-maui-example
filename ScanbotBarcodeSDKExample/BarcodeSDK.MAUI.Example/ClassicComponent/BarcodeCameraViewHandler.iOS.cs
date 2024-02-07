using ScanbotSDK.MAUI.Example.Platforms.iOS.CustomViews;
using Microsoft.Maui.Handlers;

namespace ScanbotSDK.MAUI.Example.ClassicComponent
{
    public partial class BarcodeCameraViewHandler : ViewHandler<BarcodeCameraView, BarcodeCameraView_iOS>
    {
        protected override BarcodeCameraView_iOS CreatePlatformView() => new BarcodeCameraView_iOS(this.VirtualView.Frame);

        protected override void ConnectHandler(BarcodeCameraView_iOS platformView)
        {
            base.ConnectHandler(platformView);
            platformView.ConnectHandler(this);
        }

        protected override void DisconnectHandler(BarcodeCameraView_iOS platformView)
        {
            base.DisconnectHandler(platformView);
        }

        protected override void SetupContainer()
        {
            base.SetupContainer();
        }

        protected override void RemoveContainer()
        {
            base.RemoveContainer();
        }

        public static void MapOverlayConfiguration(BarcodeCameraViewHandler current, BarcodeCameraView commonView)
        {
            current?.PlatformView?.MapOverlayConfiguration(commonView);
        }

        public static void MapIsFlashEnabled(BarcodeCameraViewHandler current, BarcodeCameraView commonView)
        {
            current?.PlatformView?.MapIsFlashEnabled(commonView.IsFlashEnabled);
        }

        public static void MapStartDetectionHandler(BarcodeCameraViewHandler current, BarcodeCameraView commonView, object arg3)
        {
            current?.PlatformView?.MapStartDetectionHandler();
        }

        public static void MapStopDetectionHandler(BarcodeCameraViewHandler current, BarcodeCameraView commonView, object arg3)
        {
            current?.PlatformView?.MapStopDetectionHandler();
        }

        public static void MapOnPauseHandler(BarcodeCameraViewHandler current, BarcodeCameraView commonView, object arg3)
        {
            // This is the common method declared in BarcodeCameraViewHandler, and it is useful for the Android native component only.
            // Based on the MauiHandler concept, we have to define it on all the platforms.
        }

        public static void MapOnResumeHandler(BarcodeCameraViewHandler current, BarcodeCameraView commonView, object arg3)
        {
            // This is the common method declared in BarcodeCameraViewHandler, and it is useful for the Android native component only.
            // Based on the MauiHandler concept, we have to define it on all the platforms.
        }
    }
}

