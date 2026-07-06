using _Microsoft.Android.Resource.Designer;
using Android.Views;
using AndroidX.AppCompat.App;
using AndroidX.Core.View;
using AndroidX.RecyclerView.Widget;

namespace BarcodeSDK.NET.Droid.Activities;

[Activity(Theme = "@style/AppTheme")]
public class BarcodeTypesActivity : AppCompatActivity, IOnApplyWindowInsetsListener
{
    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
            
        SetContentView(ResourceConstant.Layout.barcode_types);
        AndroidUtils.ApplyEdgeToEdge(FindViewById(ResourceConstant.Id.container), this);

        var list = FindViewById<RecyclerView>(ResourceConstant.Id.barcode_types_list)!;
        list.HasFixedSize = true;

        var decoration = new DividerItemDecoration(this, DividerItemDecoration.Vertical);
        list.AddItemDecoration(decoration);

        var manager = new LinearLayoutManager(this);
        list.SetLayoutManager(manager);

        var adapter = new BarcodeTypesAdapter();
        list.SetAdapter(adapter);

        FindViewById<View>(ResourceConstant.Id.apply)!.Click += OnApplyClick;
    }

    public void OnApplyClick(object sender, EventArgs e)
    {
        Finish();
    }
        
    public WindowInsetsCompat OnApplyWindowInsets(View v, WindowInsetsCompat windowInsets)
    {
        return AndroidUtils.ApplyWindowInsets(v, windowInsets);
    }
}