using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Community.Umbraco.Checklist.Extensions;
using Community.Umbraco.Checklist.Models.Configuration;

namespace Community.Umbraco.Checklist.Helpers
{
    internal static class ChecklistItemHelper
    {
        public static CheckListConfiguration.ChecklistConfigEntryItem[]? ReadFromFile(IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment)
        {

            var checkListConfiguration = configuration.ConfigurationJson<CheckListConfiguration>("Checklist");
            if (checkListConfiguration?.File != null)
            {
                var filePath = Path.Join(webHostEnvironment.ContentRootPath, checkListConfiguration.File);
                if (File.Exists(filePath))
                {
                    var fileContent = File.ReadAllText(filePath);
                    return JsonSerializer.Deserialize<CheckListConfiguration.ChecklistConfigEntryItem[]>(fileContent);
                }
            }

            return Array.Empty<CheckListConfiguration.ChecklistConfigEntryItem>();
        }
    }
}
