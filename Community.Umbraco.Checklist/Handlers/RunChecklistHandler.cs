using Community.Umbraco.Checklist.Models.Notifications;
using Community.Umbraco.Checklist.Services;
using Umbraco.Cms.Core.Events;

namespace Community.Umbraco.Checklist.Handlers;

public class RunChecklistHandler : INotificationAsyncHandler<RunChecklistNotification>
{
	private readonly ICheckListStatusService _healthCheckService;

	
	public RunChecklistHandler(ICheckListStatusService healthCheckService)
	{
		_healthCheckService = healthCheckService;
	}

	public async Task HandleAsync(RunChecklistNotification notification, CancellationToken cancellationToken)
	{
		await _healthCheckService.RunAsync();
	}
}