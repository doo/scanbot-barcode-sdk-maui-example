using System;
using BarcodeSDK.MAUI.Constants;

namespace BarcodeSDK.MAUI.Example.Common.Models
{
    public class Barcode
    {
        public string Text { get; set; }

        public ImageSource Image { get; set; }

        public BarcodeFormat Format { get; set; }

        public BarcodeDocumentFormat Type { get; set; }
    }
}

