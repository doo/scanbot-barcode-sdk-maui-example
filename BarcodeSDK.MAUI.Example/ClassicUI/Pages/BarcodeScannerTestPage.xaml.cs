using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScanbotSDK.MAUI.Barcode;
using ScanbotSDK.MAUI.Core.Barcode;
using ScanbotSDK.MAUI.Core.Geometry;
using ScanbotSDK.MAUI.Example.Common;
using ScanbotSDK.MAUI.Example.Results;

namespace ScanbotSDK.MAUI.Example.ClassicUI.Pages;

public partial class BarcodeScannerTestPage : ContentPage
{
    private const string Finder = "Finder", Flash = "Flash", Polygons = "Polygon", Start = "Start", Stop = "Stop", Visibility = "Visibility", Submit = "Submit";
    private bool _showResult;

    private bool _isFlashEnabled;

    public bool IsFlashEnabled
    {
        get => _isFlashEnabled;
        set
        {
            _isFlashEnabled = value;
            OnPropertyChanged();
        }
    }

    private bool _isCameraVisible = true;

    public bool IsCameraVisible
    {
        get => _isCameraVisible;
        set
        {
            _isCameraVisible = value;
            OnPropertyChanged();
        }
    }
    
    private string _barcodeResultText = string.Empty;

    public string BarcodeResultText
    {
        get => _barcodeResultText;
        set
        {
            _barcodeResultText = value;
            OnPropertyChanged();
        }
    }

    private List<ClassicCollectionItem> _scannerButtons = new List<ClassicCollectionItem>();
    public List<ClassicCollectionItem> ScannerButtons
    {
        get => _scannerButtons;
        set
        {
            _scannerButtons = value;
            OnPropertyChanged();
        }
    }
    
    public BarcodeScannerTestPage()
    {
        InitializeComponent();
        // CollectionView Buttons
        ScannerButtons =
        [
            new(Flash, () => IsFlashEnabled = !IsFlashEnabled),
            new(Visibility, () => IsCameraVisible = !IsCameraVisible),
            new(Finder, ToggleFinderConfig),
            new(Polygons, ToggleArOverlayConfig),
            new(Stop, null, selected:true), // This is handled inside the default ScannerButtonOnClicked handler. 
            new(Submit, () => _showResult = true)
        ];
        BindingContext = this;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        BarcodeScanner.StartDetection();
    }

    private void ToggleArOverlayConfig()
    {
        if (BarcodeScanner.OverlayConfiguration == null)
        {
            BarcodeScanner.OverlayConfiguration = new SelectionOverlayConfiguration
            {
                OverlayEnabled =  true,
                PolygonConfiguration = new OverlayPolygonConfiguration.Style
                {
                    StrokeColor = Colors.Yellow,
                    HighlightedStrokeColor = Colors.Red,
                    PolygonColor = Colors.Transparent,
                    HighlightedPolygonColor = Colors.DarkOrchid,
                },
                TextConfiguration = new OverlayTextConfiguration.Style
                {
                    TextFormat = BarcodeTextFormat.CodeAndType,
                    TextColor = Colors.Yellow,
                    TextContainerColor = Colors.Black,
                    HighlightedTextColor = Colors.Red,
                    HighlightedTextContainerColor = Colors.Black,
                }
            };
        }
        else
        {
            BarcodeScanner.OverlayConfiguration = null;
        }
    }

    private void ToggleFinderConfig()
    {
        if (BarcodeScanner.FinderConfiguration == null)
        {
            BarcodeScanner.FinderConfiguration = new FinderConfiguration
            {
                IsFinderEnabled = true,
                FinderLineColor = Colors.DarkCyan,
                FinderLineCornerRadius = 20,
                FinderLineWidth = 10,
                FinderOverlayColor = Colors.Yellow.WithAlpha(0.4f),
                FinderMinimumPadding = 50,
                RequiredAspectRatio = new AspectRatio(1, 1)
            };
        }
        else
        {
            BarcodeScanner.FinderConfiguration = null;
        }
    }

    private void ScannerButtonOnClicked(object sender, EventArgs e)
    {
        var selectedItem = (sender as Button)?.BindingContext as ClassicCollectionItem;
        if (selectedItem == null) return;

        selectedItem.ClickAction?.Invoke();

        if (selectedItem.Title != Start && selectedItem.Title != Stop)
            return;

        // Start and Stop Toggle.
        if (selectedItem.Title == Start)
        {
            selectedItem.Title = Stop;
            BarcodeScanner.UnFreezeCamera();
        }
        else if (selectedItem.Title == Stop)
        {
            selectedItem.Title = Start;
            selectedItem.Selected = false;
            BarcodeScanner.FreezeCamera();
        }
    }

    private async void BarcodeScanner_OnOnBarcodeScanResult(object sender, BarcodeItem[] items)
    {
        if (_showResult)
        {
            BarcodeScanner.StopDetection();
            await Navigation.PushAsync(new BarcodeResultPage(items.ToList()));
            _showResult = false;
        }
        else
        {
            var text  = string.Empty;
            foreach (var item in items)
            {
                text += $"{item.Format}: {item.Text}\n";
            }
            BarcodeResultText = text;
        }
    }
}