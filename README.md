# Scanbot Barcode Example App for MAUI

This example app shows how to integrate the [Scanbot Barcode Scanner SDK](https://scanbot.io/products/barcode-software/barcode-sdk/) for MAUI and .NET (Native iOS and Android platforms).

The Scanbot SDK are available as a NuGet package for the MAUI and .NET platforms:
[ScanbotBarcodeSDK.MAUI](https://www.nuget.org/packages/ScanbotBarcodeSDK.MAUI)
[ScanbotBarcodeSDK.NET](https://www.nuget.org/packages/ScanbotBarcodeSDK.NET)

## What is the Scanbot SDK?

The Scanbot SDK lets you integrate barcode & document scanning, as well as data extraction functionalities, into your mobile apps and website. It contains different modules that are licensable for an annual fixed price. For more details, visit our website https://scanbot.io.


## Trial License

The Scanbot SDK will run without a license for one minute per session!

After the trial period has expired, all SDK functions and UI components will stop working. You have to restart the app to get another one-minute trial period.

To test the Scanbot SDK without crashing, you can get a free “no-strings-attached” trial license. Please submit the [Trial License Form](https://scanbot.io/trial/) on our website.

## Free Developer Support

We provide free "no-strings-attached" developer support for the implementation & testing of the Scanbot SDK.
If you encounter technical issues with integrating the Scanbot SDK or need advice on choosing the appropriate
framework or features, please visit our [Support Page](https://docs.scanbot.io/support/).

## Supported Barcode Types

- [1D Barcodes](https://scanbot.io/products/barcode-software/1d-barcode-scanner/): [Codabar](https://scanbot.io/en/sdk/scanner-sdk/barcode-scanner-sdk/codabar), [Code 39](https://scanbot.io/en/sdk/scanner-sdk/barcode-scanner-sdk/code-39), [Code 93](https://scanbot.io/products/barcode-software/1d-barcode-scanner/code-93/), [Code 128](https://scanbot.io/products/barcode-software/1d-barcode-scanner/code-128/), [IATA 2 of 5](https://scanbot.io/products/barcode-software/1d-barcode-scanner/standard-2-of-5/), [Industrial 2 of 5](https://scanbot.io/products/barcode-software/1d-barcode-scanner/industrial-2-of-5/), [ITF](https://scanbot.io/en/sdk/scanner-sdk/barcode-scanner-sdk/itf), [EAN-8](https://scanbot.io/en/sdk/scanner-sdk/barcode-scanner-sdk/ean-code), [EAN-13](https://scanbot.io/en/sdk/scanner-sdk/barcode-scanner-sdk/ean-code), [MSI Plessey](https://scanbot.io/en/sdk/scanner-sdk/barcode-scanner-sdk/msi-plessey), [RSS 14](https://scanbot.io/products/barcode-software/1d-barcode-scanner/gs1-databar/), [RSS Expanded (Databar)](https://scanbot.io/products/barcode-software/1d-barcode-scanner/gs1-databar/), [UPC-A](https://scanbot.io/products/barcode-software/1d-barcode-scanner/upc/), [UPC-E](https://scanbot.io/en/sdk/scanner-sdk/barcode-scanner-sdk/upc-code).
- [2D Barcodes](https://scanbot.io/products/barcode-software/2d-barcode-scanner/): [Aztec](https://scanbot.io/en/sdk/scanner-sdk/barcode-scanner-sdk/aztec), [Data Matrix](https://scanbot.io/en/sdk/scanner-sdk/barcode-scanner-sdk/datamatrix), [PDF417](https://scanbot.io/products/barcode-software/2d-barcode-scanner/pdf417/), [QR Code](https://scanbot.io/products/barcode-software/2d-barcode-scanner/qr-code/).

💡 Also check out our blog post [Types of barcodes](https://scanbot.io/blog/types-of-barcodes/).


## Supported Data Parsers:

- [AAMVA](https://scanbot.io/blog/drivers-license-barcode-parser/): Parse the AAMVA data format from PDF-417 barcodes on US driver's licenses.
- Boarding pass data from PDF417 barcodes.
- Parser for German Medical Certificates (aka. Disability Certificate or AU-Bescheinigung) coded in a PDF-417 barcode.
- [GS1](https://scanbot.io/products/barcode-software/1d-barcode-scanner/gs1-databar/) encoded data from barcodes.
- Data from PDF-417 barcodes on ID Cards.
- Parse and extract data from XML of Data Matrix barcodes on Medical Plans (German Medikationsplan).
- Data parser of QR-Code values printed on SEPA pay forms.
- vCard data from a QR-Code (e.g. on business cards).
- [Swiss QR](https://scanbot.io/products/barcode-software/2d-barcode-scanner/swiss-qr/) data from a QR-Code for easy, automatic and efficient payments.

For more details please refer to the SDK documentation.


## Requirements
Developing native, cross-platform .NET Multi-platform App UI apps requires the items mentioned below, according to the system you are building on.

**MacOS** 

  - Download and install the [.NET SDK](https://dotnet.microsoft.com/en-us/download).
  - Install the latest **.NET MAUI workloads** via terminal using this command `sudo dotnet workload install maui`.
  - Install the Android and iOS SDKs.
  - **Note:** As the IDE **Visual Studio for Mac** is no longer supported, you may want to check this article on [Visual Studio alternatives for Mac](https://scanbot.io/techblog/visual-studio-alternatives-for-mac/).

**Windows**

  - Install [Visual Studio](https://dotnet.microsoft.com/en-us/download).
  - For running and debugging iOS applications, Check [Pair to Mac](https://learn.microsoft.com/en-us/dotnet/maui/ios/pair-to-mac?view=net-maui-8.0) for iOS development.

## Documentation
The documentation of the current ScanbotBarcodeSDK.NET release can be found [here](https://docs.scanbot.io/barcode-scanner-sdk/maui/introduction/)

### Build Instructions

Assuming you already have your development machine setup, the following commands will help you build and debug our projects:

#### .NET
##### iOS

To build the iOS example project for both net7.0-ios and net8.0-ios, forcing packages to be restored and everything to be compiled from scratch:

```dotnet build ScanbotBarcodeSDKExample/BarcodeSDK.NET.iOS.Example  -r ios-arm64 --force --no-incremental```

To run the project on a real device, specify a target framework with `-f net8.0-ios` and the Run target via `-t:Run`, yielding the following:

```dotnet build ScanbotBarcodeSDKExample/BarcodeSDK.NET.iOS.Example -r ios-arm64 -f net8.0-ios -t:Run --force --no-incremental```

##### Android
To build the Android example project for both net7.0-android and net8.0-android, forcing packages to be restored and everything to be compiled from scratch:

```dotnet build ScanbotBarcodeSDKExample/BarcodeSDK.NET.Droid.Example --force --no-incremental```

To run the project on a real device, specify a target framework with `-f net8.0-android` and the Run target via `-t:Run`, yielding the following:

```dotnet build ScanbotBarcodeSDKExample/BarcodeSDK.NET.Droid.Example -f net8.0-android -t:Run --force --no-incremental```

#### MAUI

To build the MAUI example project for all supported target frameworks (net7.0-android, net7.0-ios, net8.0-android and net8.0-ios) and forcing packages to be restored and everything to be compiled from scratch, execute:

```dotnet build ScanbotBarcodeSDKExample/BarcodeSDK.MAUI.Example --force --no-incremental```

To run the project on a real iOS device, specify a target framework with `-f net8.0-ios` and the Run target via `-t:Run`, yielding the following:

```dotnet build ScanbotBarcodeSDKExample/BarcodeSDK.MAUI.Example -f net8.0-ios -t:Run --force --no-incremental```

To run the project on a real Android device, specify a target framework with `-f net8.0-android` and the Run target via `-t:Run`, yielding the following:

```dotnet build ScanbotBarcodeSDKExample/BarcodeSDK.MAUI.Example -f net8.0-android -t:Run --force --no-incremental```

## Please note

The Scanbot SDK will run without a license for one minute per session!

After the trial period has expired all Scanbot SDK functions as well as the UI components (like the Document Scanner UI) will stop working or may be terminated.
You have to restart the app to get another trial period.

To get a free "no-strings-attached" trial license, please submit the [Trial License Form](https://scanbot.io/trial/) on our website.
