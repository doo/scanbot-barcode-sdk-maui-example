using System;
namespace BarcodeSDK.MAUI.Example.Common.Models
{
    public class SelectionOverlayConfiguration
    {
        public bool Enabled { get; set; } = true;

        public Color PolygonColor { get; private set; }

        public Color TextColor { get; private set; }

        public Color TextContainerColor { get; private set; }

        public Color? HighlightedPolygonColor { get; set; }

        public Color? HighlightedTextColor { get; set; }

        public Color? HighlightedTextContainerColor { get; set; }

        public SelectionOverlayConfiguration(Color polygon, Color text, Color textContainer,
            Color? highlightedPolygonColor = null, Color? highlightedTextColor = null, Color? highlightedTextContainerColor = null)
        {
            this.PolygonColor = polygon;
            this.TextColor = text;
            this.TextContainerColor = textContainer;
            this.HighlightedPolygonColor = highlightedPolygonColor;
            this.HighlightedTextColor = highlightedTextColor;
            this.HighlightedTextContainerColor = highlightedTextContainerColor;
        }
    }
}

