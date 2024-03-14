using Community.Umbraco.Checklist.Core.Enums;
using Microsoft.Extensions.Configuration;
using Community.Umbraco.Checklist.Models;
using Community.Umbraco.Checklist.Models.Notifications;
using Serilog;
using Umbraco.Cms.Core.Events;
using Community.Umbraco.Checklist.Services;

namespace Community.Umbraco.Checklist.Handlers;



public class StatusChangeForChecklistItemNotifitionHandler : INotificationAsyncHandler<StatusChangeForChecklistItemNotifition>
{
    private readonly ILogger _logger;
    private readonly IChecklistService _checkListService;
    private readonly string _serviceAccount;

    public StatusChangeForChecklistItemNotifitionHandler(ILogger logger, IChecklistService checkListService, IConfiguration configuration)
    {
        _logger = logger;
        _checkListService = checkListService;
        _serviceAccount = configuration.GetValue<string>("Checklist:DefaultServiceName") ?? "Umbraco Service Account";
    }

    
    public Task HandleAsync(StatusChangeForChecklistItemNotifition notification, CancellationToken cancellationToken)
    {
	    var foundEntry = _checkListService.GetbyKey(notification.CheckListItem.UniqueAlias);
	    var runBy = notification.CheckListItem.CheckListType == CheckListType.Automatic ? _serviceAccount : "";
        if (foundEntry == null)
	    {
            foundEntry = CreateNewCheckListEntry(notification, runBy, string.Empty);
	    }
	    else
        {
            UpdateEntry(notification, foundEntry);
        }

        _checkListService.Save(foundEntry);

	    _logger.Information($"Running task {notification.CheckListItem.Name} - Status {notification.Status}");
        return Task.CompletedTask; 
    }

    private static CheckListEntry CreateNewCheckListEntry(StatusChangeForChecklistItemNotifition notification, string runBy, string? state = null)
    {
        return new CheckListEntry
        {
            Name = notification.CheckListItem.Name,
            Description = notification.CheckListItem.Description,
            Category = notification.CheckListItem.Category,
            LastRun = notification.TimeCompleted,
            LastExecutedBy = runBy,
            UniqueAlias = notification.CheckListItem.UniqueAlias,
            LastStatus = notification.Status.StatusType,
            RunType = notification.CheckListItem.CheckListType,
            Messages = string.Join("<hr>", notification.Status.Messages),
            State = state,
            Trigger = notification.CheckListItem.Trigger
        };
    }

    private static void UpdateEntry(StatusChangeForChecklistItemNotifition notification, CheckListEntry foundEntry)
    {
        foundEntry.LastRun = notification.TimeCompleted;
        foundEntry.LastStatus = notification.Status.StatusType;
        foundEntry.State = string.Empty;
        foundEntry.Trigger = notification.CheckListItem.Trigger;
        foundEntry.Messages = string.Join("<hr>", notification.Status.Messages);
        foundEntry.State = notification.State;
    }
}