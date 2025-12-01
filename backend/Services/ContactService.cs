using Microsoft.EntityFrameworkCore;
using UranusGroup.Data;
using UranusGroup.DTOs;
using AutoMapper;

namespace UranusGroup.Services
{
    public class ContactService : IContactService
    {
        private readonly UranusGroupContext _context;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public ContactService(UranusGroupContext context, IMapper mapper, IEmailService emailService)
        {
            _context = context;
            _mapper = mapper;
            _emailService = emailService;
        }

        public async Task<ContactDto> CreateContactAsync(CreateContactDto createContactDto)
        {
            var contact = _mapper.Map<Models.Contact>(createContactDto);
            
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();

            // Send notification email
            await _emailService.SendContactNotificationAsync(contact);

            return _mapper.Map<ContactDto>(contact);
        }

        public async Task<IEnumerable<ContactDto>> GetAllContactsAsync()
        {
            var contacts = await _context.Contacts
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ContactDto>>(contacts);
        }

        public async Task<ContactDto?> GetContactByIdAsync(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            return contact == null ? null : _mapper.Map<ContactDto>(contact);
        }

        public async Task<ContactDto> UpdateContactAsync(int id, ContactDto contactDto)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
                throw new ArgumentException("Contact not found");

            _mapper.Map(contactDto, contact);
            contact.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return _mapper.Map<ContactDto>(contact);
        }

        public async Task<bool> DeleteContactAsync(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
                return false;

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> MarkAsReadAsync(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
                return false;

            contact.IsRead = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RespondToContactAsync(int id, string response)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
                return false;

            contact.Response = response;
            contact.RespondedAt = DateTime.UtcNow;
            contact.IsRead = true;

            await _context.SaveChangesAsync();

            // Send response email to customer
            await _emailService.SendContactResponseAsync(contact);

            return true;
        }
    }
}
