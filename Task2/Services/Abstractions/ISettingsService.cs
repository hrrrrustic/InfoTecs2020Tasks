using Task2.Models;

namespace Task2.Services.Abstractions
{
    public interface ISettingsService
    {
        RssSource RemoveSource(string source);
        RssSource ChangeSourceActivity(string source);
        RssSource AddNewLink(string source);
    }
}