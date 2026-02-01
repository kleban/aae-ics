using Syncfusion.Licensing;
using System;
using System.Collections.Generic;
using System.Text;

namespace AaeIcs.Client.Services
{
    public sealed class SyncfusionLicenseInitializer : ISyncfusionLicenseInitializer
    {
        private readonly IAppConfigService _config;

        public SyncfusionLicenseInitializer(IAppConfigService config)
        {
            _config = config;
        }

        public void Register()
        {
            var licenseKey = _config.Get("Syncfusion:LicenseKey");
            if (!string.IsNullOrWhiteSpace(licenseKey))
            {
                SyncfusionLicenseProvider.RegisterLicense(licenseKey);
            }
        }
    }
}
