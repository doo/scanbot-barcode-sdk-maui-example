using System.Text;
using AndroidX.AppCompat.App;
using AndroidX.ConstraintLayout.Widget;
using IO.Scanbot.Sdk.Barcode.Entity;

namespace BarcodeSDK.NET.Droid.Activities.V1
{
    [Activity(Theme = "@style/AppTheme")]
    public class DetailedItemDataActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.detailed_item_data);
            var toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            var item = Intent.GetParcelableExtra("SelectedBarcodeItem") as BarcodeItem;

            if (item == null)
                return;

            var container = FindViewById<ConstraintLayout>(Resource.Id.container);

            container.FindViewById<ImageView>(Resource.Id.image)
                .SetImageBitmap(item.Image);
            container.FindViewById<TextView>(Resource.Id.barcodeFormat)
                .Text = item.BarcodeFormat?.Name();
            container.FindViewById<TextView>(Resource.Id.description)
                .Text = ParseFormat(item);
        }

        private string ParseFormat(BarcodeItem item)
        {
            return item.Text;
        }
    }
}
