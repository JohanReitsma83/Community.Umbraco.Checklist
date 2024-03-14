using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using StackExchange.Profiling.Internal;
using Community.Umbraco.Checklist.Models.Dto;
using Umbraco.Cms.Core.Models.Membership;
using Umbraco.Cms.Core.Services;
using Umbraco.Extensions;

namespace Community.Umbraco.Checklist.Services
{
    public interface ICurrentUserService
    {
        UserInformation GetCurrentUserInformation(string email);

        string CurrentUserName { get; }
    }

    public class CurrentUserService : ICurrentUserService
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOptionsSnapshot<CookieAuthenticationOptions> _cookieOptionsSnapshot;

        public CurrentUserService(IUserService userService, IHttpContextAccessor httpContextAccessor, IOptionsSnapshot<CookieAuthenticationOptions> cookieOptionsSnapshot)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _cookieOptionsSnapshot = cookieOptionsSnapshot;
        }

        public UserInformation GetCurrentUserInformation(string email)
        {
            var user = _userService.GetByEmail(email);
            if (user != null)
            {
                return new UserInformation()
                {
                    Active = user.UserState is UserState.Disabled or UserState.Inactive,
                    Email = user.Email,
                    Name = user.Name ?? user.Email
                };

            }

            return new UserInformation()
            {
                Email = email,
                Name = email
            };
        }

        public string CurrentUserName
        {
            get
            {
                var httpContext = _httpContextAccessor.HttpContext;

                if (httpContext == null) throw new InvalidOperationException($"No httpContext available");

                var cookieOptions =
                    _cookieOptionsSnapshot.Get(global::Umbraco.Cms.Core.Constants.Security
                        .BackOfficeAuthenticationType);

                if (cookieOptions == null) throw new InvalidOperationException($"No cookie options found with name {(global::Umbraco.Cms.Core.Constants.Security.BackOfficeAuthenticationType)}");


                if (cookieOptions.Cookie.Name is not null && httpContext.Request.Cookies.TryGetValue(cookieOptions.Cookie.Name, out var cookie))
                {
                    var unprotected = cookieOptions.TicketDataFormat.Unprotect(cookie);
                    var backOfficeIdentity = unprotected?.Principal.GetUmbracoIdentity();
                    if (backOfficeIdentity != null && backOfficeIdentity.Name.HasValue())
                    {
                        return backOfficeIdentity.Name ?? throw new InvalidOperationException($"No backoffice user available");
                    }
                }

                throw new InvalidOperationException($"No backoffice user available");


            }
        }
    }
}
