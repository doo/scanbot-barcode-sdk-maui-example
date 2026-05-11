namespace ScanbotSDK.MAUI.Example.Common;

public class ToggleButton : Button
{
    /// <summary>
    /// Toggle Flash Binding property
    /// </summary>
    public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create(nameof(IsSelected), typeof(bool), typeof(ToggleButton), false, propertyChanged: SelectionPropertyChanged);

    private static void SelectionPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
    {
        if (newvalue is bool newValue)
            (bindable as ToggleButton)?.ButtonEnabled(newValue);
    }

    /// <summary>
    /// Toggle Selected property.
    /// </summary>
    public bool IsSelected
    {
        get => (bool)GetValue(IsSelectedProperty);
        set => SetValue(IsSelectedProperty, value);
    }

    private void ButtonEnabled(bool value)
    {
        BackgroundColor = value ? Colors.LightBlue : Colors.WhiteSmoke;
    }
}