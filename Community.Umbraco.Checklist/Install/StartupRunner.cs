using Community.Umbraco.Checklist.Services;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;

namespace Community.Umbraco.Checklist.Install
{
    public class StartupRunner : INotificationAsyncHandler<UmbracoApplicationStartedNotification>
    {
        private readonly ICheckListStatusService _healthCheckService;
        private readonly IChecklistBootService _checklistBootService;

        public StartupRunner(ICheckListStatusService healthCheckService, IChecklistBootService checklistBootService)
        {
            _healthCheckService = healthCheckService;
            _checklistBootService = checklistBootService;
        }


        public async Task HandleAsync(UmbracoApplicationStartedNotification notification,
            CancellationToken cancellationToken)
        {
	        //first run the manual checklist items 
            await _checklistBootService.InstallOrUpdateAsync(); //install or update records from config file
            await _checklistBootService.CleanupNotFoundAsync(); // clean up items removed from config file

            await _healthCheckService.RunAsync(); // run umbraco health check on startup so we can get the state in our table
        }

        


       
    }
}
