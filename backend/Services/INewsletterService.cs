using UranusGroup.DTOs;

namespace UranusGroup.Services
{
    public interface INewsletterService
    {
        Task<NewsletterDto> SubscribeAsync(SubscribeNewsletterDto subscribeDto);
        Task<bool> UnsubscribeAsync(string email);
        Task<IEnumerable<NewsletterDto>> GetAllSubscribersAsync();
        Task<bool> IsSubscribedAsync(string email);
    }
}
