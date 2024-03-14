using Community.Umbraco.Checklist.Website.Handlers;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Notifications;

namespace Community.Umbraco.Checklist.Website.Composers
{
    public class StartupComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {

            builder.AddNotificationAsyncHandler<UserSavedNotification, UserNotificationHandler>();
            builder.AddNotificationAsyncHandler<ContentPublishedNotification, PublishContentHandler>();

        }
    }
}
