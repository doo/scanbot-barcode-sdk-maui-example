using ScanbotSDK.iOS;

namespace BarcodeSDK.NET.iOS;

internal class BarcodeDetailModel(string caption, string text)
{
    internal BarcodeDetailModel(string caption, SBSDKGenericDocumentField field) : this(caption, field?.Value?.Text ?? "--")
    {
    }

    public string Caption { get; set; } = caption;

    public string Text { get; set; } = text;
}

public partial class BarcodeDetailsController : BaseViewController
{
    private List<BarcodeDetailModel> barcodeDetails;
    private readonly SBSDKBarcodeItem barcode;
    
    private UITableView tableView;
    private const int ImageHeight = 100;
    
    public BarcodeDetailsController(SBSDKBarcodeItem barcode)
    {
        PageTitle = "Barcode Details";
        this.barcode = barcode;
        PopulateData(barcode);
    }

    public override void ViewDidLoad()
    {
        base.ViewDidLoad();

        var topImageView = new UIImageView
        {
            ContentMode = UIViewContentMode.ScaleAspectFit,
            TranslatesAutoresizingMaskIntoConstraints = false,
            Image = barcode?.SourceImage?.ToUIImageAndReturnError(out _)
        };

        tableView = new UITableView
        {
            Source = new BarcodeDetailsTableViewSource(barcodeDetails),
            AllowsSelection = false,
            RowHeight = UITableView.AutomaticDimension,
            EstimatedRowHeight = 100,
            TableFooterView = new UIView
            {
                BackgroundColor = UIColor.Red
            },
            TranslatesAutoresizingMaskIntoConstraints = false,
        };
        
        tableView.RegisterClassForCellReuse(typeof(BarcodeDetailsTableViewCell), nameof(BarcodeDetailsTableViewCell));
        View.BackgroundColor = UIColor.White;

        View.AddSubviews(topImageView, tableView);
        NSLayoutConstraint.ActivateConstraints([
            topImageView.TopAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.TopAnchor),
            topImageView.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor),
            topImageView.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor),
            topImageView.HeightAnchor.ConstraintEqualTo(ImageHeight),

            tableView.TopAnchor.ConstraintEqualTo(topImageView.BottomAnchor),
            tableView.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor),
            tableView.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor),
            tableView.BottomAnchor.ConstraintEqualTo(View.BottomAnchor)
        ]);
    }
    
    private void PopulateData(SBSDKBarcodeItem barcode)
    {
        barcodeDetails =
        [
            new(caption: "Format", text: barcode.Format.Name),
            new(caption: "Text", text: barcode.Text),
        ];

        if (!string.IsNullOrEmpty(barcode.UpcEanExtension))
        {
            barcodeDetails.Add(new(caption: "Extension", text: barcode.UpcEanExtension));
        }

        if (barcode.ExtractedDocument != null)
        {
            GetFormattedDocument(barcode.ExtractedDocument);
        }
    }
}

internal class BarcodeDetailsTableViewSource(List<BarcodeDetailModel> barcodeDetailList) : UITableViewSource
{
    public override IntPtr RowsInSection(UITableView tableView, IntPtr section) => barcodeDetailList.Count;

    public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
    {
        var cell =
            tableView.DequeueReusableCell(nameof(BarcodeDetailsTableViewCell),
                indexPath) as BarcodeDetailsTableViewCell;
        cell?.PopulateData(barcodeDetailList[indexPath.Row]);
        return cell ?? new UITableViewCell();
    }
}

internal class BarcodeDetailsTableViewCell : UITableViewCell
{
    private UILabel labelCaption, labelValue;

    public BarcodeDetailsTableViewCell(IntPtr handle) : base(handle)
    {
        InitializeCell();
    }

    void InitializeCell()
    {
        base.AwakeFromNib();
        labelCaption = new UILabel
        {
            TextAlignment = UITextAlignment.Left,
            Font = UIFont.BoldSystemFontOfSize(18f),
            TranslatesAutoresizingMaskIntoConstraints = false,
            Lines = new IntPtr(0),
            LineBreakMode = UILineBreakMode.WordWrap
        };

        labelValue = new UILabel
        {
            TextAlignment = UITextAlignment.Left,
            Font = UIFont.SystemFontOfSize(18f),
            TranslatesAutoresizingMaskIntoConstraints = false,
            Lines = new IntPtr(0),
            LineBreakMode = UILineBreakMode.WordWrap
        };

        ContentView.AddSubviews(labelCaption, labelValue);

        NSLayoutConstraint.ActivateConstraints([
            labelCaption.TopAnchor.ConstraintEqualTo(ContentView.TopAnchor, 8),
            labelCaption.LeadingAnchor.ConstraintEqualTo(ContentView.LeadingAnchor, 16),
            labelCaption.TrailingAnchor.ConstraintEqualTo(ContentView.TrailingAnchor, -16),

            labelValue.TopAnchor.ConstraintEqualTo(labelCaption.BottomAnchor, 4),
            labelValue.LeadingAnchor.ConstraintEqualTo(labelCaption.LeadingAnchor),
            labelValue.TrailingAnchor.ConstraintEqualTo(labelCaption.TrailingAnchor),
            labelValue.BottomAnchor.ConstraintEqualTo(ContentView.BottomAnchor, -8)
        ]);
    }

    public void PopulateData(BarcodeDetailModel barcodeDetailModel)
    {
        labelCaption.Text = barcodeDetailModel.Caption;
        labelValue.Text = barcodeDetailModel.Text;
    }
}