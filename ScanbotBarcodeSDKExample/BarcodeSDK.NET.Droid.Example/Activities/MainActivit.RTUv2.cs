using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Views;
using IO.Scanbot.Sdk.Barcode.Entity;
using IO.Scanbot.Sdk.Barcode_scanner;
using IO.Scanbot.Sdk.UI.Barcode_scanner.View.Barcode;
using IO.Scanbot.Sdk.UI.Barcode_scanner.View.Barcode.Batch;
using IO.Scanbot.Sdk.UI.View.Barcode.Batch.Configuration;
using IO.Scanbot.Sdk.UI.View.Barcode.Configuration;
using IO.Scanbot.Sdk.UI.View.Base;
using IO.Scanbot.Sdk.Barcode;
using BarcodeSDK.NET.Droid.Activities;
using BarcodeSDK.NET.Droid.Activities.V1;
using BarcodeSDK.NET.Droid.Snippets;
using IO.Scanbot.Sdk.Ui_v2.Barcode.Configuration;
using BarcodeScannerConfiguration = IO.Scanbot.Sdk.UI.View.Barcode.Configuration.BarcodeScannerConfiguration;
using BarcodeScannerActivityV2 = IO.Scanbot.Sdk.Ui_v2.Barcode.BarcodeScannerActivity;

namespace BarcodeSDK.NET.Droid
{
    public partial class MainActivity : Activity
    {
        private void OnRTUUI_V2_ClickArOverlay(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this, SDK))
            {
                return;
            }
            
            var intent = BarcodeScannerActivityV2.NewIntent(this, new ArOverlayUseCaseSnippet().GetArOverlayUseCaseSnippetConfiguration());
            StartActivityForResult(intent, BARCODE_DEFAULT_UI_REQUEST_CODE_V2);
        }
        
        private void OnRTUUI_V2_ClickItemMapping(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this, SDK))
            {
                return;
            }
            
            var intent = BarcodeScannerActivityV2.NewIntent(this, new ItemMappingConfigSnippet().GetItemMappingConfigSnippetConfiguration());
            StartActivityForResult(intent, BARCODE_DEFAULT_UI_REQUEST_CODE_V2);
        }
        
        private void OnRTUUI_V2_ClickMultipleScanningPreview(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this, SDK))
            {
                return;
            }
            
            var intent = BarcodeScannerActivityV2.NewIntent(this, new MultipleScanningPreviewConfigSnippet().GetMultipleScanningPreviewConfigSnippetConfiguration());
            StartActivityForResult(intent, BARCODE_DEFAULT_UI_REQUEST_CODE_V2);
        }
        
        private void OnRTUUI_V2_ClickMultipleScanning(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this, SDK))
            {
                return;
            }
            
            var intent = BarcodeScannerActivityV2.NewIntent(this, new MultipleScanningUseCaseSnippet().GetMultipleScanningPreviewConfigSnippetConfiguration());
            StartActivityForResult(intent, BARCODE_DEFAULT_UI_REQUEST_CODE_V2);
        }
        
        private void OnRTUUI_V2_ClickSingleScanning(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this, SDK))
            {
                return;
            }
            
            var intent = BarcodeScannerActivityV2.NewIntent(this, new SingleScanningUseCaseSnippet().GetSingleScanningUseCaseSnippetConfiguration());
            StartActivityForResult(intent, BARCODE_DEFAULT_UI_REQUEST_CODE_V2);
        }
        
        private void OnRTUUI_V2_ClickTopBar(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this, SDK))
            {
                return;
            }
            
            var intent = BarcodeScannerActivityV2.NewIntent(this, new TopBarConfigSnippet().GetTopBarConfigSnippetConfiguration());
            StartActivityForResult(intent, BARCODE_DEFAULT_UI_REQUEST_CODE_V2);
        }
        
        private void OnRTUUI_V2_ClickUserGuid(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this, SDK))
            {
                return;
            }
            
            var intent = BarcodeScannerActivityV2.NewIntent(this, new UserGuidanceConfigSnippet().GetUserGuidanceConfigSnippetConfiguration());
            StartActivityForResult(intent, BARCODE_DEFAULT_UI_REQUEST_CODE_V2);
        }
    }
}