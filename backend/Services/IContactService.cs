using UranusGroup.DTOs;

namespace UranusGroup.Services
{
    public interface IContactService
    {
        Task<ContactDto> CreateContactAsync(CreateContactDto createContactDto);
        Task<IEnumerable<ContactDto>> GetAllContactsAsync();
        Task<ContactDto?> GetContactByIdAsync(int id);
        Task<ContactDto> UpdateContactAsync(int id, ContactDto contactDto);
        Task<bool> DeleteContactAsync(int id);
        Task<bool> MarkAsReadAsync(int id);
        Task<bool> RespondToContactAsync(int id, string response);
    }
}
