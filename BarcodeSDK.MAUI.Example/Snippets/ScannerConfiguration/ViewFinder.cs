using ScanbotSDK.MAUI.Barcode;
using ScanbotSDK.MAUI.Common;
using ScanbotSDK.MAUI.Core.Geometry;

namespace ScanbotSDK.MAUI.Example;

public partial class Snippets
{
    public static BarcodeScannerScreenConfiguration ViewFinder
    {
        get
        {
            // Create the default configuration object.
            var configuration = new BarcodeScannerScreenConfiguration();

            // Show the view finder
            configuration.ViewFinder.Visible = true;

            // Set the aspect ratio of the view finder
            configuration.ViewFinder.AspectRatio =
                new AspectRatio(width: 16.0, height: 9.0);

            configuration.ViewFinder.Style = new FinderCorneredStyle
            {
                // Set the color of the view finder corners
                StrokeColor = new ColorValue("#FF0005"),
                // Set the width of the view finder corners
                StrokeWidth = 5.0
            };

            // Configure other parameters as needed.

            return configuration;
        }
    }
}