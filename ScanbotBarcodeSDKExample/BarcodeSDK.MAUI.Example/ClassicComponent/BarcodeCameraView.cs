using BarcodeSDK.MAUI.Configurations;
using BarcodeSDK.MAUI.Models;

namespace BarcodeSDK.MAUI.Example.ClassicComponent
{        /*
            This is the View that will be referenced in the XAML to integrate our native Camera View.
            The class itself does not provide any specific implementation; the actual implementation
            is achieved through the use of Custom Renderers.
            Renderers are platform specific and they are implemented in the relative native projects.
            Take a look at AndroidBarcodeCameraRenderer.cs to see how the implementation is done.
         */
    public class BarcodeCameraView : View
    {
        // This is the delegate that will be used from our native controller to
        // notify us that the scanner has returned a valid result.
        // We can set this from our Page class to implement a custom behavior.
        public delegate void BarcodeScannerResultHandler(BarcodeResultBundle result);
        public BarcodeScannerResultHandler OnBarcodeScanResult;

        /// <summary>
        /// Shows an AR overlay in the camera view.
        /// </summary>
        public SelectionOverlayConfiguration OverlayConfiguration { get; set; }

        /// <summary>
        /// Toggle Flash Binding property
        /// </summary>
        public static readonly BindableProperty IsFlashEnabledProperty =
          BindableProperty.Create("IsFlashEnabled", typeof(bool), typeof(BarcodeCameraView), false);

        /// <summary>
        /// Toggle Flash property.
        /// </summary>
        public bool IsFlashEnabled
        {
            get { return (bool)GetValue(IsFlashEnabledProperty); }
            set { SetValue(IsFlashEnabledProperty, value); }
        }

        // This event is defined from our native control through the Custom Renderer.
        // We call this from our Page in accord to the view lifecycle (OnAppearing)
        public EventHandler<EventArgs> OnResumeHandler;
        public void Resume()
        {
            OnResumeHandler?.Invoke(this, EventArgs.Empty);
            Handler?.Invoke(nameof(BarcodeCameraView.OnResumeHandler), EventArgs.Empty);
        }

        // This event is defined from our native control through the Custom Renderer.
        // We call this from our Page in accord to the view lifecycle (OnDisappearing)
        public EventHandler<EventArgs> OnPauseHandler;
        public void Pause()
        {
            OnPauseHandler?.Invoke(this, EventArgs.Empty);
            Handler?.Invoke(nameof(BarcodeCameraView.OnPauseHandler), EventArgs.Empty);
        }

        // This event is defined from our native control through the Custom Renderer.
        // We call this from our Page when we want to start detecting barcodes.
        public EventHandler<EventArgs> StartDetectionHandler;
        public void StartDetection()
        {
            StartDetectionHandler?.Invoke(this, EventArgs.Empty);
            Handler?.Invoke(nameof(BarcodeCameraView.StartDetectionHandler), EventArgs.Empty);
        }

        // This event is defined from our native control through the Custom Renderer.
        // We call this from our Page when we want to stop detecting barcodes.
        public EventHandler<EventArgs> StopDetectionHandler;
        public void StopDetection()
        {
            StopDetectionHandler?.Invoke(this, EventArgs.Empty);
            Handler?.Invoke(nameof(BarcodeCameraView.StopDetectionHandler), EventArgs.Empty);
        }

        public BarcodeCameraView()
        {
            OnBarcodeScanResult = HandleBarcodeScanResult;
        }

        private void HandleBarcodeScanResult(BarcodeResultBundle result)
        {
            // If we don't implement the delegate from our Page class, this method
            // will be called instead as a fallback mechanism.
        }
    }
}
