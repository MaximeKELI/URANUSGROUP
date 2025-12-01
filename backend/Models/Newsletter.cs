using System.ComponentModel.DataAnnotations;

namespace UranusGroup.Models
{
    public class Newsletter
    {
        public int Id { get; set; }
        
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;
        
        public DateTime SubscribedAt { get; set; } = DateTime.UtcNow;
        
        public bool IsActive { get; set; } = true;
        
        public DateTime? UnsubscribedAt { get; set; }
        
        [StringLength(50)]
        public string? Source { get; set; } // Website, Social Media, etc.
    }
}
