using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.App;
using AndroidX.RecyclerView.Widget;
using IO.Scanbot.Sdk.Barcode;
using JetBrains.Annotations;

namespace BarcodeSDK.NET.Droid.Activities;

public class BarcodeDetailsModel(string name, string value)
{
    public string PropertyName { get; set; } = name;

    public string PropertyValue { get; set; } = value;
}

[Activity(Theme = "@style/AppTheme")]
public partial class DetailedItemDataActivity : AppCompatActivity
{
    private List<BarcodeDetailsModel> BarcodeDetailList;

    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        SetContentView(Resource.Layout.detailed_item_data);

        var toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar);
        SetSupportActionBar(toolbar);

        var item = Intent.GetParcelableExtra("SelectedBarcodeItem") as BarcodeItem;
        if (item == null)
        {
            return;
        }

        BarcodeDetailList = new List<BarcodeDetailsModel>
        {
            new BarcodeDetailsModel(nameof(item.Format), item.Format.Name()),
            new BarcodeDetailsModel(nameof(item.Text), item.Text),
        };

        if (!string.IsNullOrEmpty(item.UpcEanExtension))
        {
            BarcodeDetailList.Add(new BarcodeDetailsModel("Extension", item.UpcEanExtension));
        }

        if (item.ExtractedDocument != null)
        {
            BarcodeDetailList.AddRange(ParseDocument(item.ExtractedDocument));
        }

        var recyclerView = FindViewById<RecyclerView>(Resource.Id.recycler_view_barcode_details);

        if (recyclerView == null) return;

        recyclerView.SetAdapter(new BarcodeDetailListAdapter(BarcodeDetailList));
        
        var decoration = new DividerItemDecoration(this, DividerItemDecoration.Vertical);
        recyclerView.AddItemDecoration(decoration);

        var manager = new LinearLayoutManager(this);
        recyclerView.SetLayoutManager(manager);
    }
}

public class BarcodeDetailListAdapter : RecyclerView.Adapter
{
    private readonly List<BarcodeDetailsModel> barcodeDetailList;
    public BarcodeDetailListAdapter(List<BarcodeDetailsModel> list)
    {
        barcodeDetailList = list;
    }

    public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
    {
        if (holder is BarcodeDetailListItemHolder listItem)
        {
            listItem.PopulateData(barcodeDetailList[position].PropertyName, barcodeDetailList[position].PropertyValue);
        }
    }

    public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
    {
        var inflater = LayoutInflater.From(parent.Context);
        var view = inflater.Inflate(Resource.Layout.barcode_detail_item, parent, false);
        return new BarcodeDetailListItemHolder(view);
    }

    public override int ItemCount => barcodeDetailList.Count;
}

public class BarcodeDetailListItemHolder: RecyclerView.ViewHolder
{
    public TextView PropertyName { get; private set; }

    public TextView PropertyValue { get; private set; }

    public BarcodeDetailListItemHolder(View item) : base(item)
    {
        PropertyName = item.FindViewById<TextView>(Resource.Id.property_name);
        PropertyValue = item.FindViewById<TextView>(Resource.Id.property_value);
    }
    
    internal void PopulateData(string name, string value)
    {
        PropertyName.Text = name;
        PropertyValue.Text = value;
    }
}
