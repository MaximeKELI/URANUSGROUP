namespace UranusGroup.DTOs
{
    public class ContactDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; }
        public string? Response { get; set; }
        public DateTime? RespondedAt { get; set; }
    }

    public class CreateContactDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }

    public class ServiceDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? LongDescription { get; set; }
        public string Icon { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public int SortOrder { get; set; }
        public List<ServiceFeatureDto> Features { get; set; } = new();
    }

    public class ServiceFeatureDto
    {
        public int Id { get; set; }
        public string Feature { get; set; } = string.Empty;
        public int SortOrder { get; set; }
    }

    public class NewsletterDto
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public DateTime SubscribedAt { get; set; }
        public bool IsActive { get; set; }
        public string? Source { get; set; }
    }

    public class SubscribeNewsletterDto
    {
        public string Email { get; set; } = string.Empty;
        public string? Source { get; set; }
    }
}
