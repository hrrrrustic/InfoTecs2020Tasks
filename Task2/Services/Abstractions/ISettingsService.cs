using Task2.Models;

namespace Task2.Services.Abstractions
{
    public interface ISettingsService
    {
        void RemoveSource(string source);
        void ChangeSourceActivity(string source);
        RssSource AddNewLink(string source);
    }
}