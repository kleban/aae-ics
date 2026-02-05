using Syncfusion.Licensing;
using AAEICS.Services.AppConfiguration;

namespace AAEICS.Services.SyncfusionLicenseInitializerService;

public sealed class SyncfusionLicenseInitializer(IAppConfigService config) : ISyncfusionLicenseInitializer
{
    public void Register()
    {
        var licenseKey = config.Get("Keys:Syncfusion");
        if (!string.IsNullOrWhiteSpace(licenseKey))            
            SyncfusionLicenseProvider.RegisterLicense(licenseKey);            
    }
}
