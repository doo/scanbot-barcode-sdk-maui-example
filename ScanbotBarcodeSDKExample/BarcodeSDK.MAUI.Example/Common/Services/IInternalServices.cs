using System;
using BarcodeSDK.MAUI.Example.Common.Models;
using BarcodeSDK.MAUI.Example.Common.Constants;

namespace BarcodeSDK.MAUI.Example.Common.Services
{
    public interface IInternalServices
    {
        public LicenseInfo Initialize(InitializationOptions options);

        public LicenseInfo GetLicenseInfo();
    }
}

