namespace ClassicComponent.MAUI.Legacy;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new NavigationPage(new BarcodeCustomClassicComponentPage());
    }
}