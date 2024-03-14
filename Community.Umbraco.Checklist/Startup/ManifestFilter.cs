using Community.Umbraco.Checklist.Core.Extensions;
using Umbraco.Cms.Core.Manifest;

namespace Community.Umbraco.Checklist.Startup;
public class ManifestFilter : IManifestFilter
{
    void IManifestFilter.Filter(List<PackageManifest> manifests)
    {        
        var manifest = new PackageManifest()
        {
            PackageName = Constants.PackageName.ToKebabCase(),
            BundleOptions = BundleOptions.Independent,
            
            Scripts = new[]
            {
               $"/App_Plugins/{Constants.PackageName}/Scripts/Controllers/Dashboards/default.js",

            }
        };

        manifests.Add(manifest);
    }
}