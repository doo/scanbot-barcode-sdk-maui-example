﻿using BarcodeSDK.MAUI.Constants;

namespace BarcodeSDK.MAUI.Example.Common.Models
{
    public class LicenseInfo
    {
        public bool IsValid { get; set; }

        public LicenseStatus Status { get; set; }

        public DateTime? ExpirationDate { get; set; }
    }
}
