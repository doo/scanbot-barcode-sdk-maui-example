using Android.Util;
using Android.Views;
using AndroidX.Core.View;

namespace BarcodeSDK.NET.Droid;

public static class AndroidUtils
{
    public static void ApplyEdgeToEdge(View rootView, IOnApplyWindowInsetsListener listener)
    {
        ViewCompat.SetOnApplyWindowInsetsListener(rootView, listener);
    }
    
    public static WindowInsetsCompat ApplyWindowInsets(View view, WindowInsetsCompat windowInsets)
    {
        var insets = windowInsets.GetInsetsIgnoringVisibility(WindowInsetsCompat.Type.SystemBars());

        if (view.LayoutParameters != null)
        {
            var layoutParams = (ViewGroup.MarginLayoutParams) view.LayoutParameters;
            layoutParams.TopMargin = insets.Top;
            layoutParams.BottomMargin = insets.Bottom;
            layoutParams.LeftMargin = insets.Left;
            layoutParams.RightMargin = insets.Right;

            view.LayoutParameters = layoutParams;
        }
            
        return WindowInsetsCompat.Consumed;
    }
    
    internal static int DpToPx(Android.Content.Context context, int dp)
    {
        return (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, dp, context.Resources.DisplayMetrics);
    }
}
