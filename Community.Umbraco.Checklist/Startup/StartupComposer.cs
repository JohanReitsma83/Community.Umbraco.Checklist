using Community.Umbraco.Checklist.Extensions;
using Community.Umbraco.Checklist.Models;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;

namespace Community.Umbraco.Checklist.Startup
{
    public class StartupComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.RegisterRepositories()
                .RegisterServices()
                .RegisterNotificationHandlers()
                .ManifestFilters()
                .Append<ManifestFilter>();
        }

        
    }
}
