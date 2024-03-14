using System.Reflection;
using System.Threading;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Community.Umbraco.Checklist.Core.Attribute;
using Community.Umbraco.Checklist.Core.Enums;
using Community.Umbraco.Checklist.Core.Models;
using Community.Umbraco.Checklist.Helpers;
using Community.Umbraco.Checklist.Models;
using Community.Umbraco.Checklist.Models.Notifications;
using Community.Umbraco.Checklist.Repository;
using Umbraco.Cms.Core.Events;
using Umbraco.Extensions;

namespace Community.Umbraco.Checklist.Services
{
    public interface IChecklistBootService
    {
        Task CleanupNotFoundAsync();

        Task InstallOrUpdateAsync();

    }

    internal class ChecklistBootService : IChecklistBootService
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ICheckListItemRepository _repository;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ChecklistBootService(IEventAggregator eventAggregator,
            ICheckListItemRepository checkListItemRepository, IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment)
        {
            _eventAggregator = eventAggregator;
            _repository = checkListItemRepository;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        public Task CleanupNotFoundAsync()
        {
            var allCheckListsItems = _repository.GetAll().Where(c => c.RunType == CheckListType.Manual);
            var configEntries = ChecklistItemHelper.ReadFromFile(_configuration, _webHostEnvironment);

            if (configEntries != null)
            {
                foreach (var item in allCheckListsItems)
                {
                    if (!configEntries.Select(i => i.Name).Contains(item.Name))
                    {
                        _repository.Delete(item.Id);
                    }
                }
            }

            return Task.CompletedTask;
        }

        public async Task InstallOrUpdateAsync()
        {
            var configEntries = ChecklistItemHelper.ReadFromFile(_configuration, _webHostEnvironment);
            if (configEntries != null)
            {
                foreach (var entry in configEntries)
                {
                    await _eventAggregator.PublishAsync(new StatusChangeForChecklistItemNotifition(null,
                        new CheckListItem(entry.Name, entry.UniqueAlias ?? entry.Name, entry.Category,
                            entry.Description, entry.Trigger),
                        new CheckListItemStatus()
                        {
                            Messages = new List<string>(),
                            StatusType = CheckListItemStatusType.Warning
                        }));
                }
            }

        }

    }
}
