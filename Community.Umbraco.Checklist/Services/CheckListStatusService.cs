using Microsoft.Extensions.DependencyInjection;
using Community.Umbraco.Checklist.Core.Attribute;
using Community.Umbraco.Checklist.Core.Enums;
using Community.Umbraco.Checklist.Core.Models;
using Community.Umbraco.Checklist.Helpers;
using Community.Umbraco.Checklist.Models;
using Community.Umbraco.Checklist.Models.Notifications;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.HealthChecks;
using Umbraco.Extensions;

namespace Community.Umbraco.Checklist.Services;

public interface ICheckListStatusService
{
    Task RunAsync();

    Task<StateInformation> GetStateAsync(CheckListEntry entry);

}

public class CheckListStatusService : ICheckListStatusService
{
        private readonly IServiceProvider _serviceProvider;
    private readonly IEventAggregator _eventAggregator;
    private readonly IChecklistService _checklistService;

    public CheckListStatusService(IServiceProvider serviceProvider, IEventAggregator eventAggregator, IChecklistService checklistService)
    {
        _serviceProvider = serviceProvider;
        _eventAggregator = eventAggregator;
        _checklistService = checklistService;
    }

    public async Task RunAsync()
    {
        await RunUmbracoHealthChecksAsync();
        await RunChecklistAsync();

    }

    public async Task<StateInformation> GetStateAsync(CheckListEntry entry)
    {
        var result = new StateInformation()
        {
            State = entry.State,
            Status = new CheckListItemStatus()
            {
                StatusType = entry.LastStatus
            }
        };

        var foundTypes = await TypeHelpers.GetTypesWithAttribute<ChecklistItemTrigger, ChecklistTriggerAttribute>();

        var checklistItemType = foundTypes
            .Find(f => f.GetCustomAttribute<ChecklistTriggerAttribute>(false)?.TriggerName == entry.Trigger);

        if (checklistItemType == null) return result;
        var instance = _serviceProvider.CreateObjectOfType<ChecklistItemTrigger>(checklistItemType);

        return instance.GetStatus(new StateInformation()
        {
            Status = new CheckListItemStatus()
            {
                StatusType = entry.LastStatus
            },
            State = entry.State
        });

    }

    private async Task RunChecklistAsync()
    {
        var allCheckListsItems = _checklistService.GetAll().Where(c => c.RunType == CheckListType.Manual);
        foreach (var entry in allCheckListsItems)
        {
            var state = await GetStateAsync(entry);
            if (state.Status.StatusType != entry.LastStatus)
            {

                var checkListItem = new CheckListItem(entry.Name, entry.UniqueAlias, entry.Category, entry.Description,
                    entry.Trigger);
                var status = new CheckListItemStatus()
                {
                    Messages = state.Status.Messages,
                    StatusType = CheckListItemStatusType.Warning
                };
                var notification = new StatusChangeForChecklistItemNotifition(DateTime.Now,
                    checkListItem, status);

                await PublishNotificationAsync(notification);
            }
        }
    }

    private async Task RunUmbracoHealthChecksAsync()
    {
        var foundTypes = await TypeHelpers.GetTypesWithAttribute<HealthCheck, HealthCheckAttribute>();

        foreach (var instance in foundTypes.Select(type => (HealthCheck)ActivatorUtilities.CreateInstance(_serviceProvider, type)))
        {
            var status = await GetStatusOfUmbracoHealthCheckAsync(instance);

            var entry = new AutomaticChecklistItem(instance.Name, instance.Id.ToString(), $"Healthcheck {instance.Group}", instance.Description ?? string.Empty);

            var notification = new StatusChangeForChecklistItemNotifition(DateTime.Now, entry, status);

            await PublishNotificationAsync(notification);
        }
    }

    private async Task PublishNotificationAsync(StatusChangeForChecklistItemNotifition notification)
    {
        await _eventAggregator.PublishAsync(notification);
    }

    
    private static async Task<CheckListItemStatus> GetStatusOfUmbracoHealthCheckAsync(HealthCheck healthCheck)
    {
        var result = new CheckListItemStatus()
        {
            StatusType = CheckListItemStatusType.Succes
        };

        var statusOfHealthcare = await healthCheck.GetStatus();
        foreach (var state in statusOfHealthcare)
        {
            if (state.ResultType == StatusResultType.Success) continue;

            result.StatusType = CheckListItemStatusType.Error;
            result.Messages.Add($"{healthCheck.Name} - {state.Message}");

        }
        return result;
    }
}