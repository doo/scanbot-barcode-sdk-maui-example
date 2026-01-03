namespace ScanbotSDK.MAUI.Example;

public partial class App
{
    public static Color ScanbotColor => Color.FromRgb(200, 25, 60);

    public App()
    {
        InitializeComponent();
        
        var navigationPage = new NavigationPage(new HomePage())
        {
            BarBackgroundColor = ScanbotColor,
            BarTextColor = Colors.White
        };
        
        MainPage = navigationPage;
    }
}