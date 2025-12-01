using Microsoft.AspNetCore.Mvc;
using UranusGroup.DTOs;
using UranusGroup.Services;
using FluentValidation;

namespace UranusGroup.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly IValidator<CreateContactDto> _validator;

        public ContactController(IContactService contactService, IValidator<CreateContactDto> validator)
        {
            _contactService = contactService;
            _validator = validator;
        }

        [HttpPost]
        public async Task<ActionResult<ContactDto>> CreateContact([FromBody] CreateContactDto createContactDto)
        {
            var validationResult = await _validator.ValidateAsync(createContactDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var contact = await _contactService.CreateContactAsync(createContactDto);
            return CreatedAtAction(nameof(GetContact), new { id = contact.Id }, contact);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactDto>>> GetContacts()
        {
            var contacts = await _contactService.GetAllContactsAsync();
            return Ok(contacts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContactDto>> GetContact(int id)
        {
            var contact = await _contactService.GetContactByIdAsync(id);
            if (contact == null)
                return NotFound();

            return Ok(contact);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ContactDto>> UpdateContact(int id, [FromBody] ContactDto contactDto)
        {
            if (id != contactDto.Id)
                return BadRequest();

            try
            {
                var contact = await _contactService.UpdateContactAsync(id, contactDto);
                return Ok(contact);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteContact(int id)
        {
            var result = await _contactService.DeleteContactAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpPatch("{id}/read")]
        public async Task<ActionResult> MarkAsRead(int id)
        {
            var result = await _contactService.MarkAsReadAsync(id);
            if (!result)
                return NotFound();

            return Ok();
        }

        [HttpPost("{id}/respond")]
        public async Task<ActionResult> RespondToContact(int id, [FromBody] string response)
        {
            var result = await _contactService.RespondToContactAsync(id, response);
            if (!result)
                return NotFound();

            return Ok();
        }
    }
}
