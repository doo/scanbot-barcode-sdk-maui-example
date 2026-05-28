using _Microsoft.Android.Resource.Designer;
using Android.Views;
using AndroidX.AppCompat.App;
using AndroidX.Core.View;
using AndroidX.RecyclerView.Widget;
using IO.Scanbot.Sdk.Barcode;

namespace BarcodeSDK.NET.Droid.Activities;

public class BarcodeDetailsModel(string name, string value)
{
    public string PropertyName { get; set; } = name;

    public string PropertyValue { get; set; } = value;
}

[Activity(Theme = "@style/AppTheme")]
public partial class DetailedItemDataActivity : AppCompatActivity, IOnApplyWindowInsetsListener
{
    private List<BarcodeDetailsModel> _barcodeDetailList;
    private const string SelectedBarcodeItemKey = "SelectedBarcodeItem";

    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        SetContentView(ResourceConstant.Layout.detailed_item_data);
        AndroidUtils.ApplyEdgeToEdge(FindViewById(ResourceConstant.Id.container), this);

        var toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(ResourceConstant.Id.toolbar);
        SetSupportActionBar(toolbar);

        if (Intent!.GetParcelableExtra(SelectedBarcodeItemKey) is not BarcodeItem item)
        {
            return;
        }

        _barcodeDetailList =
        [
            new BarcodeDetailsModel(nameof(item.Format), item.Format.Name()),
            new BarcodeDetailsModel(nameof(item.Text), item.Text),
        ];

        if (!string.IsNullOrEmpty(item.UpcEanExtension))
        {
            _barcodeDetailList.Add(new BarcodeDetailsModel("Extension", item.UpcEanExtension));
        }

        if (item.ExtractedDocument != null)
        {
            _barcodeDetailList.AddRange(ParseDocument(item.ExtractedDocument));
        }

        var recyclerView = FindViewById<RecyclerView>(ResourceConstant.Id.recycler_view_barcode_details);

        if (recyclerView == null) return;

        recyclerView.SetAdapter(new BarcodeDetailListAdapter(_barcodeDetailList));
        
        var decoration = new DividerItemDecoration(this, DividerItemDecoration.Vertical);
        recyclerView.AddItemDecoration(decoration);

        var manager = new LinearLayoutManager(this);
        recyclerView.SetLayoutManager(manager);
    }
    
    public WindowInsetsCompat OnApplyWindowInsets(View v, WindowInsetsCompat windowInsets)
    {
        return AndroidUtils.ApplyWindowInsets(v, windowInsets);
    }
}

public class BarcodeDetailListAdapter(List<BarcodeDetailsModel> list) : RecyclerView.Adapter
{
    public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
    {
        if (holder is BarcodeDetailListItemHolder listItem)
        {
            listItem.PopulateData(list[position].PropertyName, list[position].PropertyValue);
        }
    }

    public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
    {
        var inflater = LayoutInflater.From(parent.Context);
        var view = inflater?.Inflate(ResourceConstant.Layout.barcode_detail_item, parent, false);
        return new BarcodeDetailListItemHolder(view);
    }

    public override int ItemCount => list.Count;
}

public class BarcodeDetailListItemHolder: RecyclerView.ViewHolder
{
    public TextView PropertyName { get; private set; }

    public TextView PropertyValue { get; private set; }

    public BarcodeDetailListItemHolder(View item) : base(item)
    {
        PropertyName = item.FindViewById<TextView>(ResourceConstant.Id.property_name);
        PropertyValue = item.FindViewById<TextView>(ResourceConstant.Id.property_value);
    }
    
    internal void PopulateData(string name, string value)
    {
        PropertyName.Text = name;
        PropertyValue.Text = value;
    }
}
