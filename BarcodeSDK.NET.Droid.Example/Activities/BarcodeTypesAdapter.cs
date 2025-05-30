﻿using Android.Views;
using AndroidX.RecyclerView.Widget;
using IO.Scanbot.Sdk.Barcode;

namespace BarcodeSDK.NET.Droid.Activities
{
    public class BarcodeTypesAdapter : RecyclerView.Adapter
    {
        public override int ItemCount => BarcodeFormats.All.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var format = BarcodeFormats.All[position];
            var barcodeHolder = (BarcodeViewHolder)holder;
            barcodeHolder.Name.Text = format.Name();
            barcodeHolder.Checker.Checked = BarcodeTypes.Instance.AcceptedBarcodesDictionary[format];

            barcodeHolder.Checker.CheckedChange += (sender, e) =>
            {
                BarcodeTypes.Instance.Update(format, e.IsChecked);
            };
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var inflater = LayoutInflater.From(parent.Context);
            var view = inflater.Inflate(Resource.Layout.barcode_type, parent, false);
            return new BarcodeViewHolder(view);
        }
    }

    class BarcodeViewHolder : RecyclerView.ViewHolder
    {
        public TextView Name { get; private set; }

        public CheckBox Checker { get; private set; }

        public BarcodeViewHolder(View item) : base(item)
        {
            Name = item.FindViewById<TextView>(Resource.Id.barcode_type_name);
            Checker = item.FindViewById<CheckBox>(Resource.Id.barcode_type_checker);
        }
    }
}
