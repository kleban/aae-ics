using Syncfusion.Licensing;

using AAEICS.Services.AppConfig;

namespace AAEICS.Client.Services
{
    public sealed class SyncfusionLicenseInitializer(IAppConfigService config) : ISyncfusionLicenseInitializer
    {
        public void Register()
        {
            var licenseKey = config.Get("Syncfusion:LicenseKey");
            if (!string.IsNullOrWhiteSpace(licenseKey))
            {
                SyncfusionLicenseProvider.RegisterLicense(licenseKey);
            }
        }
    }
}
