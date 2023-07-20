using Android.Content;
using Android.Graphics;
using AndroidX.AppCompat.App;
using IO.Scanbot.Sdk.Barcode.Entity;

namespace BarcodeSDK.NET.Droid
{
    [Activity(Theme = "@style/AppTheme")]
    public class BarcodeResultActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.barcode_result);

            var toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            string imagePath = null;

            if (BarcodeResultBundle.Instance.PreviewPath != null)
            {
                imagePath = BarcodeResultBundle.Instance.PreviewPath;
            }
            else if (BarcodeResultBundle.Instance.ImagePath != null)
            {
                imagePath = BarcodeResultBundle.Instance.ImagePath;
            }

            if (imagePath != null)
            {
                ShowSnapImageFromPath(imagePath);
            }
            else if (BarcodeResultBundle.Instance.ResultBitmap != null)
            {
                ShowSnapImageFromBitmap();
            }

            ShowBarcodeResult(BarcodeResultBundle.Instance.ScanningResult);
        }

        void ShowSnapImageFromPath(string path)
        {
            AddImageView().SetImageURI(Android.Net.Uri.Parse(path));
        }

        void ShowSnapImageFromBitmap()
        {
            var original = BarcodeResultBundle.Instance.ResultBitmap;
            Bitmap scaled = Bitmap.CreateScaledBitmap(original, 200, 200, true);
            AddImageView().SetImageBitmap(scaled);
        }

        ImageView AddImageView()
        {
            var items = FindViewById<LinearLayout>(Resource.Id.recognisedItems);

            var view = LayoutInflater.Inflate(Resource.Layout.snap_image_item, items, false);
            items.AddView(view);

            return view.FindViewById<ImageView>(Resource.Id.snapImage);
        }

        void ShowBarcodeResult(BarcodeScanningResult result)
        {
            var parent = FindViewById<LinearLayout>(Resource.Id.recognisedItems);

            if (result == null)
            {
                return;
            }

            foreach (var item in result.BarcodeItems)
            {
                var child = LayoutInflater.Inflate(Resource.Layout.barcode_item, parent, false);

                var image = child.FindViewById<ImageView>(Resource.Id.image);
                var barFormat = child.FindViewById<TextView>(Resource.Id.barcodeFormat);
                var docFormat = child.FindViewById<TextView>(Resource.Id.docFormat);
                var docText = child.FindViewById<TextView>(Resource.Id.docText);

                if (item.Image != null)
                {
                    image.SetImageBitmap(item.Image);
                }
                
                barFormat.Text = "Format: " + item.BarcodeFormat.Name();

                if (item.FormattedData != null)
                {
                    docFormat.Text = item.FormattedData.ToString();
                }
                else
                {
                    docFormat.Text = "Document: –";
                }
                
                docText.Text = "Content: " + item.Text;

                child.Click += delegate
                {
                    BarcodeResultBundle.SelectedBarcodeItem = item;
                    var intent = new Intent(this, typeof(DetailedItemDataActivity));
                    StartActivity(intent);
                };

                parent.AddView(child);
            }
        }
    }
}
