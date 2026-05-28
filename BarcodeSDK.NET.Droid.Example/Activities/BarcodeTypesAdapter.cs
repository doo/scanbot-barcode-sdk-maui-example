using _Microsoft.Android.Resource.Designer;
using Android.Views;
using AndroidX.RecyclerView.Widget;
using BarcodeSDK.NET.Droid;
using IO.Scanbot.Sdk.Barcode;

public class BarcodeTypesAdapter : RecyclerView.Adapter
{
      public override int ItemCount => BarcodeFormats.All.Count;

      public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
      {
            var format = BarcodeFormats.All[position];
            var barcodeHolder = (BarcodeViewHolder)holder;
            barcodeHolder.Name.Text = format.Name();
            barcodeHolder.Checker.Checked = BarcodeTypes.Instance.AcceptedBarcodesDictionary[format];
            barcodeHolder.Index = position;
      }

      public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
      {
            var inflater = LayoutInflater.From(parent.Context);
            var view = inflater?.Inflate(ResourceConstant.Layout.barcode_type, parent, false);
            return new BarcodeViewHolder(view);
      }
}

class BarcodeViewHolder : RecyclerView.ViewHolder
{
      public int Index = 0;
      public TextView Name { get; private set; }

      public CheckBox Checker { get; private set; }

      public BarcodeViewHolder(View item) : base(item)
      {
            Name = item.FindViewById<TextView>(ResourceConstant.Id.barcode_type_name)!;
            Checker = item.FindViewById<CheckBox>(ResourceConstant.Id.barcode_type_checker)!;
            Checker.CheckedChange += CheckChangeHandler;
      }

      private void CheckChangeHandler(object sender, CompoundButton.CheckedChangeEventArgs e)
      {
            var format = BarcodeFormats.All[Index];
            BarcodeTypes.Instance.Update(format, e.IsChecked);
      }
}