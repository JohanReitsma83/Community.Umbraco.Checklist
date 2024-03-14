using Community.Umbraco.Checklist.Models.Notifications;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;

namespace Community.Umbraco.Checklist.Website.Handlers;

public class PublishContentHandler : INotificationAsyncHandler<ContentPublishedNotification>
{
    private readonly IEventAggregator _eventAggregator;

    public PublishContentHandler(IEventAggregator eventAggregator)
    {
        _eventAggregator = eventAggregator;
    }

    public async Task HandleAsync(ContentPublishedNotification notification, CancellationToken cancellationToken)
    {
        await _eventAggregator.PublishAsync(new RunChecklistNotification(), cancellationToken);
    }
}