using Umbraco.Cms.Core.HealthChecks;
using Umbraco.Cms.Core.Models.Membership;
using Umbraco.Cms.Core.Services;

namespace Examples.HealthChecks.Security;

[HealthCheck("a4968a31-0b47-4641-beaa-dc486a86767a", "Admin Check",
    Description = "Check if unwanted users are admin",
    Group = "Site Configuration")]
public class AdminHealthCheck : HealthCheck
{
    private readonly IUserService _userService;

    public AdminHealthCheck(IUserService userService)
    {
        _userService = userService;
    }

    public override Task<IEnumerable<HealthCheckStatus>> GetStatus()
    {
        return Task.FromResult((IEnumerable<HealthCheckStatus>)new[] { CheckForAdminUsers() });
    }

    private HealthCheckStatus CheckForAdminUsers()
    {
        var userGroups = _userService.GetAllUserGroups();
        var adminGroup = userGroups.FirstOrDefault(f => f.Alias == "admin");
        if (adminGroup != null)
        {
            var users = _userService.GetAllInGroup(adminGroup.Id);
            var unwantedAdminUsers = users.Where(user => !user.Email.ToLowerInvariant().EndsWith(Constants.UserNameAllowedAsAdmin));
            var adminUsers = unwantedAdminUsers as IUser[] ?? unwantedAdminUsers.ToArray();
            if (adminUsers.Any())
            {
                return
                    new HealthCheckStatus(
                        $"The following users are found in the admin group that needs to be removed: {string.Join(", ", adminUsers.Select(user => $"User {user.Name} with email {user.Email}"))}")
                    {
                        ResultType = StatusResultType.Error
                    };
            }

            return new HealthCheckStatus("No unwanted admins found")
            {
                ResultType = StatusResultType.Success
            };
        }

        return new HealthCheckStatus("No admin group found")
        {
            ResultType = StatusResultType.Warning
        };
    }

    public override HealthCheckStatus ExecuteAction(HealthCheckAction action)
    {
        throw new NotImplementedException();
    }


}