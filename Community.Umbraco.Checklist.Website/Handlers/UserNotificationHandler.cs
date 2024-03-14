using Community.Umbraco.Checklist.Models.Notifications;
using Umbraco.Cms.Core.Events;
using Notifications = Umbraco.Cms.Core.Notifications;

namespace Community.Umbraco.Checklist.Website.Handlers
{
    public class UserNotificationHandler : INotificationAsyncHandler<Notifications.UserSavedNotification>
    {
        private readonly IEventAggregator _eventAggregator;

        public UserNotificationHandler(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }


        public async Task HandleAsync(Notifications.UserSavedNotification notification, CancellationToken cancellationToken)
        {
            if (notification.SavedEntities.Any(e => !e.Email.EndsWith(Examples.Constants.UserNameAllowedAsAdmin)))
            {
                await _eventAggregator.PublishAsync(new RunChecklistNotification(), cancellationToken);
            }
        }
    }

    
}
