using System;

namespace BarcodeSDK.MAUI.Example.Common.Models
{
    public class BarcodeResultBundle : Result
    {
        public List<Barcode> Barcodes { get; set; }

        public string ImagePath { get; set; }

        public ImageSource Image { get; set; }
    }
}

