// using ScanbotSDK.MAUI.Barcode;
// using ScanbotSDK.MAUI.Barcode.Core;
//
// namespace ScanbotSDK.MAUI.Example
// {
//     public partial class Snippets : IStaticBarcodeItemMapper
//     {
//         public static BarcodeScannerScreenConfiguration ItemMapping
//         {
//             get
//             {
//                 // Create the default configuration object.
//                 var config = new BarcodeScannerScreenConfiguration();
//                 
//                 var useCase = new SingleScanningMode();
//                 var productTitle = "Some product title";
//
//                 useCase.ConfirmationSheetEnabled = true;
//                 useCase.BarcodeInfoMapping = new BarcodeInfoMapping()
//                 {
//                     BarcodeItemMapper = new DelegateBarcodeItemMapper((barcodeItem, onResult, onError) => {
//                         var title = $"{productTitle} {barcodeItem.UpcEanExtension}";
//                         var subTitle = "Subtitle";
//                         var image = "https://raw.githubusercontent.com/doo/scanbot-sdk-examples/master/sdk-logo.png";
//
//                         if (barcodeItem.TextWithExtension == "Error occurred!")
//                         {
//                             onError();
//                         }
//                         else
//                         {
//                             onResult(new BarcodeMappedData(title: title, subtitle: subTitle, barcodeImage: image));
//                         }
//                     }),
//
//                     // Comment out the above and uncomment below to use static item mappers.
//                     // The class has to implement IStaticBarcodeItemMapper.
//                     //BarcodeItemMapper = new StaticBarcodeItemMapper<Snippets>()
//                 };
//
//                 // Configure other parameters, pertaining to single-scanning mode as needed.
//                 config.UseCase = useCase;
//
//                 return config;
//             }
//         }
//
//         public static void MapBarcodeItem(BarcodeItem barcodeItem, Action<BarcodeMappedData> onResult, Action onError)
//         {
//             var title = $"Static item mapper product {barcodeItem.UpcEanExtension}";
//             var subTitle = "Subtitle";
//             var image = "https://raw.githubusercontent.com/doo/scanbot-sdk-examples/master/sdk-logo.png";
//
//             if (barcodeItem.UpcEanExtension == "Error occurred!")
//             {
//                 onError();
//             }
//             else
//             {
//                 onResult(new BarcodeMappedData(title: title, subtitle: subTitle, barcodeImage: image));
//             }
//         }
//     }
// }
