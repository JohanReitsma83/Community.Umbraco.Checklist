using System.Text.Json;
using Community.Umbraco.Checklist.Core.Attribute;
using Community.Umbraco.Checklist.Core.Enums;
using Community.Umbraco.Checklist.Core.Models;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;

namespace Examples.TriggerOn
{
    [ChecklistTrigger("HomepagePublished")]
    public class HomepagePublishedTrigger : ChecklistItemTrigger
    {
        private readonly IUmbracoContextFactory _contextFactory;

        public HomepagePublishedTrigger(IUmbracoContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        private int[] GetStateObject()
        {
            var ctx = _contextFactory.EnsureUmbracoContext();
            var roots = ctx.UmbracoContext.Content?.GetAtRoot().Where(r => r.ContentType.Alias == "homepage").Select(root => root.Id).ToArray();


            return roots ?? Array.Empty<int>();
        }

        public override StateInformation GetStatus(StateInformation previousState)
        {

            var previousPages = !previousState.State.IsNullOrWhiteSpace() ? JsonSerializer.Deserialize<int[]>(previousState.State) : Array.Empty<int>();
	        var newState = GetStateObject();
            var changed = false;
	        var messages = new List<string>();
	        foreach (var id in newState)
	        {
		        if (previousPages != null && !previousPages.Contains(id))
		        {
			        changed = true;
			        var ctx = _contextFactory.EnsureUmbracoContext();
			        var root = ctx.UmbracoContext.Content?.GetById(id);
			        if (root == null)
				        continue;
			        messages.Add($"new Homepage is added, please check {root.Name} with Url {root.Url()}");
		        }
	        }

	        return new StateInformation()
	        {
                State = JsonSerializer.Serialize(newState),
                Status = new CheckListItemStatus()
                {
                    StatusType =  changed ? CheckListItemStatusType.Warning : previousState.Status.StatusType,
                    Messages = messages
                }
	        };
        }

    }


}