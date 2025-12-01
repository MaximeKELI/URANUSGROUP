using SendGrid;
using SendGrid.Helpers.Mail;
using UranusGroup.Models;

namespace UranusGroup.Services
{
    public class EmailService : IEmailService
    {
        private readonly ISendGridClient _sendGridClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;

        public EmailService(ISendGridClient sendGridClient, IConfiguration configuration, ILogger<EmailService> logger)
        {
            _sendGridClient = sendGridClient;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task SendContactNotificationAsync(Contact contact)
        {
            try
            {
                var from = new EmailAddress("noreply@uranusgroup.com", "URANUS GROUP");
                var to = new EmailAddress("contact@uranusgroup.com", "URANUS GROUP Team");
                
                var subject = $"Nouveau message de contact - {contact.Subject}";
                var htmlContent = $@"
                    <h2>Nouveau message de contact</h2>
                    <p><strong>Nom:</strong> {contact.Name}</p>
                    <p><strong>Email:</strong> {contact.Email}</p>
                    <p><strong>Téléphone:</strong> {contact.Phone ?? "Non fourni"}</p>
                    <p><strong>Sujet:</strong> {contact.Subject}</p>
                    <p><strong>Message:</strong></p>
                    <p>{contact.Message}</p>
                    <p><strong>Date:</strong> {contact.CreatedAt:dd/MM/yyyy HH:mm}</p>
                ";

                var msg = MailHelper.CreateSingleEmail(from, to, subject, null, htmlContent);
                var response = await _sendGridClient.SendEmailAsync(msg);
                
                _logger.LogInformation("Contact notification email sent. Status: {StatusCode}", response.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending contact notification email");
            }
        }

        public async Task SendContactResponseAsync(Contact contact)
        {
            try
            {
                var from = new EmailAddress("contact@uranusgroup.com", "URANUS GROUP");
                var to = new EmailAddress(contact.Email, contact.Name);
                
                var subject = $"Réponse à votre message - {contact.Subject}";
                var htmlContent = $@"
                    <h2>Bonjour {contact.Name},</h2>
                    <p>Merci pour votre message concernant <strong>{contact.Subject}</strong>.</p>
                    <p>Voici notre réponse :</p>
                    <div style='background: #f8f9fa; padding: 20px; border-left: 4px solid #6366f1; margin: 20px 0;'>
                        {contact.Response}
                    </div>
                    <p>Si vous avez d'autres questions, n'hésitez pas à nous contacter.</p>
                    <p>Cordialement,<br>L'équipe URANUS GROUP</p>
                ";

                var msg = MailHelper.CreateSingleEmail(from, to, subject, null, htmlContent);
                var response = await _sendGridClient.SendEmailAsync(msg);
                
                _logger.LogInformation("Contact response email sent. Status: {StatusCode}", response.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending contact response email");
            }
        }

        public async Task SendNewsletterWelcomeAsync(Newsletter newsletter)
        {
            try
            {
                var from = new EmailAddress("newsletter@uranusgroup.com", "URANUS GROUP");
                var to = new EmailAddress(newsletter.Email);
                
                var subject = "Bienvenue dans notre newsletter !";
                var htmlContent = $@"
                    <h2>Bienvenue chez URANUS GROUP !</h2>
                    <p>Merci de vous être abonné à notre newsletter.</p>
                    <p>Vous recevrez régulièrement nos dernières actualités, conseils technologiques et offres spéciales.</p>
                    <p>Restez connecté avec nous !</p>
                    <p>L'équipe URANUS GROUP</p>
                ";

                var msg = MailHelper.CreateSingleEmail(from, to, subject, null, htmlContent);
                var response = await _sendGridClient.SendEmailAsync(msg);
                
                _logger.LogInformation("Newsletter welcome email sent. Status: {StatusCode}", response.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending newsletter welcome email");
            }
        }

        public async Task SendNewsletterAsync(string subject, string content, List<string> recipients)
        {
            try
            {
                var from = new EmailAddress("newsletter@uranusgroup.com", "URANUS GROUP");
                var tos = recipients.Select(email => new EmailAddress(email)).ToList();
                
                var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, tos, subject, null, content);
                var response = await _sendGridClient.SendEmailAsync(msg);
                
                _logger.LogInformation("Newsletter sent to {Count} recipients. Status: {StatusCode}", recipients.Count, response.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending newsletter");
            }
        }
    }
}
