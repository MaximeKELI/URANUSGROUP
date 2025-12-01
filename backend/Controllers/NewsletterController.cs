using Microsoft.AspNetCore.Mvc;
using UranusGroup.DTOs;
using UranusGroup.Services;
using FluentValidation;

namespace UranusGroup.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsletterController : ControllerBase
    {
        private readonly INewsletterService _newsletterService;
        private readonly IValidator<SubscribeNewsletterDto> _validator;

        public NewsletterController(INewsletterService newsletterService, IValidator<SubscribeNewsletterDto> validator)
        {
            _newsletterService = newsletterService;
            _validator = validator;
        }

        [HttpPost("subscribe")]
        public async Task<ActionResult<NewsletterDto>> Subscribe([FromBody] SubscribeNewsletterDto subscribeDto)
        {
            var validationResult = await _validator.ValidateAsync(subscribeDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var subscription = await _newsletterService.SubscribeAsync(subscribeDto);
            return Ok(subscription);
        }

        [HttpPost("unsubscribe")]
        public async Task<ActionResult> Unsubscribe([FromBody] string email)
        {
            var result = await _newsletterService.UnsubscribeAsync(email);
            if (!result)
                return NotFound();

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NewsletterDto>>> GetSubscribers()
        {
            var subscribers = await _newsletterService.GetAllSubscribersAsync();
            return Ok(subscribers);
        }

        [HttpGet("check/{email}")]
        public async Task<ActionResult<bool>> IsSubscribed(string email)
        {
            var isSubscribed = await _newsletterService.IsSubscribedAsync(email);
            return Ok(isSubscribed);
        }
    }
}
