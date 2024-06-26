using Android.Views;
using AndroidX.AppCompat.App;
using AndroidX.RecyclerView.Widget;

namespace BarcodeSDK.NET.Droid.Activities
{
    [Activity(Theme = "@style/AppTheme")]
    public class BarcodeTypesActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.barcode_types);

            var list = FindViewById<RecyclerView>(Resource.Id.barcode_types_list);
            list.HasFixedSize = true;

            var decoration = new DividerItemDecoration(this, DividerItemDecoration.Vertical);
            list.AddItemDecoration(decoration);

            var manager = new LinearLayoutManager(this);
            list.SetLayoutManager(manager);

            var adapter = new BarcodeTypesAdapter();
            list.SetAdapter(adapter);

            FindViewById<View>(Resource.Id.apply).Click += OnApplyClick;
        }

        public void OnApplyClick(object sender, EventArgs e)
        {
            Finish();
        }
    }
}
