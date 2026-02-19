using Syncfusion.Licensing;
using AAEICS.Services.AppConfiguration;

namespace AAEICS.Client.Services.SyncfusionLicenseInitializer;

public sealed class SyncfusionLicenseInitializerService(IAppConfigService config) : ISyncfusionLicenseInitializerService
{
    public void Register()
    {
        var licenseKey = config.Get("Keys:Syncfusion");
        if (!string.IsNullOrWhiteSpace(licenseKey))            
            SyncfusionLicenseProvider.RegisterLicense(licenseKey);            
    }
}
