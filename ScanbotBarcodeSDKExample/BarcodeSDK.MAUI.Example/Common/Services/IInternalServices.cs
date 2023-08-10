using BarcodeSDK.MAUI.Example.Common.Models;

namespace BarcodeSDK.MAUI.Example.Common.Services
{
    public interface IInternalServices
    {
        public LicenseInfo Initialize(InitializationOptions options);

        public LicenseInfo GetLicenseInfo();
    }
}