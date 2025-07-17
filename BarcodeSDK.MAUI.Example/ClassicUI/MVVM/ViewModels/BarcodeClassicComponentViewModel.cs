using System.Windows.Input;
using ScanbotSDK.MAUI.Barcode;
using ScanbotSDK.MAUI.Barcode.Core;

namespace ScanbotSDK.MAUI.Example.ClassicUI.MVVM.ViewModels;

public class BarcodeClassicComponentViewModel : BaseViewModel
{
    public BarcodeClassicComponentViewModel()
    {
        BarcodeFormatConfigurations =
        [
            new BarcodeFormatCommonConfiguration
            {
                Formats = BarcodeFormats.All
            },

            // You may add more advanced format configurations like shown below
            // new BarcodeFormatAztecConfiguration
            // {
            //     Gs1Handling = Gs1Handling.DecodeStructure,
            //     AddAdditionalQuietZone = true
            // }
        ];

        BarcodeScanResultCommand = new Command<BarcodeItem[]>(OnBarcodeScanResult);
    }

    public List<BarcodeFormatConfigurationBase> BarcodeFormatConfigurations { get; private set; }

    public ICommand BarcodeScanResultCommand { get; private set; }

    private string resultLabel;
    public string ResultLabel
    {
        get => resultLabel;
        set
        {
            resultLabel = value;
            OnPropertyChanged();
        }
    }

    private void OnBarcodeScanResult(BarcodeItem[] barcodeItems)
    {
        if (barcodeItems.Length == 0)
            return;

        string text = string.Empty;
        foreach (var barcode in barcodeItems)
        {
            text += $"{barcode.Text} ({barcode.Format.ToString().ToUpper()})\n";
        }

        System.Diagnostics.Debug.WriteLine(text);
        ResultLabel = text;
    }
}