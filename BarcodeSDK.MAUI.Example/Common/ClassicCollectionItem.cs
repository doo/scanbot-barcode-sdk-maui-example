namespace ScanbotSDK.MAUI.Example.Common;

using System.ComponentModel;
using System.Runtime.CompilerServices;

public class ClassicCollectionItem : INotifyPropertyChanged
{
    private string _title;
    public string Title 
    {
        get => _title;
        set
        {
            _title = value;
            OnPropertyChanged(nameof(Title));
        }
    }
	
    public Action ClickAction { get; private set; }

    private bool _selected;
    public bool Selected
    {
        get => _selected;
        set
        {
            _selected = value;
            OnPropertyChanged(nameof(Selected));
        }
    }
	
    public ClassicCollectionItem(string title, Action clickAction, bool selected = false)
    {
        Title = title;
        ClickAction = clickAction;
        Selected = selected;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}