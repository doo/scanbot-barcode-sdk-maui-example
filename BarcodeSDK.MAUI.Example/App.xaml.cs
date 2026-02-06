namespace ScanbotSDK.MAUI.Example;

public partial class App
{
    /// <summary>
    /// Returns the NavigationPage object by accessing the singleton CurrentWindow instance.
    /// </summary>
    public static NavigationPage Navigation => CurrentWindow?.Page as NavigationPage;

    private static Window _currentWindow;
    /// <summary>
    /// Singleton Window object to retrieve the Navigation of the App.
    /// </summary>
    public static Window CurrentWindow
    {
        get
        {
            if (_currentWindow == null)
            {
                _currentWindow = new Window(new NavigationPage(new HomePage())
                {
                    BarBackgroundColor = ScanbotColor,
                    BarTextColor = Colors.White
                });
            }

            return _currentWindow;
        }
    }
    
    public static Color ScanbotColor => Color.FromRgb(200, 25, 60);

    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState activationState) => CurrentWindow;
}