using UranusGroup.Models;

namespace UranusGroup.Services
{
    public interface IEmailService
    {
        Task SendContactNotificationAsync(Contact contact);
        Task SendContactResponseAsync(Contact contact);
        Task SendNewsletterWelcomeAsync(Newsletter newsletter);
        Task SendNewsletterAsync(string subject, string content, List<string> recipients);
    }
}
