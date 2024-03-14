using Microsoft.Extensions.DependencyInjection;
using Community.Umbraco.Checklist.Handlers;
using Community.Umbraco.Checklist.Install;
using Community.Umbraco.Checklist.Models.Notifications;
using Community.Umbraco.Checklist.Repository;
using Community.Umbraco.Checklist.Services;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Notifications;

namespace Community.Umbraco.Checklist.Extensions;

public static class UmbracoBuilderExtensions
{
    public static IUmbracoBuilder RegisterRepositories(this IUmbracoBuilder builder)
    {
        builder.Services.AddScoped<ICheckListItemRepository, CheckListItemRepository>();
        return builder;
    }

    public static IUmbracoBuilder RegisterServices(this IUmbracoBuilder builder)
    {
        builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
        builder.Services.AddScoped<IChecklistBootService, ChecklistBootService>();
        builder.Services.AddScoped<IChecklistService, ChecklistService>();
        builder.Services.AddScoped<ICheckListStatusService, CheckListStatusService>();
        
        return builder;
    }

    public static IUmbracoBuilder RegisterNotificationHandlers(this IUmbracoBuilder builder)
    {
        builder.AddNotificationHandler<UmbracoApplicationStartingNotification, MigrationRunner>();
        builder.AddNotificationAsyncHandler<UmbracoApplicationStartedNotification, StartupRunner>();


        builder.AddNotificationAsyncHandler<StatusChangeForChecklistItemNotifition, StatusChangeForChecklistItemNotifitionHandler>();
        builder.AddNotificationAsyncHandler<RunChecklistNotification, RunChecklistHandler>();
        return builder;
    }



}