namespace BarcodeSDK.NET.iOS
{
    public class MainView : UIView
    {
        public UIButton ClassicButton { get; private set; }

        public UIButton ClassicScanAndCountButton { get; private set; }

        public UIButton RTUUIButton { get; private set; }

        public UIButton RTUUIImageButton { get; private set; }

        public UIButton RTUUIBatchBarcodeButton { get; private set; }

        public UIButton LibraryButton { get; private set; }

        public UIButton CodeTypesButton { get; private set; }

        public UIButton StorageClearButton { get; private set; }

        public UIButton LicenseInfoButton { get; private set; }

        private readonly List<UIButton> buttons = new List<UIButton>();

        public MainView()
        {
            BackgroundColor = UIColor.White;

            ClassicButton = CreateButton("CLASSIC COMPONENT");

            ClassicScanAndCountButton = CreateButton("CLASSIC SCAN AND COUNT COMPONENT");

            RTUUIButton = CreateButton("RTU UI - BARCODE SCANNER");

            RTUUIImageButton = CreateButton("RTU UI – WITH BARCODE IMAGE");

            RTUUIBatchBarcodeButton = CreateButton("RTU UI - BATCH BARCODE SCANNER");

            LibraryButton = CreateButton("PICK IMAGE FROM LIBRARY");

            CodeTypesButton = CreateButton("SET ACCEPTED BARCODE TYPES");

            StorageClearButton = CreateButton("CLEAR IMAGE STORAGE");

            LicenseInfoButton = CreateButton("VIEW LICENSE INFO");
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            nfloat padding = 10;

            nfloat x = padding;
            nfloat y = padding;
            nfloat w = Frame.Width - 2 * padding;
            nfloat h = w / 7.5f;

            foreach (var button in buttons)
            {
                button.Frame = new CGRect(x, y, w, h);
                y += h + padding;
            }
        }

        private UIButton CreateButton(string text)
        {
            var button = new UIButton();
            button.SetTitle(text, UIControlState.Normal);
            button.SetTitleColor(UIColor.FromRGB(10, 132, 255), UIControlState.Normal);
            button.TitleLabel.Font = UIFont.FromName("HelveticaNeue", 14);
            AddSubview(button);
            buttons.Add(button);

            return button;
        }
    }
}
