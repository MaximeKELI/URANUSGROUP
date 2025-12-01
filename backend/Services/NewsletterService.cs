using Microsoft.EntityFrameworkCore;
using UranusGroup.Data;
using UranusGroup.DTOs;
using AutoMapper;

namespace UranusGroup.Services
{
    public class NewsletterService : INewsletterService
    {
        private readonly UranusGroupContext _context;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public NewsletterService(UranusGroupContext context, IMapper mapper, IEmailService emailService)
        {
            _context = context;
            _mapper = mapper;
            _emailService = emailService;
        }

        public async Task<NewsletterDto> SubscribeAsync(SubscribeNewsletterDto subscribeDto)
        {
            // Check if already subscribed
            var existingSubscription = await _context.Newsletters
                .FirstOrDefaultAsync(n => n.Email == subscribeDto.Email);

            if (existingSubscription != null)
            {
                if (existingSubscription.IsActive)
                {
                    return _mapper.Map<NewsletterDto>(existingSubscription);
                }
                else
                {
                    // Reactivate subscription
                    existingSubscription.IsActive = true;
                    existingSubscription.UnsubscribedAt = null;
                    await _context.SaveChangesAsync();
                    return _mapper.Map<NewsletterDto>(existingSubscription);
                }
            }

            // Create new subscription
            var newsletter = _mapper.Map<Models.Newsletter>(subscribeDto);
            _context.Newsletters.Add(newsletter);
            await _context.SaveChangesAsync();

            // Send welcome email
            await _emailService.SendNewsletterWelcomeAsync(newsletter);

            return _mapper.Map<NewsletterDto>(newsletter);
        }

        public async Task<bool> UnsubscribeAsync(string email)
        {
            var subscription = await _context.Newsletters
                .FirstOrDefaultAsync(n => n.Email == email && n.IsActive);

            if (subscription == null)
                return false;

            subscription.IsActive = false;
            subscription.UnsubscribedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<NewsletterDto>> GetAllSubscribersAsync()
        {
            var subscribers = await _context.Newsletters
                .Where(n => n.IsActive)
                .OrderByDescending(n => n.SubscribedAt)
                .ToListAsync();

            return _mapper.Map<IEnumerable<NewsletterDto>>(subscribers);
        }

        public async Task<bool> IsSubscribedAsync(string email)
        {
            return await _context.Newsletters
                .AnyAsync(n => n.Email == email && n.IsActive);
        }
    }
}
