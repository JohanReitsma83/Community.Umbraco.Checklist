using Microsoft.AspNetCore.Mvc;
using Community.Umbraco.Checklist.Core.Enums;
using Community.Umbraco.Checklist.Extensions;
using Community.Umbraco.Checklist.Models.Dto;
using Community.Umbraco.Checklist.Services;
using Umbraco.Cms.Web.BackOffice.Controllers;
using Umbraco.Cms.Web.Common.Attributes;

namespace Community.Umbraco.Checklist.Controllers;

[PluginController(Constants.Prefix)]
public class CheckListController : UmbracoAuthorizedJsonController
{
    private readonly ICheckListStatusService _checkListStatusService;
    private readonly IChecklistService _checklistService;
    private readonly ICurrentUserService _currentUserService;

	public CheckListController(ICheckListStatusService checkListStatusService, IChecklistService checklistService, ICurrentUserService currentUserService)
	{
        _checkListStatusService = checkListStatusService;
        _checklistService = checklistService;
        _currentUserService = currentUserService;
    }

    [HttpGet]
    public Task<ActionResult> GetAll()
    {
        var groupedBy = _checklistService.GetAllGroupedByCategory();
		var result = groupedBy.Select(group =>
			new ChecklistGroup(group.Key, group.Select(entry => entry.EntryToDto(_currentUserService.GetCurrentUserInformation)).ToList()));
		return Task.FromResult<ActionResult>(new JsonResult(result));
	}


    [HttpGet]
	public Task<ActionResult> StatusReport()
	{
		var entries = _checklistService.GetAll();
		var entriesWithError = entries.Count(entry => entry.LastStatus == CheckListItemStatusType.Error);
		var entriesWithWarning = entries.Count(entry => entry.LastStatus == CheckListItemStatusType.Warning);
		var entriesWithSucces = entries.Count(entry => entry.LastStatus == CheckListItemStatusType.Succes);
		
		var progress = new ChecklistStatusReport()
		{
			Errors =  entriesWithError,
			Warnings = entriesWithWarning,
			Success = entriesWithSucces,
			Total = entries.Count(),
		};
		return Task.FromResult<ActionResult>(new JsonResult(progress));
	}


    [HttpPost]
    public async Task<ActionResult> Save(CheckListItem model)
    {
        if (ModelState.IsValid)
        {
            var entry = _checklistService.GetbyId(model.Id);
            if (entry == null)
            {
                throw new KeyNotFoundException($"Checklist item with id {model.Id} not found");
            }
            var itemState = await _checkListStatusService.GetStateAsync(entry);

            entry.LastExecutedBy = _currentUserService.CurrentUserName;
            entry.LastRun = DateTime.Now;
            entry.LastStatus = CheckListItemStatusType.Succes;
            entry.Messages = $"Approved by {_currentUserService.CurrentUserName}";
            entry.State = itemState?.State;

            _checklistService.Save(entry);
            return new JsonResult("OK");
        }
        return new JsonResult(nameof(model));
    }

    [HttpPost]
    public async Task<ActionResult> Refresh()
    {
        await _checkListStatusService.RunAsync();
        return new JsonResult("OK");
    }

}