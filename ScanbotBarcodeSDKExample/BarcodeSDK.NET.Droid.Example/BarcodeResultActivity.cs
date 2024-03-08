using Android.Content;
using Android.Graphics;
using AndroidX.AppCompat.App;
using IO.Scanbot.Sdk.Barcode.Entity;
using IO.Scanbot.Sdk.Ui_v2.Barcode.Configuration;

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

            var barcodeResult = BarcodeResult.CreateFromBundle(Intent.GetBundleExtra(nameof(BarcodeResult)));

            string imagePath = null;

            if (barcodeResult.PreviewPath != null)
            {
                imagePath = barcodeResult.PreviewPath;
            }
            else if (barcodeResult.ImagePath != null)
            {
                imagePath = barcodeResult.ImagePath;
            }

            if (imagePath != null)
            {
                ShowSnapImageFromPath(imagePath);
            }
            else if (barcodeResult.ResultBitmap != null)
            {
                ShowSnapImageFromBitmap(barcodeResult);
            }

            ShowBarcodeResult(barcodeResult.ScanningResult);
            ShowBarcodeResultV2(barcodeResult.ScanningResultV2);
        }
        
        private byte[] ConvertToByteArray(IList<Java.Lang.Byte> rawBytes)
        {
            byte[] byteArray = new byte[rawBytes.Count];
            for (int i = 0; i < rawBytes.Count; i++)
            {
                byteArray[i] = (byte)rawBytes[i].ByteValue();
            }
            return byteArray;
        }
        
        private void ShowBarcodeResultV2(BarcodeScannerResult result)
        {
            var parent = FindViewById<LinearLayout>(Resource.Id.recognisedItems);

            if (result == null)
            {
                return;
            }

            foreach (var item in result.Items)
            {
                var child = LayoutInflater.Inflate(Resource.Layout.barcode_item, parent, false);

                var image = child.FindViewById<ImageView>(Resource.Id.image);
                var barFormat = child.FindViewById<TextView>(Resource.Id.barcodeFormat);
                var docFormat = child.FindViewById<TextView>(Resource.Id.docFormat);
                var docText = child.FindViewById<TextView>(Resource.Id.docText);

                if (item.RawBytes != null)
                {   
                    byte[] byteArray = ConvertToByteArray(item.RawBytes);
                    Bitmap bitmap = BitmapFactory.DecodeByteArray(byteArray, 0, byteArray.Length);

                    image.SetImageBitmap(bitmap);

                }
                
                barFormat.Text = "Format: " + item.FormattedResult?.TypeName;

                if (item.FormattedResult != null)
                {
                    docFormat.Text = item.FormattedResult.ToString();
                }
                else
                {
                    docFormat.Text = "Document: –";
                }
                
                docText.Text = "Content: " + item.TextWithExtension;

                child.Click += delegate
                {
                    var intent = new Intent(this, typeof(DetailedItemDataActivity));
                    intent.PutExtra("SelectedBarcodeItem", item);
                    StartActivity(intent);
                };

                parent.AddView(child);
            }
        }

        void ShowSnapImageFromPath(string path)
        {
            AddImageView().SetImageURI(Android.Net.Uri.Parse(path));
        }

        void ShowSnapImageFromBitmap(BarcodeResult barcodeResult)
        {
            var original = barcodeResult.ResultBitmap;
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
                    var intent = new Intent(this, typeof(DetailedItemDataActivity));
                    intent.PutExtra("SelectedBarcodeItem", item);
                    StartActivity(intent);
                };

                parent.AddView(child);
            }
        }
    }
}
