using ScanbotSDK.MAUI.Example.ClassicUI.MVVM.ViewModels;

namespace ScanbotSDK.MAUI.Example.ClassicUI.MVVM.Views;

public partial class BarcodeClassicComponentView : ContentPage
{
    public BarcodeClassicComponentView()
    {
        BindingContext = new BarcodeClassicComponentViewModel();
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Start barcode detection manually
        CameraView.StartDetection();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        // Stop barcode detection manually
        CameraView.StopDetection();

        CameraView.Handler?.DisconnectHandler();
    }
}