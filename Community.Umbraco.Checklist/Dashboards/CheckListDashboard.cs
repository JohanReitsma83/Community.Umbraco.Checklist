using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Dashboards;

namespace Community.Umbraco.Checklist.Dashboards;

[Weight(-10)]
public class CheckListDashboard : IDashboard
{
    
    public string Alias => Constants.PackageName;

    public string[] Sections => new[]
    {
       global::Umbraco.Cms.Core.Constants.Applications.Content,
    };

    public string View => $"/App_Plugins/{Constants.PackageName}/Views/dashboard.html?{Constants.Version}";

    public IAccessRule[] AccessRules
    {
        get
        {
            var rules = new IAccessRule[]
            {
                new AccessRule {Type = AccessRuleType.Grant, Value = global::Umbraco.Cms.Core.Constants.Security.AdminGroupAlias},
                new AccessRule{Type = AccessRuleType.Grant, Value = Constants.Security.GroupAlias}
			};
            return rules;
        }
    }
    
}
